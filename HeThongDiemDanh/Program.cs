using System.Windows.Forms;

namespace HeThongDiemDanh
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Kiểm tra kết nối SQL Server trước khi mở app
            // Nếu lỗi → mở form cấu hình để người dùng nhập server name đúng
            if (!DatabaseConnection.KiemTraKetNoi(out string err))
            {
                using var frm = new FrmCauHinhKetNoi(err);
                if (frm.ShowDialog() != DialogResult.OK)
                    return; // Người dùng hủy → thoát app
            }

            Application.Run(new Form1());
        }
    }
}
