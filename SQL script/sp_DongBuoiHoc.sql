USE QlyDiemDanh;
GO

-- =============================================
-- Author:      Thành viên 2 (Phân hệ Giảng viên)
-- Create date: 2026-10-07
-- Description: Đóng buổi học điểm danh, tự động đánh dấu 'Vắng' cho các sinh viên chưa điểm danh,
--              cập nhật trạng thái buổi học thành 'DaDong' và trả về thống kê tổng kết.
-- =============================================
CREATE OR ALTER PROCEDURE sp_DongBuoiHoc
    @MaBuoiHoc INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra buổi học có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM BuoiHoc WHERE MaBuoiHoc = @MaBuoiHoc)
    BEGIN
        RAISERROR(N'Buổi học không tồn tại.', 16, 1);
        RETURN;
    END

    -- Lấy thông tin lớp học phần của buổi học
    DECLARE @MaLopHP VARCHAR(20);
    DECLARE @TrangThaiHienTai NVARCHAR(15);

    SELECT @MaLopHP = MaLopHP, @TrangThaiHienTai = TrangThai
    FROM BuoiHoc
    WHERE MaBuoiHoc = @MaBuoiHoc;

    -- Nếu buổi học đã đóng từ trước thì không thực hiện lại mà vẫn trả về kết quả
    IF (@TrangThaiHienTai <> N'DaDong')
    BEGIN
        BEGIN TRANSACTION;
        BEGIN TRY
            -- 1. Chèn bản ghi 'Vắng' cho mọi sinh viên trong lớp học phần chưa có bản ghi điểm danh
            INSERT INTO DiemDanh (MaBuoiHoc, MaSV, ThoiGianQuet, IP_SV, TrangThai, GhiChu)
            SELECT 
                @MaBuoiHoc,
                ct.MaSV,
                NULL,
                NULL,
                N'Vắng',
                N'Tự động đánh vắng khi kết thúc buổi học'
            FROM ChiTietLopHP ct
            WHERE ct.MaLopHP = @MaLopHP
              AND NOT EXISTS (
                  SELECT 1 
                  FROM DiemDanh dd 
                  WHERE dd.MaBuoiHoc = @MaBuoiHoc 
                    AND dd.MaSV = ct.MaSV
              );

            -- 2. Cập nhật trạng thái buổi học thành 'DaDong'
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

    -- 3. Trả về bảng thống kê tổng kết
    DECLARE @TongSiSo INT = 0;
    DECLARE @SoCoMat INT = 0;
    DECLARE @SoDiMuon INT = 0;
    DECLARE @SoVang INT = 0;
    DECLARE @SoNghiPhep INT = 0;

    SELECT @TongSiSo = COUNT(1)
    FROM ChiTietLopHP
    WHERE MaLopHP = @MaLopHP;

    SELECT 
        @SoCoMat = COUNT(CASE WHEN TrangThai = N'Có mặt' THEN 1 END),
        @SoDiMuon = COUNT(CASE WHEN TrangThai = N'Đi muộn' THEN 1 END),
        @SoVang = COUNT(CASE WHEN TrangThai = N'Vắng' THEN 1 END),
        @SoNghiPhep = COUNT(CASE WHEN TrangThai = N'Nghỉ có phép' THEN 1 END)
    FROM DiemDanh
    WHERE MaBuoiHoc = @MaBuoiHoc;

    SELECT 
        @MaBuoiHoc AS MaBuoiHoc,
        @MaLopHP AS MaLopHP,
        @TongSiSo AS TongSiSo,
        @SoCoMat AS SoCoMat,
        @SoDiMuon AS SoDiMuon,
        @SoVang AS SoVang,
        @SoNghiPhep AS SoNghiPhep,
        (@SoCoMat + @SoDiMuon) AS TongDaDiemDanh;
END;
GO

PRINT N'Tạo Stored Procedure sp_DongBuoiHoc thành công!';
GO
