USE master;
GO

-- Ngắt hết kết nối đang dùng rồi xóa
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'QlyDiemDanh')
BEGIN
    ALTER DATABASE QlyDiemDanh SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QlyDiemDanh;
END
GO