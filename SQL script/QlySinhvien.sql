CREATE DATABASE QlyDiemDanh;
GO
USE QlyDiemDanh;
GO

-- =============================================
-- TẦNG 1: Bảng không phụ thuộc ai
-- =============================================
CREATE TABLE Khoa (
    MaKhoa   VARCHAR(10)   NOT NULL,
    TenKhoa  NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_Khoa PRIMARY KEY (MaKhoa)
);
GO

CREATE TABLE LopHanhChinh (
    MaLop   VARCHAR(10)  NOT NULL,
    TenLop  NVARCHAR(50) NOT NULL,
    MaKhoa  VARCHAR(10)  NOT NULL,
    CONSTRAINT PK_LopHanhChinh PRIMARY KEY (MaLop),
    CONSTRAINT FK_LopHC_Khoa   FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa)
);
GO

CREATE TABLE MonHoc (
    MaMon     VARCHAR(20)   NOT NULL,
    TenMon    NVARCHAR(100) NOT NULL,
    SoTinChi  INT           NOT NULL DEFAULT 3,
    CONSTRAINT PK_MonHoc    PRIMARY KEY (MaMon),
    CONSTRAINT CK_TC        CHECK (SoTinChi BETWEEN 1 AND 10)
);
GO

-- =============================================
-- TẦNG 2: TaiKhoan — bảng cha của Admin/GV/SV
-- =============================================

CREATE TABLE TaiKhoan (
    TenDangNhap  VARCHAR(10)  NOT NULL,  -- hạ từ 30 → 10
    MatKhau      VARCHAR(255) NOT NULL,
    VaiTro       NVARCHAR(15) NOT NULL,
    TrangThai    NVARCHAR(20) NOT NULL DEFAULT N'Hoạt động',
    CONSTRAINT PK_TaiKhoan     PRIMARY KEY (TenDangNhap),
    CONSTRAINT CK_TK_VaiTro    CHECK (VaiTro   IN (N'Admin', N'GiangVien', N'SinhVien')),
    CONSTRAINT CK_TK_TrangThai CHECK (TrangThai IN (N'Hoạt động', N'Bị khóa', N'Chờ'))
);
GO

-- =============================================
-- TẦNG 3: Admin, GiangVien, SinhVien
--          (đều FK vào TaiKhoan — phải tạo sau)
-- =============================================
CREATE TABLE Admin (
    MaAD   VARCHAR(10)  NOT NULL,
    HoTen  NVARCHAR(50) NOT NULL,
    Email  VARCHAR(100) NULL,
    CONSTRAINT PK_Admin    PRIMARY KEY (MaAD),
    CONSTRAINT FK_Admin_TK FOREIGN KEY (MaAD) REFERENCES TaiKhoan(TenDangNhap)
);
GO

CREATE TABLE GiangVien (
    MaGV        VARCHAR(10)  NOT NULL,
    HoTen       NVARCHAR(50) NOT NULL,
    Email       VARCHAR(100) NULL,
    HocHamHocVi NVARCHAR(30) NULL,
    MaKhoa      VARCHAR(10)  NOT NULL,
    CONSTRAINT PK_GiangVien  PRIMARY KEY (MaGV),
    CONSTRAINT FK_GV_TK      FOREIGN KEY (MaGV)   REFERENCES TaiKhoan(TenDangNhap),
    CONSTRAINT FK_GV_Khoa    FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa)
);
GO

CREATE TABLE SinhVien (
    MaSV     VARCHAR(10)  NOT NULL,
    HoTen    NVARCHAR(50) NOT NULL,
    Email    VARCHAR(100) NULL,
    NgaySinh DATE         NOT NULL,
    SoCCCD   VARCHAR(12)  NOT NULL,
    MaLop    VARCHAR(10)  NOT NULL,
    MaKhoa   VARCHAR(10)  NOT NULL,
    CONSTRAINT PK_SinhVien  PRIMARY KEY (MaSV),
    CONSTRAINT FK_SV_TK     FOREIGN KEY (MaSV)   REFERENCES TaiKhoan(TenDangNhap),
    CONSTRAINT FK_SV_Lop    FOREIGN KEY (MaLop)  REFERENCES LopHanhChinh(MaLop),
    CONSTRAINT FK_SV_Khoa   FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa)
);
GO

-- =============================================
-- TẦNG 4: LopHocPhan (FK vào MonHoc và GiangVien)
-- =============================================
CREATE TABLE LopHocPhan (
    MaLopHP    VARCHAR(20)   NOT NULL,
    TenLopHP   NVARCHAR(100) NOT NULL,
    MaMon      VARCHAR(20)   NOT NULL,
    MaGV       VARCHAR(10)   NOT NULL,
    PhongHoc   VARCHAR(20)   NULL,
    NamHoc     VARCHAR(20)   NOT NULL,
    TongSoBuoi INT           NOT NULL DEFAULT 0,
    CONSTRAINT PK_LopHocPhan PRIMARY KEY (MaLopHP),
    CONSTRAINT FK_LHP_Mon    FOREIGN KEY (MaMon) REFERENCES MonHoc(MaMon),
    CONSTRAINT FK_LHP_GV     FOREIGN KEY (MaGV)  REFERENCES GiangVien(MaGV)
);
GO

-- =============================================
-- TẦNG 5: ChiTietLopHP (FK vào LopHocPhan và SinhVien)
-- =============================================
CREATE TABLE ChiTietLopHP (
    MaLopHP  VARCHAR(20) NOT NULL,
    MaSV     VARCHAR(10) NOT NULL,
    CONSTRAINT PK_ChiTietLopHP PRIMARY KEY (MaLopHP, MaSV),
    CONSTRAINT FK_CT_LopHP     FOREIGN KEY (MaLopHP) REFERENCES LopHocPhan(MaLopHP),
    CONSTRAINT FK_CT_SV        FOREIGN KEY (MaSV)    REFERENCES SinhVien(MaSV)
);
GO

-- =============================================
-- TẦNG 6: BuoiHoc (FK vào LopHocPhan)
-- =============================================
CREATE TABLE BuoiHoc (
    MaBuoiHoc       INT          NOT NULL IDENTITY(1,1),
    MaLopHP         VARCHAR(20)  NOT NULL,
    TenBuoiHoc      NVARCHAR(50) NOT NULL,
    NgayHoc         DATE         NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    MaOTP           VARCHAR(10)  NOT NULL,
    IP_GV           VARCHAR(50)  NOT NULL,
    SubnetMask_GV   VARCHAR(50)  NULL,
    ThoiGianBatDau  DATETIME     NOT NULL DEFAULT GETDATE(),
    ThoiGianDemNguoc INT         NOT NULL DEFAULT 10,
    TrangThai       NVARCHAR(15) NOT NULL DEFAULT N'Mo',
    CONSTRAINT PK_BuoiHoc      PRIMARY KEY (MaBuoiHoc),
    CONSTRAINT FK_BH_LopHP     FOREIGN KEY (MaLopHP) REFERENCES LopHocPhan(MaLopHP),
    CONSTRAINT CK_BH_TrangThai CHECK (TrangThai IN (N'Mo', N'DaDong'))
);
GO

-- =============================================
-- TẦNG 7: DiemDanh (FK vào BuoiHoc và SinhVien)
-- =============================================
CREATE TABLE DiemDanh (
    MaDiemDanh   BIGINT        NOT NULL IDENTITY(1,1),
    MaBuoiHoc    INT           NOT NULL,
    MaSV         VARCHAR(10)   NOT NULL,
    ThoiGianQuet DATETIME      NULL,
    IP_SV        VARCHAR(50)   NULL,
    TrangThai    NVARCHAR(20)  NOT NULL,
    GhiChu       NVARCHAR(255) NULL,
    NguoiSua     VARCHAR(10)   NULL,
    ThoiGianSua  DATETIME      NULL,
    CONSTRAINT PK_DiemDanh        PRIMARY KEY (MaDiemDanh),
    CONSTRAINT FK_DD_BuoiHoc      FOREIGN KEY (MaBuoiHoc) REFERENCES BuoiHoc(MaBuoiHoc),
    CONSTRAINT FK_DD_SV           FOREIGN KEY (MaSV)      REFERENCES SinhVien(MaSV),
    CONSTRAINT UQ_DD_BuoiHoc_SV   UNIQUE (MaBuoiHoc, MaSV),
    CONSTRAINT CK_DD_TrangThai    CHECK (TrangThai IN (
        N'Có mặt', N'Đi muộn', N'Vắng', N'Nghỉ có phép'))
);
GO

-- =============================================
-- TẦNG 8: KetQuaChuyenCan
-- =============================================
CREATE TABLE KetQuaChuyenCan (
    MaLopHP        VARCHAR(20) NOT NULL,
    MaSV           VARCHAR(10) NOT NULL,
    TongSoBuoi     INT         NOT NULL DEFAULT 0,
    SoBuoiCoMat    INT         NOT NULL DEFAULT 0,
    SoBuoiDiMuon   INT         NOT NULL DEFAULT 0,
    SoBuoiVang     INT         NOT NULL DEFAULT 0,
    SoBuoiNghiPhep INT         NOT NULL DEFAULT 0,
    TyLeChuyenCan  FLOAT       NOT NULL DEFAULT 0,
    DiemChuyenCan  FLOAT       NOT NULL DEFAULT 0,
    BiCamThi       BIT         NOT NULL DEFAULT 0,
    NgayTongKet    DATETIME    NULL,
    CONSTRAINT PK_KetQua   PRIMARY KEY (MaLopHP, MaSV),
    CONSTRAINT FK_KQ_LopHP FOREIGN KEY (MaLopHP) REFERENCES LopHocPhan(MaLopHP),
    CONSTRAINT FK_KQ_SV    FOREIGN KEY (MaSV)    REFERENCES SinhVien(MaSV)
);
GO

PRINT N'Tạo tất cả bảng thành công!';
GO