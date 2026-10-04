using System;
using System.Collections.Generic;
using System.Text;

namespace HeThongDiemDanh
{
    internal static class UserSession
    {
        public static string TenDangNhap { get; set; }
        public static string HoTen { get; set; }
        public static string VaiTro { get; set; } // "SinhVien", "GiangVien", "Admin"
        public static string Email { get; set; }

        // Dành riêng cho câu hỏi xác thực bước 2 của Sinh viên
        public static string NgaySinh { get; set; }
        public static string SoCCCD { get; set; }
        public static string LopHanhChinh { get; set; }

        public static void Clear()
        {
            TenDangNhap = null;
            HoTen = null;
            VaiTro = null;
            Email = null;
            NgaySinh = null;
            SoCCCD = null;
            LopHanhChinh = null;
        }
    }
}

