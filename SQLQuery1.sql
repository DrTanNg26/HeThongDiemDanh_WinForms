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