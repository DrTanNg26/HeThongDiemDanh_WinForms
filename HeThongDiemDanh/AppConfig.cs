using System.Configuration;

namespace HeThongDiemDanh
{
    /// <summary>
    /// Đọc các hằng số cấu hình từ App.config (mục appSettings).
    /// Các thành viên khác dùng class này thay vì tự gọi ConfigurationManager.
    /// </summary>
    public static class AppConfig
    {
        /// <summary>
        /// true = bỏ qua kiểm tra IP Wi-Fi khi test trên 1 máy.
        /// Đổi thành false trong App.config trước khi demo thật.
        /// </summary>
        public static bool TestMode
            => ConfigurationManager.AppSettings["TestMode"] == "true";

        /// <summary>Số phút đếm ngược mặc định khi mở phiên điểm danh (mặc định 10).</summary>
        public static int ThoiGianDemNguoc
            => int.Parse(ConfigurationManager.AppSettings["ThoiGianDemNguoc"] ?? "10");

        /// <summary>Số phút tối đa một phiên được giữ mở, sau đó tự coi là đóng (mặc định 240 = 4 tiếng).</summary>
        public static int GioiHanTuDongDongPhien
            => int.Parse(ConfigurationManager.AppSettings["GioiHanTuDongDongPhien"] ?? "240");

        /// <summary>Ngưỡng % chuyên cần hiển thị cảnh báo nguy cơ cấm thi (mặc định 80%).</summary>
        public static double NguongCanhBaoCamThi
            => double.Parse(ConfigurationManager.AppSettings["NguongCanhBaoCamThi"] ?? "80");

        /// <summary>Ngưỡng % chuyên cần bị cấm thi và nhận 0 điểm (mặc định 50%).</summary>
        public static double NguongCamThi
            => double.Parse(ConfigurationManager.AppSettings["NguongCamThi"] ?? "50");
    }
}