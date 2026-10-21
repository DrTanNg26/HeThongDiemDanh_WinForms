USE QlyDiemDanh;
GO

-- =======================================================================
-- SCRIPT: Dữ liệu điểm danh mẫu nhiều buổi
-- MỤC ĐÍCH: Dữ liệu chuẩn để test màn hình Lịch sử, Sửa thủ công & Tổng kết
-- KỊCH BẢN KIỂM THỬ ĐẦY ĐỦ CÁC TRƯỜNG HỢP:
--   1. SV001: Đi đủ 5/5 buổi             -> 100% -> 10 điểm (Đạt)
--   2. SV002: Đi 4 buổi, Muộn 1 buổi     ->  90% -> 10 điểm (Đạt)
--   3. SV003: Đi 3, Muộn 1, Nghỉ phép 1  -> 87.5% -> 9 điểm (Đạt)
--   4. SV004: Đi 2, Muộn 1, Vắng 2       ->  50% ->  6 điểm (Cảnh báo vàng)
--   5. SV005: Muộn 1, Vắng 4             ->  10% ->  0 điểm (CẤM THI ĐỎ)
-- =======================================================================

-- Xóa dữ liệu cũ trước khi nạp mới
DELETE FROM DiemDanh;
DELETE FROM KetQuaChuyenCan;
DELETE FROM BuoiHoc;
GO

-- [1] Tạo 5 buổi học đã kết thúc (DaDong) cho Lớp LTPQ_N01
SET IDENTITY_INSERT BuoiHoc ON;
INSERT INTO BuoiHoc (MaBuoiHoc, MaLopHP, TenBuoiHoc, NgayHoc, MaOTP, IP_GV, ThoiGianBatDau, TrangThai) VALUES
(1, 'LTPQ_N01', N'Buổi 1: Giới thiệu C# & WinForms', '2026-09-01', '123456', '192.168.1.100', '2026-09-01 07:30', N'DaDong'),
(2, 'LTPQ_N01', N'Buổi 2: Thiết kế Controls',        '2026-09-08', '234567', '192.168.1.100', '2026-09-08 07:30', N'DaDong'),
(3, 'LTPQ_N01', N'Buổi 3: Xử lý sự kiện',            '2026-09-15', '345678', '192.168.1.100', '2026-09-15 07:30', N'DaDong'),
(4, 'LTPQ_N01', N'Buổi 4: Kết nối CSDL ADO.NET',      '2026-09-22', '456789', '192.168.1.100', '2026-09-22 07:30', N'DaDong'),
(5, 'LTPQ_N01', N'Buổi 5: Xuất báo cáo ClosedXML',   '2026-09-29', '567890', '192.168.1.100', '2026-09-29 07:30', N'DaDong'),
-- Tạo 3 buổi học cho Lớp CSDL_N01
(6, 'CSDL_N01', N'Buổi 1: Mô hình thực thể ER',      '2026-09-03', '678901', '192.168.1.105', '2026-09-03 13:30', N'DaDong'),
(7, 'CSDL_N01', N'Buổi 2: Truy vấn SQL T-SQL',       '2026-09-10', '789012', '192.168.1.105', '2026-09-10 13:30', N'DaDong'),
(8, 'CSDL_N01', N'Buổi 3: Stored Procedure & Trigger','2026-09-17', '890123', '192.168.1.105', '2026-09-17 13:30', N'DaDong');
SET IDENTITY_INSERT BuoiHoc OFF;
GO

-- [2] Chi tiết điểm danh từng buổi học
INSERT INTO DiemDanh (MaBuoiHoc, MaSV, ThoiGianQuet, IP_SV, TrangThai, GhiChu) VALUES
-- Buổi 1: SV004 đi muộn, SV005 vắng
(1, 'SV001', '2026-09-01 07:32', '192.168.1.12', N'Có mặt', NULL),
(1, 'SV002', '2026-09-01 07:33', '192.168.1.14', N'Có mặt', NULL),
(1, 'SV003', '2026-09-01 07:34', '192.168.1.15', N'Có mặt', NULL),
(1, 'SV004', '2026-09-01 07:42', '192.168.1.18', N'Đi muộn', N'Quét muộn 12 phút'),
(1, 'SV005', NULL,               NULL,            N'Vắng',     NULL),

-- Buổi 2: SV002 đi muộn, SV003 nghỉ phép có đơn, SV004 và SV005 vắng
(2, 'SV001', '2026-09-08 07:31', '192.168.1.12', N'Có mặt', NULL),
(2, 'SV002', '2026-09-08 07:44', '192.168.1.14', N'Đi muộn', N'Kẹt xe'),
(2, 'SV003', NULL,               NULL,            N'Nghỉ có phép', N'Gửi đơn xin phép qua email'),
(2, 'SV004', NULL,               NULL,            N'Vắng',     NULL),
(2, 'SV005', NULL,               NULL,            N'Vắng',     NULL),

-- Buổi 3: SV005 vắng
(3, 'SV001', '2026-09-15 07:33', '192.168.1.12', N'Có mặt', NULL),
(3, 'SV002', '2026-09-15 07:32', '192.168.1.14', N'Có mặt', NULL),
(3, 'SV003', '2026-09-15 07:35', '192.168.1.15', N'Có mặt', NULL),
(3, 'SV004', '2026-09-15 07:31', '192.168.1.18', N'Có mặt', NULL),
(3, 'SV005', NULL,               NULL,            N'Vắng',     NULL),

-- Buổi 4: SV003 muộn, SV004 vắng, SV005 muộn
(4, 'SV001', '2026-09-22 07:31', '192.168.1.12', N'Có mặt', NULL),
(4, 'SV002', '2026-09-22 07:34', '192.168.1.14', N'Có mặt', NULL),
(4, 'SV003', '2026-09-22 07:38', '192.168.1.15', N'Đi muộn', N'Đến muộn do thời tiết'),
(4, 'SV004', NULL,               NULL,            N'Vắng',     NULL),
(4, 'SV005', '2026-09-22 07:43', '192.168.1.20', N'Đi muộn', N'Vào muộn'),

-- Buổi 5: SV005 vắng
(5, 'SV001', '2026-09-29 07:30', '192.168.1.12', N'Có mặt', NULL),
(5, 'SV002', '2026-09-29 07:31', '192.168.1.14', N'Có mặt', NULL),
(5, 'SV003', '2026-09-29 07:32', '192.168.1.15', N'Có mặt', NULL),
(5, 'SV004', '2026-09-29 07:33', '192.168.1.18', N'Có mặt', NULL),
(5, 'SV005', NULL,               NULL,            N'Vắng',     NULL),

-- Lớp CSDL_N01:
(6, 'SV001', '2026-09-03 13:31', '192.168.1.12', N'Có mặt', NULL),
(6, 'SV004', '2026-09-03 13:32', '192.168.1.18', N'Có mặt', NULL),
(7, 'SV001', '2026-09-10 13:30', '192.168.1.12', N'Có mặt', NULL),
(7, 'SV004', NULL,               NULL,            N'Nghỉ có phép', N'Có giấy phép y tế'),
(8, 'SV001', '2026-09-17 13:31', '192.168.1.12', N'Có mặt', NULL),
(8, 'SV004', '2026-09-17 13:40', '192.168.1.18', N'Đi muộn', N'Muộn 10 phút');
GO

PRINT N'✅ Đã nạp thành công dữ liệu điểm danh mẫu!';
GO
