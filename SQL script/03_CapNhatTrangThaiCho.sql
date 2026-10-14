-- Cap nhat CSDL da ton tai de ho tro dang ky cho Admin duyet.
-- Khong xoa bang, khong sua du lieu tai khoan hien co.
USE [QlyDiemDanh];
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;
    IF OBJECT_ID(N'dbo.TaiKhoan', N'U') IS NULL
        THROW 50001, N'Khong tim thay bang dbo.TaiKhoan.', 1;

    IF EXISTS (SELECT 1 FROM dbo.TaiKhoan
               WHERE TrangThai NOT IN (N'Hoạt động', N'Bị khóa', N'Chờ') OR TrangThai IS NULL)
        THROW 50002, N'Co trang thai tai khoan khong hop le. Can kiem tra truoc khi cap nhat.', 1;

    IF EXISTS (SELECT 1 FROM sys.check_constraints
               WHERE parent_object_id = OBJECT_ID(N'dbo.TaiKhoan') AND name = N'CK_TK_TrangThai')
        ALTER TABLE dbo.TaiKhoan DROP CONSTRAINT CK_TK_TrangThai;

    ALTER TABLE dbo.TaiKhoan WITH CHECK
        ADD CONSTRAINT CK_TK_TrangThai
        CHECK (TrangThai IN (N'Hoạt động', N'Bị khóa', N'Chờ'));
    ALTER TABLE dbo.TaiKhoan CHECK CONSTRAINT CK_TK_TrangThai;
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
