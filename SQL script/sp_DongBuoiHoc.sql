USE QlyDiemDanh;
GO
-- Đóng phiên nguyên tử; có thể gọi lặp từ nút kết thúc và bộ đếm giờ.
CREATE OR ALTER PROCEDURE dbo.sp_DongBuoiHoc
    @MaBuoiHoc int,
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

        DECLARE @MaLopHP varchar(20), @TrangThai nvarchar(15);
        DECLARE @BatDau datetime, @SoPhut int;
        -- Cùng thứ tự khóa với sp_GhiNhanDiemDanh để không chốt vắng trong lúc ghi có mặt.
        SELECT @MaLopHP = MaLopHP, @TrangThai = TrangThai,
               @BatDau = ThoiGianBatDau, @SoPhut = ThoiGianDemNguoc
        FROM dbo.BuoiHoc WITH (UPDLOCK, HOLDLOCK)
        WHERE MaBuoiHoc = @MaBuoiHoc;
        IF @MaLopHP IS NULL
            THROW 50020, N'Buổi học không còn tồn tại.', 1;

        IF @TrangThai = N'Mo' AND (@ChiKhiHetHan = 0 OR DATEADD(MINUTE, @SoPhut, @BatDau) <= GETDATE())
        BEGIN
            INSERT dbo.DiemDanh (MaBuoiHoc, MaSV, ThoiGianQuet, IP_SV, TrangThai, GhiChu)
            SELECT @MaBuoiHoc, ct.MaSV, NULL, NULL, N'Vắng', N'Hệ thống chốt vắng khi kết thúc buổi học'
            FROM dbo.ChiTietLopHP ct
            WHERE ct.MaLopHP = @MaLopHP
              AND NOT EXISTS (SELECT 1 FROM dbo.DiemDanh dd WITH (UPDLOCK, HOLDLOCK)
                              WHERE dd.MaBuoiHoc = @MaBuoiHoc AND dd.MaSV = ct.MaSV);
            UPDATE dbo.BuoiHoc SET TrangThai = N'DaDong' WHERE MaBuoiHoc = @MaBuoiHoc;
        END;

        IF @TuMoGiaoDich = 1 COMMIT TRANSACTION;
        SELECT @MaBuoiHoc AS MaBuoiHoc, @MaLopHP AS MaLopHP,
               (SELECT COUNT(*) FROM dbo.ChiTietLopHP WHERE MaLopHP = @MaLopHP) AS TongSiSo,
               COUNT(CASE WHEN TrangThai = N'Có mặt' THEN 1 END) AS SoCoMat,
               COUNT(CASE WHEN TrangThai = N'Đi muộn' THEN 1 END) AS SoDiMuon,
               COUNT(CASE WHEN TrangThai = N'Vắng' THEN 1 END) AS SoVang,
               COUNT(CASE WHEN TrangThai = N'Nghỉ có phép' THEN 1 END) AS SoNghiPhep
        FROM dbo.DiemDanh WHERE MaBuoiHoc = @MaBuoiHoc;
    END TRY
    BEGIN CATCH
        IF @TuMoGiaoDich = 1 AND XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        ELSE IF @TuMoGiaoDich = 0 AND XACT_STATE() = 1 ROLLBACK TRANSACTION DongBuoiHoc;
        THROW;
    END CATCH;
END;
GO
