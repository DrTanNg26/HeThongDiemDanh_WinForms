USE QlyDiemDanh;
GO

-- ============================================================
-- FIX: Đảm bảo CHECK constraint cho phép đúng các giá trị
-- và SP dùng đúng chuỗi Unicode N'Vắng'
-- ============================================================

-- BƯỚC 1: Xem giá trị constraint hiện tại trên DB
SELECT
    cc.name           AS TenConstraint,
    cc.definition     AS NoiDung,
    t.name            AS Bang,
    c.name            AS Cot
FROM sys.check_constraints cc
JOIN sys.tables  t ON cc.parent_object_id = t.object_id
JOIN sys.columns c ON cc.parent_object_id = c.object_id
                   AND cc.parent_column_id = c.column_id
WHERE t.name IN ('DiemDanh', 'BuoiHoc');
GO

-- BƯỚC 2: Drop và tạo lại constraint để chắc chắn đúng Unicode
ALTER TABLE dbo.DiemDanh DROP CONSTRAINT IF EXISTS CK_DD_TrangThai;
GO
ALTER TABLE dbo.DiemDanh
    ADD CONSTRAINT CK_DD_TrangThai
    CHECK (TrangThai IN (N'Có mặt', N'Đi muộn', N'Vắng', N'Nghỉ có phép'));
GO

-- BƯỚC 3: Tạo lại SP với N-prefix rõ ràng trên tất cả chuỗi Unicode
CREATE OR ALTER PROCEDURE dbo.sp_DongBuoiHoc
    @MaBuoiHoc    int,
    @ChiKhiHetHan bit = 0
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @TuMoGiaoDich bit = 0;
    BEGIN TRY
        IF @@TRANCOUNT = 0
        BEGIN
            BEGIN TRANSACTION;
            SET @TuMoGiaoDich = 1;
        END
        ELSE SAVE TRANSACTION DongBuoiHoc;

        DECLARE @MaLopHP   varchar(20),
                @TrangThai nvarchar(15),
                @BatDau    datetime,
                @SoPhut    int;

        -- Khóa hàng trước khi xử lý (tránh race condition với sp_GhiNhanDiemDanh)
        SELECT @MaLopHP   = MaLopHP,
               @TrangThai = TrangThai,
               @BatDau    = ThoiGianBatDau,
               @SoPhut    = ThoiGianDemNguoc
        FROM dbo.BuoiHoc WITH (UPDLOCK, HOLDLOCK)
        WHERE MaBuoiHoc = @MaBuoiHoc;

        IF @MaLopHP IS NULL
            THROW 50020, N'Buổi học không còn tồn tại.', 1;

        -- Chỉ đóng khi trạng thái còn 'Mo' và (không hạn chế giờ HOẶC đã hết giờ)
        IF @TrangThai = N'Mo'
           AND (@ChiKhiHetHan = 0
                OR DATEADD(MINUTE, @SoPhut, @BatDau) <= GETDATE())
        BEGIN
            -- Chốt Vắng cho SV chưa có bản ghi điểm danh
            INSERT INTO dbo.DiemDanh (MaBuoiHoc, MaSV, ThoiGianQuet, IP_SV, TrangThai, GhiChu)
            SELECT @MaBuoiHoc,
                   ct.MaSV,
                   NULL,
                   NULL,
                   N'Vắng',                                          -- ← Unicode N prefix
                   N'Hệ thống chốt vắng khi kết thúc buổi học'
            FROM dbo.ChiTietLopHP ct
            WHERE ct.MaLopHP = @MaLopHP
              AND NOT EXISTS (
                    SELECT 1
                    FROM   dbo.DiemDanh dd WITH (UPDLOCK, HOLDLOCK)
                    WHERE  dd.MaBuoiHoc = @MaBuoiHoc
                      AND  dd.MaSV      = ct.MaSV);

            -- Đổi trạng thái buổi học
            UPDATE dbo.BuoiHoc
            SET    TrangThai = N'DaDong'
            WHERE  MaBuoiHoc = @MaBuoiHoc;
        END;

        IF @TuMoGiaoDich = 1 COMMIT TRANSACTION;

        -- Trả kết quả thống kê sau khi đóng
        SELECT
            @MaBuoiHoc AS MaBuoiHoc,
            @MaLopHP   AS MaLopHP,
            (SELECT COUNT(*) FROM dbo.ChiTietLopHP
             WHERE MaLopHP = @MaLopHP)                                     AS TongSiSo,
            COUNT(CASE WHEN TrangThai = N'Có mặt'      THEN 1 END)        AS SoCoMat,
            COUNT(CASE WHEN TrangThai = N'Đi muộn'     THEN 1 END)        AS SoDiMuon,
            COUNT(CASE WHEN TrangThai = N'Vắng'        THEN 1 END)        AS SoVang,
            COUNT(CASE WHEN TrangThai = N'Nghỉ có phép' THEN 1 END)       AS SoNghiPhep
        FROM dbo.DiemDanh
        WHERE MaBuoiHoc = @MaBuoiHoc;

    END TRY
    BEGIN CATCH
        IF @TuMoGiaoDich = 1 AND XACT_STATE() <> 0
            ROLLBACK TRANSACTION;
        ELSE IF @TuMoGiaoDich = 0 AND XACT_STATE() = 1
            ROLLBACK TRANSACTION DongBuoiHoc;
        THROW;
    END CATCH;
END;
GO

PRINT N'Fix hoàn tất: constraint và SP đã được cập nhật!';
GO
