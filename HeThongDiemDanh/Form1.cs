namespace HeThongDiemDanh
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Đăng nhập
            string tk = TaiKhoan_tb.Text.Trim();
            string mk = MatKhau_tb.Text;

            if (string.IsNullOrEmpty(tk)) { MessageBox.Show("Vui lòng nhập tài khoản!"); return; }
            if (string.IsNullOrEmpty(mk)) { MessageBox.Show("Vui lòng nhập mật khẩu!"); return; }

            string sql = @"SELECT tk.TenDangNhap, tk.VaiTro, tk.TrangThai, tk.MatKhau,
                          COALESCE(sv.HoTen, gv.HoTen, ad.HoTen) AS HoTen,
                          COALESCE(sv.Email, gv.Email, ad.Email)  AS Email,
                          sv.NgaySinh, sv.SoCCCD, sv.MaLop, sv.MaKhoa AS MaKhoaSV,
                          gv.MaKhoa AS MaKhoaGV, gv.HocHamHocVi
                   FROM TaiKhoan tk
                   LEFT JOIN SinhVien sv ON sv.MaSV = tk.TenDangNhap
                   LEFT JOIN GiangVien gv ON gv.MaGV = tk.TenDangNhap
                   LEFT JOIN Admin ad ON ad.MaAD = tk.TenDangNhap
                   WHERE tk.TenDangNhap = @tk";

            System.Data.DataTable dt;
            try
            {
                dt = DatabaseConnection.ExecuteQuery(sql, new[]
                {
                    new Microsoft.Data.SqlClient.SqlParameter("@tk", tk)
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kết nối cơ sở dữ liệu: " + ex.Message);
                return;
            }

            if (dt.Rows.Count == 0)
            { MessageBox.Show("Tài khoản không tồn tại!"); return; }

            var row = dt.Rows[0];

            string tt = row["TrangThai"] != System.DBNull.Value ? row["TrangThai"].ToString() : "";
            if (tt == "Bị khóa")
            { MessageBox.Show("Tài khoản đang bị khóa. Vui lòng liên hệ Admin!"); return; }
            if (tt == "Chờ")
            { MessageBox.Show("Tài khoản của bạn đang ở trạng thái 'Chờ' duyệt từ Admin. Vui lòng quay lại sau!"); return; }
            if (tt != "Hoạt động")
            { MessageBox.Show("Tài khoản không ở trạng thái hoạt động!"); return; }

            // So sánh mật khẩu (Hỗ trợ cả SHA256 và Plaintext tạm thời để tránh làm vỡ dữ liệu mẫu cũ)
            var storedPwd = row.Table.Columns.Contains("MatKhau") && row["MatKhau"] != System.DBNull.Value
                ? row["MatKhau"].ToString() : string.Empty;

            bool isMatch = PasswordHelper.KiemTra(mk, storedPwd) || (mk == storedPwd);

            if (!isMatch)
            { MessageBox.Show("Mật khẩu không chính xác!"); return; }

            // Gán UserSession
            UserSession.TenDangNhap = row.Table.Columns.Contains("TenDangNhap") ? row["TenDangNhap"].ToString() : tk;
            UserSession.HoTen = row.Table.Columns.Contains("HoTen") ? row["HoTen"].ToString() : string.Empty;
            UserSession.VaiTro = row.Table.Columns.Contains("VaiTro") ? row["VaiTro"].ToString() : string.Empty;
            UserSession.Email = row.Table.Columns.Contains("Email") ? row["Email"].ToString() : string.Empty;

            // Nếu là sinh viên, lấy thêm thông tin an toàn
            if (UserSession.LaSinhVien)
            {
                if (row.Table.Columns.Contains("NgaySinh") && row["NgaySinh"] != System.DBNull.Value)
                    UserSession.NgaySinh = Convert.ToDateTime(row["NgaySinh"]).ToString("dd/MM/yyyy");

                if (row.Table.Columns.Contains("SoCCCD") && row["SoCCCD"] != System.DBNull.Value)
                {
                    var so = row["SoCCCD"].ToString();
                    UserSession.BonSoCuoiCCCD = so.Length >= 4 ? so[^4..] : so;
                }

                UserSession.MaLopHanhChinh = row.Table.Columns.Contains("MaLop") ? row["MaLop"].ToString() : string.Empty;
                UserSession.MaKhoa = row.Table.Columns.Contains("MaKhoaSV") ? row["MaKhoaSV"].ToString() : string.Empty;
            }
            else if (UserSession.LaGiangVien)
            {
                UserSession.MaKhoa = row.Table.Columns.Contains("MaKhoaGV") ? row["MaKhoaGV"].ToString() : string.Empty;
                UserSession.HocHamHocVi = row.Table.Columns.Contains("HocHamHocVi") ? row["HocHamHocVi"].ToString() : string.Empty;
            }

            // Đăng nhập thành công
            MessageBox.Show("Đăng nhập thành công! Vai trò: " + UserSession.VaiTro);

            if (UserSession.LaAdmin)
            {
                // Open admin main form
                var adminForm = new FrmAdmin();
                adminForm.Show();
                this.Hide();
                return;
            }

            // Non-admin: hide login for now (future: open student/lecturer forms)
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void DangKi_bt_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmDangKy())
            {
                frm.ShowDialog();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void MatKhau_tb_TextChanged(object sender, EventArgs e)
        {

        }

        private void QuenMatKhau_llb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Exit application
            Application.Exit();
        }
    }
}
