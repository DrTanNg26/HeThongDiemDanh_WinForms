using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    public partial class FrmQuenMK : Form
    {
        public FrmQuenMK()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Xác nhận thông tin và cấp lại mật khẩu
            string tk = tb_NhapTK.Text.Trim();
            DateTime ngaySinh = ngaysinh_dtp.Value.Date;
            string cccd = cccd_tb.Text.Trim();
            string email = email_tb.Text.Trim();

            infoTK_lb.Items.Clear();

            if (string.IsNullOrEmpty(tk))
            {
                MessageBox.Show("Vui lòng nhập tài khoản / Mã sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb_NhapTK.Focus();
                return;
            }

            if (string.IsNullOrEmpty(cccd))
            {
                MessageBox.Show("Vui lòng nhập số CCCD!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cccd_tb.Focus();
                return;
            }

            try
            {
                // Truy vấn thông tin tài khoản và sinh viên
                string sql = @"SELECT tk.TenDangNhap, tk.VaiTro, tk.TrangThai, tk.MatKhau,
                                      sv.HoTen, sv.Email, sv.NgaySinh, sv.SoCCCD, sv.MaLop, sv.MaKhoa
                               FROM TaiKhoan tk
                               INNER JOIN SinhVien sv ON sv.MaSV = tk.TenDangNhap
                               WHERE tk.TenDangNhap = @tk";

                var dt = DatabaseConnection.ExecuteQuery(sql, new[]
                {
                    new Microsoft.Data.SqlClient.SqlParameter("@tk", tk)
                });

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin sinh viên cho tài khoản này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var row = dt.Rows[0];

                // Kiểm tra trạng thái tài khoản
                string trangThai = row["TrangThai"]?.ToString() ?? "";
                if (trangThai == "Bị khóa")
                {
                    MessageBox.Show("Tài khoản đang bị khóa. Vui lòng liên hệ Admin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra thông tin xác thực: Ngày sinh và CCCD
                DateTime dbNgaySinh = DateTime.MinValue;
                if (row["NgaySinh"] != DBNull.Value)
                {
                    dbNgaySinh = Convert.ToDateTime(row["NgaySinh"]).Date;
                }

                string dbCCCD = row["SoCCCD"]?.ToString()?.Trim() ?? "";
                string dbEmail = row["Email"]?.ToString()?.Trim() ?? "";

                // So khớp ngày sinh và CCCD
                if (dbNgaySinh != DateTime.MinValue && dbNgaySinh != ngaySinh)
                {
                    MessageBox.Show("Ngày sinh không trùng khớp với hồ sơ sinh viên!", "Xác thực thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!string.Equals(dbCCCD, cccd, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Số CCCD không trùng khớp với hồ sơ sinh viên!", "Xác thực thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Nếu có nhập email thì kiểm tra thêm email (nếu hồ sơ có lưu)
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(dbEmail))
                {
                    if (!string.Equals(dbEmail, email, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Email không trùng khớp với hồ sơ sinh viên!", "Xác thực thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Xác thực thành công: Đổi trạng thái tài khoản sang "Chờ" để Admin duyệt
                TaiKhoanDAL.ThayDoiTrangThai(tk, "Chờ");

                // Hiển thị thông tin lên ListBox
                string hoTen = row["HoTen"]?.ToString() ?? "";
                string maLop = row["MaLop"]?.ToString() ?? "";

                infoTK_lb.Items.Add($"=== XÁC THỰC THÀNH CÔNG ===");
                infoTK_lb.Items.Add($"Tài khoản / Mã SV: {tk}");
                infoTK_lb.Items.Add($"Họ và tên: {hoTen} | Lớp: {maLop}");
                infoTK_lb.Items.Add($"Trạng thái hiện tại: CHỜ DUYỆT TỪ ADMIN");
                infoTK_lb.Items.Add($"Sau khi Admin duyệt, tài khoản sẽ 'Hoạt động'");
                infoTK_lb.Items.Add($"và mật khẩu sẽ tự động đổi về mặc định: 123456.");

                MessageBox.Show($"Xác thực danh tính thành công!\n\nYêu cầu đặt lại mật khẩu cho tài khoản '{tk}' đã được chuyển sang trạng thái 'Chờ' duyệt từ Admin.\nKhi Admin duyệt tài khoản, mật khẩu sẽ tự động được đặt lại về mặc định (123456) và tài khoản sẽ hoạt động trở lại.", "Đã gửi yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xử lý quên mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
