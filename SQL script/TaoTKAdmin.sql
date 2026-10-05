USE [master];
GO

-- 1. Kiểm tra nếu đã tồn tại Login AdminApp thì xóa đi để tạo mới sạch sẽ
IF EXISTS (SELECT * FROM sys.server_principals WHERE name = N'AdminApp')
BEGIN
    DROP LOGIN [AdminApp];
END
GO

-- 2. Tạo Login AdminApp với Mật khẩu 123456, tắt ràng buộc độ phức tạp & hết hạn mật khẩu
CREATE LOGIN [AdminApp] 
WITH PASSWORD = N'admin123', 
     DEFAULT_DATABASE = [master], 
     CHECK_EXPIRATION = OFF, 
     CHECK_POLICY = OFF;
GO

-- 3. Cấp Full quyền quản trị tối cao (sysadmin) cho tài khoản AdminApp
ALTER SERVER ROLE [sysadmin] ADD MEMBER [AdminApp];
GO

-- 4. Chuyển sang CSDL HeThongDiemDanh và cấp quyền db_owner (Full quyền CSDL)
USE [QlyDiemDanh]; -- Thay bằng đúng tên CSDL của bạn nếu khác
GO

IF EXISTS (SELECT * FROM sys.database_principals WHERE name = N'AdminApp')
BEGIN
    DROP USER [AdminApp];
END
GO

CREATE USER [AdminApp] FOR LOGIN [AdminApp];
ALTER ROLE [db_owner] ADD MEMBER [AdminApp];
GO