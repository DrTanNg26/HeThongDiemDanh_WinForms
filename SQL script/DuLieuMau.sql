-- =============================================
-- Script 02: Dữ liệu mẫu
-- Mật khẩu mẫu đều là "123456" 
-- (chuỗi SHA256: 8d969eef6ecad3c29a3a629280e686cf
--                0c3f5d5a86aff3ca12020c923adc6c92)
-- Trong C# bạn sẽ băm trước khi lưu thật,
-- ở đây dùng chuỗi plaintext để test nhanh
-- =============================================
USE QlyDiemDanh;
GO

-- Khoa
INSERT INTO Khoa VALUES ('KHCN',  N'Khoa Công nghệ thông tin');
INSERT INTO Khoa VALUES ('KHKT',  N'Khoa Kỹ thuật');
INSERT INTO Khoa VALUES ('KHKT2', N'Khoa Kinh tế - Vận tải');
GO

-- Lớp hành chính
INSERT INTO LopHanhChinh VALUES ('CNTT14A', N'CNTT14A', 'KHCN');
INSERT INTO LopHanhChinh VALUES ('CNTT14B', N'CNTT14B', 'KHCN');
GO

-- Môn học
INSERT INTO MonHoc VALUES ('LTPQ',  N'Lập trình trực quan', 3);
INSERT INTO MonHoc VALUES ('CSDL',  N'Cơ sở dữ liệu',       3);
INSERT INTO MonHoc VALUES ('MANG',  N'Mạng máy tính',        2);
GO

-- Tài khoản (mật khẩu plaintext "123456" để test)
INSERT INTO TaiKhoan VALUES ('AD01',   '123456', N'Admin',      N'Hoạt động');
INSERT INTO TaiKhoan VALUES ('AD02',   '123456', N'Admin',      N'Hoạt động');
INSERT INTO TaiKhoan VALUES ('GV01',   '123456', N'GiangVien',  N'Hoạt động');
INSERT INTO TaiKhoan VALUES ('GV02',   '123456', N'GiangVien',  N'Hoạt động');
INSERT INTO TaiKhoan VALUES ('SV001',  '123456', N'SinhVien',   N'Hoạt động');
INSERT INTO TaiKhoan VALUES ('SV002',  '123456', N'SinhVien',   N'Hoạt động');
INSERT INTO TaiKhoan VALUES ('SV003',  '123456', N'SinhVien',   N'Hoạt động');
INSERT INTO TaiKhoan VALUES ('SV004',  '123456', N'SinhVien',   N'Hoạt động');
INSERT INTO TaiKhoan VALUES ('SV005',  '123456', N'SinhVien',   N'Hoạt động');
GO

-- Admin
INSERT INTO Admin VALUES ('AD01', N'Nguyễn Quản Trị', 'admin@utt.edu.vn');
INSERT INTO Admin VALUES ('AD02', N'Nguyễn Duy Tân', 'tanadmin@utt.edu.vn');

GO

-- Giảng viên
INSERT INTO GiangVien VALUES ('GV01', N'Trần Văn An',   'gv01@utt.edu.vn', N'Thạc sĩ',  'KHCN');
INSERT INTO GiangVien VALUES ('GV02', N'Lê Thị Bình',   'gv02@utt.edu.vn', N'Tiến sĩ',  'KHCN');
GO

-- Sinh viên (NgaySinh và SoCCCD dùng cho xác thực bước 2)
INSERT INTO SinhVien VALUES ('SV001', N'Nguyễn Văn An',    'sv001@utt.edu.vn', '2004-05-10', '001234567890', 'CNTT14A', 'KHCN');
INSERT INTO SinhVien VALUES ('SV002', N'Trần Thị Bảo',     'sv002@utt.edu.vn', '2004-08-22', '002345678901', 'CNTT14A', 'KHCN');
INSERT INTO SinhVien VALUES ('SV003', N'Lê Minh Châu',     'sv003@utt.edu.vn', '2004-11-05', '003456789012', 'CNTT14A', 'KHCN');
INSERT INTO SinhVien VALUES ('SV004', N'Phạm Thị Dung',    'sv004@utt.edu.vn', '2004-03-18', '004567890123', 'CNTT14B', 'KHCN');
INSERT INTO SinhVien VALUES ('SV005', N'Hoàng Văn Em',     'sv005@utt.edu.vn', '2004-07-30', '005678901234', 'CNTT14B', 'KHCN');
GO

-- Lớp học phần
INSERT INTO LopHocPhan VALUES ('LTPQ_N01', N'Lập trình trực quan - Nhóm 01', 'LTPQ', 'GV01', 'P502-H3', '2025-2026_HK1', 0);
INSERT INTO LopHocPhan VALUES ('CSDL_N01', N'Cơ sở dữ liệu - Nhóm 01',       'CSDL', 'GV02', 'P301-H2', '2025-2026_HK1', 0);
GO

-- Phân lớp học phần
INSERT INTO ChiTietLopHP VALUES ('LTPQ_N01', 'SV001');
INSERT INTO ChiTietLopHP VALUES ('LTPQ_N01', 'SV002');
INSERT INTO ChiTietLopHP VALUES ('LTPQ_N01', 'SV003');
INSERT INTO ChiTietLopHP VALUES ('LTPQ_N01', 'SV004');
INSERT INTO ChiTietLopHP VALUES ('LTPQ_N01', 'SV005');
INSERT INTO ChiTietLopHP VALUES ('CSDL_N01', 'SV001');
INSERT INTO ChiTietLopHP VALUES ('CSDL_N01', 'SV004');
GO

PRINT N'Dữ liệu mẫu đã được thêm.';
GO