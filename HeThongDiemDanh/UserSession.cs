using System;

namespace HeThongDiemDanh
{
    /// <summary>
    /// Lưu thông tin người dùng đang đăng nhập.
    /// Gán sau khi xác thực thành công trong FrmLogin.
    /// Gọi Clear() khi đăng xuất.
    /// </summary>
    internal static class UserSession
    {
        // -------------------------------------------------------
        // Thông tin chung — mọi vai trò đều có
        // -------------------------------------------------------

        /// <summary>Tên đăng nhập = MaSV / MaGV / MaAD</summary>
        public static string TenDangNhap { get; set; }

        public static string HoTen { get; set; }
        public static string Email { get; set; }

        /// <summary>"SinhVien" | "GiangVien" | "Admin"</summary>
        public static string VaiTro { get; set; }

        public static string MaKhoa { get; set; }

        // -------------------------------------------------------
        // Chỉ dùng cho Sinh viên
        // -------------------------------------------------------

        /// <summary>
        /// Dùng cho xác thực bước 2 (câu hỏi ngày sinh).
        /// Lưu dạng "dd/MM/yyyy" để hiển thị, so sánh theo DATE trong SQL.
        /// </summary>
        public static string NgaySinh { get; set; }

        /// <summary>
        /// Dùng cho xác thực bước 2 (4 số cuối CCCD).
        /// Chỉ lưu 4 số cuối, không lưu toàn bộ số CCCD.
        /// </summary>
        public static string BonSoCuoiCCCD { get; set; }

        public static string MaLopHanhChinh { get; set; }

        // -------------------------------------------------------
        // Chỉ dùng cho Giảng viên
        // -------------------------------------------------------

        public static string HocHamHocVi { get; set; }

        // -------------------------------------------------------
        // Kiểm tra nhanh vai trò (dùng trong code để tránh so chuỗi)
        // -------------------------------------------------------

        public static bool LaSinhVien => VaiTro == "SinhVien";
        public static bool LaGiangVien => VaiTro == "GiangVien";
        public static bool LaAdmin => VaiTro == "Admin";

        public static bool DaDangNhap => !string.IsNullOrEmpty(TenDangNhap);

        // -------------------------------------------------------
        // Xóa phiên khi đăng xuất
        // -------------------------------------------------------
        public static void Clear()
        {
            TenDangNhap = null;
            HoTen = null;
            Email = null;
            VaiTro = null;
            MaKhoa = null;
            NgaySinh = null;
            BonSoCuoiCCCD = null;
            MaLopHanhChinh = null;
            HocHamHocVi = null;
        }
    }
}