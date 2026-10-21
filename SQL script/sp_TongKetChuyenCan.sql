USE QlyDiemDanh;
GO

-- =======================================================================
-- THỦ TỤC: sp_TongKetChuyenCan
-- MỤC ĐÍCH: Tính % chuyên cần, quy đổi điểm và lưu vào KetQuaChuyenCan
-- ĐẶC ĐIỂM: Dùng MERGE để chạy lại nhiều lần mà không bị trùng dữ liệu
-- =======================================================================
CREATE OR ALTER PROCEDURE dbo.sp_TongKetChuyenCan
    @MaLopHP VARCHAR(20) -- Mã lớp học phần cần tổng kết
AS
BEGIN
    SET NOCOUNT ON;

    -- [BƯỚC 1]: Kiểm tra nếu còn buổi học đang mở (chưa kết thúc) thì chặn không cho tổng kết
    IF EXISTS (SELECT 1 FROM dbo.BuoiHoc WHERE MaLopHP = @MaLopHP AND TrangThai = N'Mo')
    BEGIN
        RAISERROR(N'Lớp vẫn còn buổi học đang mở. Vui lòng đóng trước khi tổng kết!', 16, 1);
        RETURN;
    END

    -- [BƯỚC 2]: Đếm tổng số buổi đã học xong (TrangThai = 'DaDong') và cập nhật vào LopHocPhan
    DECLARE @TongBuoi INT = (SELECT COUNT(*) FROM dbo.BuoiHoc WHERE MaLopHP = @MaLopHP AND TrangThai = N'DaDong');
    UPDATE dbo.LopHocPhan SET TongSoBuoi = @TongBuoi WHERE MaLopHP = @MaLopHP;

    -- [BƯỚC 3]: Tính toán chuyên cần từng sinh viên theo công thức
    ;WITH ThongKe AS (
        SELECT 
            ct.MaLopHP,
            ct.MaSV,
            @TongBuoi AS TongSoBuoi,
            -- Đếm số buổi theo từng trạng thái điểm danh
            ISNULL(SUM(CASE WHEN dd.TrangThai = N'Có mặt' THEN 1 ELSE 0 END), 0) AS SoCoMat,
            ISNULL(SUM(CASE WHEN dd.TrangThai = N'Đi muộn' THEN 1 ELSE 0 END), 0) AS SoDiMuon,
            -- Nếu buổi học đã đóng mà SV chưa quét (dd.TrangThai IS NULL) hoặc đã ghi 'Vắng' -> tính là Vắng
            ISNULL(SUM(CASE WHEN bh.MaBuoiHoc IS NOT NULL AND (dd.TrangThai IS NULL OR dd.TrangThai = N'Vắng') THEN 1 ELSE 0 END), 0) AS SoVang,
            ISNULL(SUM(CASE WHEN dd.TrangThai = N'Nghỉ có phép' THEN 1 ELSE 0 END), 0) AS SoNghiPhep
        FROM dbo.ChiTietLopHP ct
        LEFT JOIN dbo.BuoiHoc bh ON bh.MaLopHP = ct.MaLopHP AND bh.TrangThai = N'DaDong'
        LEFT JOIN dbo.DiemDanh dd ON dd.MaBuoiHoc = bh.MaBuoiHoc AND dd.MaSV = ct.MaSV
        WHERE ct.MaLopHP = @MaLopHP
        GROUP BY ct.MaLopHP, ct.MaSV
    ),
    TinhDiem AS (
        SELECT *,
            -- Công thức: % = (Có mặt + Muộn * 0.5) / (Tổng buổi - Nghỉ phép) * 100
            CASE WHEN (TongSoBuoi - SoNghiPhep) > 0 
                 THEN ROUND(((SoCoMat + SoDiMuon * 0.5) / (TongSoBuoi - SoNghiPhep)) * 100.0, 2)
                 ELSE 100.0 END AS TyLeChuyenCan
        FROM ThongKe
    ),
    QuyDoi AS (
        SELECT *,
            -- Quy đổi thang điểm 10:
            --   >= 90% → 10 | >= 80% → 9 | >= 70% → 8 | >= 50% → 6 | < 50% → 0
            CASE WHEN TyLeChuyenCan >= 90.0 THEN 10.0
                 WHEN TyLeChuyenCan >= 80.0 THEN  9.0
                 WHEN TyLeChuyenCan >= 70.0 THEN  8.0
                 WHEN TyLeChuyenCan >= 50.0 THEN  6.0  -- 50–69% → 6 điểm
                 ELSE 0.0 END AS DiemChuyenCan,          -- < 50%  → 0 điểm, cấm thi
            -- Cờ cấm thi: dưới 50% chuyên cần
            CASE WHEN TyLeChuyenCan < 50.0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS BiCamThi
        FROM TinhDiem
    )
    -- [BƯỚC 4]: MERGE vào KetQuaChuyenCan
    MERGE dbo.KetQuaChuyenCan AS target
    USING QuyDoi AS src
    ON (target.MaLopHP = src.MaLopHP AND target.MaSV = src.MaSV)
    WHEN MATCHED THEN
        UPDATE SET target.TongSoBuoi     = src.TongSoBuoi,
                   target.SoBuoiCoMat    = src.SoCoMat,
                   target.SoBuoiDiMuon   = src.SoDiMuon,
                   target.SoBuoiVang     = src.SoVang,
                   target.SoBuoiNghiPhep = src.SoNghiPhep,
                   target.TyLeChuyenCan  = src.TyLeChuyenCan,
                   target.DiemChuyenCan  = src.DiemChuyenCan,
                   target.BiCamThi       = src.BiCamThi,
                   target.NgayTongKet    = GETDATE()
    WHEN NOT MATCHED THEN
        INSERT (MaLopHP, MaSV, TongSoBuoi, SoBuoiCoMat, SoBuoiDiMuon, SoBuoiVang, SoBuoiNghiPhep, TyLeChuyenCan, DiemChuyenCan, BiCamThi, NgayTongKet)
        VALUES (src.MaLopHP, src.MaSV, src.TongSoBuoi, src.SoCoMat, src.SoDiMuon, src.SoVang, src.SoNghiPhep, src.TyLeChuyenCan, src.DiemChuyenCan, src.BiCamThi, GETDATE());

    -- [BƯỚC 5]: Trả về kết quả hoàn chỉnh để hiển thị lên lưới
    SELECT kq.*, sv.HoTen, sv.NgaySinh, sv.MaLop AS LopHanhChinh
    FROM dbo.KetQuaChuyenCan kq
    JOIN dbo.SinhVien sv ON sv.MaSV = kq.MaSV
    WHERE kq.MaLopHP = @MaLopHP
    ORDER BY kq.MaSV;
END;
GO
