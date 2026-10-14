-- Chạy sau khi đã có các bảng trong QlyDiemDanh.
-- Có thể chạy lại: chỉ tạo/cập nhật hàm và thủ tục, không xóa bảng hay dữ liệu.
USE QlyDiemDanh;
GO

-- Đổi IPv4 dạng a.b.c.d thành số để so sánh subnet bằng phép AND.
-- Trả NULL nếu địa chỉ thiếu phần, ngoài 0..255 hoặc có ký tự khác chữ số/dấu chấm.
CREATE OR ALTER FUNCTION dbo.fn_IPv4ThanhSo (@DiaChi varchar(50))
RETURNS bigint
AS
BEGIN
    IF @DiaChi IS NULL OR DATALENGTH(@DiaChi) < 7 OR DATALENGTH(@DiaChi) > 15
        RETURN NULL;
    IF @DiaChi COLLATE Latin1_General_100_BIN2 LIKE '%[^0-9.]%'
        RETURN NULL;
    IF LEN(@DiaChi) - LEN(REPLACE(@DiaChi, '.', '')) <> 3
        RETURN NULL;

    DECLARE @KetQua bigint = 0;
    DECLARE @ViTri int = 1;
    DECLARE @Lan int = 0;
    DECLARE @DauCham int;
    DECLARE @Phan varchar(50);
    DECLARE @GiaTri int;

    WHILE @Lan < 4
    BEGIN
        SET @DauCham = CHARINDEX('.', @DiaChi + '.', @ViTri);
        SET @Phan = SUBSTRING(@DiaChi, @ViTri, @DauCham - @ViTri);
        IF LEN(@Phan) < 1 OR LEN(@Phan) > 3
            RETURN NULL;
        SET @GiaTri = TRY_CONVERT(int, @Phan);
        IF @GiaTri IS NULL OR @GiaTri < 0 OR @GiaTri > 255
            RETURN NULL;
        SET @KetQua = @KetQua * 256 + @GiaTri;
        SET @ViTri = @DauCham + 1;
        SET @Lan = @Lan + 1;
    END;

    RETURN @KetQua;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_GhiNhanDiemDanh
    @MaSV varchar(10),
    @MaBuoiHoc int,
    @MaOTP varchar(10),
    @LoaiCauHoi varchar(20),
    @TraLoi nvarchar(100),
    @IP_SV varchar(50),
    @BoQuaKiemTraIP bit = 0,
    @GioiHanPhut int = 240
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @GioiHanPhut IS NULL OR @GioiHanPhut < 1 OR @GioiHanPhut > 1440
        THROW 50001, N'Cấu hình thời hạn buổi điểm danh phải từ 1 đến 1440 phút.', 1;
    IF @BoQuaKiemTraIP IS NULL
        THROW 50002, N'Cấu hình kiểm tra mạng không hợp lệ.', 1;

    DECLARE @TuMoGiaoDich bit = 0;
    BEGIN TRY
        IF @@TRANCOUNT = 0
        BEGIN
            BEGIN TRANSACTION;
            SET @TuMoGiaoDich = 1;
        END
        ELSE
            SAVE TRANSACTION GhiNhanDiemDanh;

        DECLARE @MaLopHP varchar(20);
        DECLARE @OTPDung varchar(10);
        DECLARE @IP_GV varchar(50);
        DECLARE @Mask_GV varchar(50);
        DECLARE @BatDau datetime;
        DECLARE @DemNguoc int;
        DECLARE @TrangThaiBuoi nvarchar(15);

        -- Giữ khóa buổi học tới khi ghi xong: đổi OTP/đóng phiên sẽ phải chờ.
        SELECT @MaLopHP = MaLopHP, @OTPDung = MaOTP, @IP_GV = IP_GV,
               @Mask_GV = SubnetMask_GV, @BatDau = ThoiGianBatDau,
               @DemNguoc = ThoiGianDemNguoc, @TrangThaiBuoi = TrangThai
        FROM dbo.BuoiHoc WITH (UPDLOCK, HOLDLOCK)
        WHERE MaBuoiHoc = @MaBuoiHoc;

        IF @MaLopHP IS NULL
            THROW 50003, N'Buổi học không còn tồn tại. Hãy làm mới danh sách.', 1;
        IF @DemNguoc IS NULL OR @DemNguoc < 0 OR @DemNguoc > 1440
            THROW 50005, N'Thời gian đếm ngược của buổi học không hợp lệ. Hãy báo giảng viên.', 1;

        DECLARE @NgaySinh date;
        DECLARE @SoCCCD varchar(12);
        SELECT @NgaySinh = sv.NgaySinh, @SoCCCD = sv.SoCCCD
        FROM dbo.SinhVien sv WITH (HOLDLOCK)
        INNER JOIN dbo.TaiKhoan tk WITH (HOLDLOCK) ON tk.TenDangNhap = sv.MaSV
        INNER JOIN dbo.ChiTietLopHP ct WITH (HOLDLOCK) ON ct.MaSV = sv.MaSV
        WHERE sv.MaSV = @MaSV AND ct.MaLopHP = @MaLopHP
          AND tk.VaiTro = N'SinhVien' AND tk.TrangThai = N'Hoạt động';

        IF @NgaySinh IS NULL
            THROW 50006, N'Tài khoản không còn hoạt động hoặc bạn không thuộc lớp học phần này.', 1;

        -- Không ghi đè kết quả có sẵn, kể cả Vắng/Nghỉ có phép do giảng viên đặt.
        IF EXISTS (SELECT 1 FROM dbo.DiemDanh WITH (UPDLOCK, HOLDLOCK)
                   WHERE MaBuoiHoc = @MaBuoiHoc AND MaSV = @MaSV)
            THROW 50007, N'Bạn đã có kết quả điểm danh trong buổi này. Hãy làm mới danh sách.', 1;

        -- Lấy giờ SAU khi nhận đủ khóa, không dùng giờ trên máy sinh viên.
        DECLARE @HienTai datetime = GETDATE();
        IF @TrangThaiBuoi <> N'Mo' OR @BatDau > @HienTai
           OR DATEADD(MINUTE, @GioiHanPhut, @BatDau) <= @HienTai
            THROW 50004, N'Buổi điểm danh đã đóng, chưa bắt đầu hoặc đã hết hạn.', 1;

        -- Xác thực lại trong SQL để việc sửa hồ sơ giữa hai màn hình không bị bỏ qua.
        DECLARE @XacThucDung bit = 0;
        IF @LoaiCauHoi = 'NgaySinh'
        BEGIN
            DECLARE @NgayTraLoi date = TRY_CONVERT(date, @TraLoi, 103);
            IF @NgayTraLoi = @NgaySinh AND DATALENGTH(@TraLoi) = 20
               AND CONVERT(nvarchar(10), @NgayTraLoi, 103) COLLATE Latin1_General_100_BIN2
                   = @TraLoi COLLATE Latin1_General_100_BIN2
                SET @XacThucDung = 1;
        END
        ELSE IF @LoaiCauHoi = 'CCCD'
        BEGIN
            IF DATALENGTH(@TraLoi) = 8
               AND @TraLoi COLLATE Latin1_General_100_BIN2 NOT LIKE N'%[^0-9]%'
               AND @TraLoi COLLATE Latin1_General_100_BIN2 = RIGHT(@SoCCCD, 4) COLLATE Latin1_General_100_BIN2
                SET @XacThucDung = 1;
        END;
        IF @XacThucDung = 0
            THROW 50008, N'Thông tin xác thực không đúng hoặc đã thay đổi. Hãy xác thực lại.', 1;

        -- So sánh đúng mã và đúng độ dài, không chấp nhận khoảng trắng thừa.
        IF @MaOTP IS NULL OR DATALENGTH(@MaOTP) = 0
           OR DATALENGTH(@MaOTP) <> DATALENGTH(@OTPDung)
           OR @MaOTP COLLATE Latin1_General_100_BIN2 <> @OTPDung COLLATE Latin1_General_100_BIN2
            THROW 50009, N'Mã OTP không đúng hoặc đã được thay đổi. Hãy nhập mã hiện tại của giảng viên.', 1;

        -- Chế độ test chỉ bỏ qua bước mạng này, không bỏ bất kỳ kiểm tra nào phía trên.
        IF @BoQuaKiemTraIP = 0
        BEGIN
            DECLARE @SoIPSV bigint = dbo.fn_IPv4ThanhSo(@IP_SV);
            DECLARE @SoIPGV bigint = dbo.fn_IPv4ThanhSo(@IP_GV);
            DECLARE @SoMask bigint = dbo.fn_IPv4ThanhSo(@Mask_GV);
            IF @SoIPSV IS NULL OR @SoIPGV IS NULL OR @SoMask IS NULL OR @SoMask = 0
                THROW 50010, N'IP hoặc subnet mask chưa hợp lệ. Hãy kiểm tra lại mạng của sinh viên và giảng viên.', 1;

            DECLARE @MaskDao bigint = CAST(4294967295 AS bigint) - @SoMask;
            IF (@MaskDao & (@MaskDao + 1)) <> 0
                THROW 50010, N'Subnet mask của giảng viên không hợp lệ.', 1;

            -- Bỏ loopback, link-local, địa chỉ 0.x.x.x và multicast/reserved.
            IF @SoIPSV / 16777216 NOT BETWEEN 1 AND 223
               OR @SoIPGV / 16777216 NOT BETWEEN 1 AND 223
               OR @SoIPSV / 16777216 = 127 OR @SoIPGV / 16777216 = 127
               OR @SoIPSV / 65536 = (169 * 256 + 254)
               OR @SoIPGV / 65536 = (169 * 256 + 254)
                THROW 50010, N'Phải sử dụng địa chỉ IPv4 của mạng Wi-Fi hoặc Ethernet đang kết nối.', 1;

            IF (@SoIPSV & @SoMask) <> (@SoIPGV & @SoMask)
                THROW 50011, N'Bạn chưa kết nối cùng mạng với giảng viên.', 1;
        END;

        DECLARE @KetQua nvarchar(20);
        IF @HienTai <= DATEADD(MINUTE, @DemNguoc, @BatDau)
            SET @KetQua = N'Có mặt';
        ELSE
            SET @KetQua = N'Đi muộn';

        DECLARE @GhiChu nvarchar(255) = NULL;
        IF @BoQuaKiemTraIP = 1
            SET @GhiChu = N'Chế độ test: bỏ qua kiểm tra cùng mạng.';

        INSERT INTO dbo.DiemDanh (MaBuoiHoc, MaSV, ThoiGianQuet, IP_SV, TrangThai, GhiChu)
        VALUES (@MaBuoiHoc, @MaSV, @HienTai, @IP_SV, @KetQua, @GhiChu);

        IF @TuMoGiaoDich = 1
            COMMIT TRANSACTION;
        SELECT @KetQua AS TrangThai;
    END TRY
    BEGIN CATCH
        IF @TuMoGiaoDich = 1 AND XACT_STATE() <> 0
            ROLLBACK TRANSACTION;
        ELSE IF @TuMoGiaoDich = 0 AND XACT_STATE() = 1
            ROLLBACK TRANSACTION GhiNhanDiemDanh;
        -- Nếu giao dịch của bên gọi đã lỗi (XACT_STATE = -1), bên gọi phải ROLLBACK.
        IF ERROR_NUMBER() IN (2601, 2627)
            THROW 50007, N'Bạn đã có kết quả điểm danh trong buổi này. Hãy làm mới danh sách.', 1;
        THROW;
    END CATCH;
END;
GO
