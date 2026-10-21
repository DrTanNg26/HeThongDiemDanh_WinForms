using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh
{
    // Form cấu hình kết nối SQL Server — tự động hiện khi không kết nối được
    // Người dùng chỉnh Server / Database rồi nhấn "Kiểm tra & Lưu"
    public class FrmCauHinhKetNoi : Form
    {
        private TextBox txtServer, txtDatabase, txtUser, txtPass;
        private Label   lblLoi;
        private Button  btnKiemTra, btnThoat;

        public FrmCauHinhKetNoi(string loiBanDau = null)
        {
            Text            = "Cấu hình kết nối SQL Server";
            Size            = new Size(480, 320);
            StartPosition   = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox     = false;
            Font            = new Font("Segoe UI", 9.5f);

            // Đọc connection string hiện tại để điền sẵn
            var cb = new SqlConnectionStringBuilder(DatabaseConnection.ConnectionString);

            int y = 16;
            txtServer   = Field("Tên Server (Data Source):", cb.DataSource,         ref y);
            txtDatabase = Field("Tên Database (Catalog):",   cb.InitialCatalog,      ref y);
            txtUser     = Field("Tài khoản SQL:",            cb.UserID,              ref y);
            txtPass     = Field("Mật khẩu:",                 cb.Password,            ref y, isPass: true);

            lblLoi = new Label
            {
                Text = loiBanDau != null ? "Lỗi: " + loiBanDau : "",
                ForeColor = Color.DarkRed, AutoSize = false, Height = 40,
                Location = new Point(12, y), Width = 440, Font = new Font("Segoe UI", 8.5f)
            };

            btnKiemTra = new Button { Text = "Kiểm tra & Lưu", Location = new Point(120, y + 48), Width = 150, Height = 32 };
            btnThoat   = new Button { Text = "Thoát",           Location = new Point(284, y + 48), Width = 80,  Height = 32 };
            btnKiemTra.Click += BtnKiemTra_Click;
            btnThoat.Click   += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.AddRange(new Control[] { lblLoi, btnKiemTra, btnThoat });
        }

        // Thử kết nối với thông tin mới; nếu OK → lưu vào runtime và đóng form (OK)
        private void BtnKiemTra_Click(object sender, EventArgs e)
        {
            string cs = new SqlConnectionStringBuilder
            {
                DataSource        = txtServer.Text.Trim(),
                InitialCatalog    = txtDatabase.Text.Trim(),
                UserID            = txtUser.Text.Trim(),
                Password          = txtPass.Text,
                Encrypt           = true,
                TrustServerCertificate = true
            }.ConnectionString;

            DatabaseConnection.ConnectionString = cs;

            if (DatabaseConnection.KiemTraKetNoi(out string err))
            {
                // Lưu vào App.config để lần sau không phải cấu hình lại
                try
                {
                    var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(
                        System.Configuration.ConfigurationUserLevel.None);
                    config.ConnectionStrings.ConnectionStrings["DefaultConnection"].ConnectionString = cs;
                    config.Save(System.Configuration.ConfigurationSaveMode.Modified);
                    System.Configuration.ConfigurationManager.RefreshSection("connectionStrings");
                }
                catch { /* Không lưu được App.config (không phải lỗi nghiêm trọng) */ }

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                lblLoi.Text = "Lỗi kết nối: " + err;
            }
        }

        // Helper: tạo label + textbox theo chiều dọc
        private TextBox Field(string label, string value, ref int y, bool isPass = false)
        {
            Controls.Add(new Label { Text = label, Location = new Point(12, y), AutoSize = true });
            y += 20;
            var tb = new TextBox { Text = value, Location = new Point(12, y), Width = 440,
                                   UseSystemPasswordChar = isPass };
            Controls.Add(tb);
            y += 34;
            return tb;
        }
    }
}
