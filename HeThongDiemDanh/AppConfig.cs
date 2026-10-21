using System.Configuration;

namespace HeThongDiemDanh
{
    // ============================================================
    // THÀNH VIÊN 4 – Đọc cấu hình từ App.config (mục appSettings).
    // Các thành viên khác dùng class này, không tự gọi ConfigurationManager.
    // ============================================================
    public static class AppConfig
    {
        // Thời gian điểm danh
        /// <summary>Số phút đếm ngược mặc định khi mở phiên điểm danh.</summary>
        public static int ThoiGianDemNguoc
            => int.Parse(ConfigurationManager.AppSettings["ThoiGianDemNguoc"] ?? "10");

        /// <summary>Số phút tối đa giữ phiên mở, sau đó tự đóng (mặc định 240 = 4 tiếng).</summary>
        public static int GioiHanTuDongDongPhien
            => int.Parse(ConfigurationManager.AppSettings["GioiHanTuDongDongPhien"] ?? "240");

        // Ngưỡng chuyên cần (%)
        /// <summary>Ngưỡng 90%: ≥ giá trị này → 10 điểm.</summary>
        public static double Nguong90
            => double.Parse(ConfigurationManager.AppSettings["Nguong90"] ?? "90");

        /// <summary>Ngưỡng 80%: ≥ giá trị này → 9 điểm; dưới ngưỡng này → cảnh báo nguy cơ cấm thi.</summary>
        public static double Nguong80
            => double.Parse(ConfigurationManager.AppSettings["Nguong80"] ?? "80");

        /// <summary>Ngưỡng 70%: ≥ giá trị này → 8 điểm.</summary>
        public static double Nguong70
            => double.Parse(ConfigurationManager.AppSettings["Nguong70"] ?? "70");

        /// <summary>Ngưỡng cảnh báo nguy cơ cấm thi (mặc định = Nguong80 = 80%).</summary>
        public static double NguongCanhBaoCamThi
            => double.Parse(ConfigurationManager.AppSettings["NguongCanhBaoCamThi"] ?? "80");

        /// <summary>Ngưỡng cấm thi: dưới giá trị này → 0 điểm, cấm thi (mặc định 50%).</summary>
        public static double NguongCamThi
            => double.Parse(ConfigurationManager.AppSettings["NguongCamThi"] ?? "50");
    }
}