USE QlyDiemDanh;
GO

-- =============================================
-- Author:      Thành viên 2 (Phân hệ Giảng viên)
-- Create date: 2026-10-07
-- Description: Đóng buổi học điểm danh, tự động đánh 'Vắng' sinh viên chưa quét,
--              cập nhật trạng thái 'DaDong' và trả về thống kê khớp với BuoiHocDAL.cs.
-- =============================================
CREATE OR ALTER PROCEDURE sp_DongBuoiHoc
    @MaBuoiHoc INT
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Kiểm tra tồn tại
    IF NOT EXISTS (SELECT 1 FROM BuoiHoc WHERE MaBuoiHoc = @MaBuoiHoc)
    BEGIN
        RAISERROR(N'Buổi học không tồn tại.', 16, 1);
        RETURN;
    END

    DECLARE @MaLopHP VARCHAR(20);
    DECLARE @TrangThaiHienTai NVARCHAR(15);

    SELECT @MaLopHP = MaLopHP, @TrangThaiHienTai = TrangThai
    FROM BuoiHoc
    WHERE MaBuoiHoc = @MaBuoiHoc;

    -- 2. Nếu phiên chưa đóng: tự động chèn 'Vắng' cho các sinh viên còn lại và chuyển trạng thái 'DaDong'
    IF (@TrangThaiHienTai <> N'DaDong')
    BEGIN
        BEGIN TRANSACTION;
        BEGIN TRY
            -- Tự động đánh Vắng cho mọi sinh viên trong lớp chưa có bản ghi điểm danh
            INSERT INTO DiemDanh (MaBuoiHoc, MaSV, ThoiGianQuet, IP_SV, TrangThai, GhiChu)
            SELECT 
                @MaBuoiHoc,
                ct.MaSV,
                NULL,
                NULL,
                N'Vắng',
                N'Hệ thống tự động đánh vắng khi đóng buổi học'
            FROM ChiTietLopHP ct
            WHERE ct.MaLopHP = @MaLopHP
              AND NOT EXISTS (
                  SELECT 1 FROM DiemDanh dd 
                  WHERE dd.MaBuoiHoc = @MaBuoiHoc AND dd.MaSV = ct.MaSV
              );

            -- Chuyển trạng thái buổi học
            UPDATE BuoiHoc
            SET TrangThai = N'DaDong'
            WHERE MaBuoiHoc = @MaBuoiHoc;

            COMMIT TRANSACTION;
        END TRY
        BEGIN CATCH
            ROLLBACK TRANSACTION;
            DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
            RAISERROR(@ErrMsg, 16, 1);
            RETURN;
        END CATCH
    END

    -- 3. Trả về bảng tổng kết (các cột khớp trực tiếp với KetQuaDongBuoiHoc trong BuoiHocDAL.cs)
    SELECT 
        @MaBuoiHoc AS MaBuoiHoc,
        @MaLopHP AS MaLopHP,
        (SELECT COUNT(1) FROM ChiTietLopHP WHERE MaLopHP = @MaLopHP) AS TongSiSo,
        COUNT(CASE WHEN TrangThai = N'Có mặt' THEN 1 END) AS SoCoMat,
        COUNT(CASE WHEN TrangThai = N'Đi muộn' THEN 1 END) AS SoDiMuon,
        COUNT(CASE WHEN TrangThai = N'Vắng' THEN 1 END) AS SoVang,
        COUNT(CASE WHEN TrangThai = N'Nghỉ có phép' THEN 1 END) AS SoNghiPhep
    FROM DiemDanh
    WHERE MaBuoiHoc = @MaBuoiHoc;
END;
GO

PRINT N'Tạo Stored Procedure sp_DongBuoiHoc thành công!';
GO
