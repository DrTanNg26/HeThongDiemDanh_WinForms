using System;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    public partial class FrmDangKy : Form
    {
        public FrmDangKy()
        {
            InitializeComponent();
            cmbVaiTro.SelectedIndex = 0;
            cmbVaiTro_SelectedIndexChanged(this, EventArgs.Empty);
            LoadCombos();
        }

        private void LoadCombos()
        {
            try
            {
                var dtKhoa = KhoaDAL.LayDanhSach();
                cboSVMaKhoa.DataSource = dtKhoa;
                cboSVMaKhoa.DisplayMember = "TenKhoa";
                cboSVMaKhoa.ValueMember = "MaKhoa";

                cboGVMaKhoa.DataSource = dtKhoa.Copy();
                cboGVMaKhoa.DisplayMember = "TenKhoa";
                cboGVMaKhoa.ValueMember = "MaKhoa";

                var dtLop = LopHanhChinhDAL.LayDanhSach();
                cboSVMaLop.DataSource = dtLop;
                cboSVMaLop.DisplayMember = "TenLop";
                cboSVMaLop.ValueMember = "MaLop";
            }
            catch
            {
                // ignore load errors
            }
        }

        private void cmbVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sel = cmbVaiTro.SelectedItem?.ToString();
            pnlSinhVien.Visible = sel == "SinhVien";
            pnlGiangVien.Visible = sel == "GiangVien";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var role = cmbVaiTro.SelectedItem?.ToString();
            try
            {
                if (role == "SinhVien")
                {
                    string ma = txtSVMa.Text.Trim();
                    string ten = txtSVHoTen.Text.Trim();
                    string email = txtSVEmail.Text.Trim();
                    DateTime ns = dtpSVNgaySinh.Value.Date;
                    string cccd = txtSVCCCD.Text.Trim();
                    string malop = cboSVMaLop.SelectedValue?.ToString();
                    string makhoa = cboSVMaKhoa.SelectedValue?.ToString();
                    string mk = txtPassword.Text.Trim();

                    if (string.IsNullOrEmpty(ma)) { MessageBox.Show("Mã Sinh viên không được để trống"); return; }
                    if (TaiKhoanService.AccountExists(ma)) { MessageBox.Show("Mã Sinh viên đã tồn tại trên hệ thống!"); return; }
                    if (string.IsNullOrEmpty(ten)) { MessageBox.Show("Họ tên không được để trống"); return; }
                    if (string.IsNullOrEmpty(mk)) { MessageBox.Show("Mật khẩu không được để trống"); return; }
                    if (string.IsNullOrEmpty(cccd) || cccd.Length < 12) { MessageBox.Show("Số CCCD phải đủ 12 số"); return; }
                    if (string.IsNullOrEmpty(malop)) { MessageBox.Show("Phải chọn Lớp hành chính"); return; }

                    // Đăng ký với trạng thái "Chờ"
                    TaiKhoanService.AddStudentAccount(ma, ten, string.IsNullOrEmpty(email) ? null : email, ns, cccd, malop, makhoa, mk, "Chờ");
                    MessageBox.Show("Đăng ký tài khoản Sinh viên thành công! Tài khoản của bạn đang ở trạng thái 'Chờ' duyệt từ Admin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (role == "GiangVien")
                {
                    string ma = txtGVMa.Text.Trim();
                    string ten = txtGVHoTen.Text.Trim();
                    string email = txtGVEmail.Text.Trim();
                    string hhhv = txtGVHocHam.Text.Trim();
                    string makhoa = cboGVMaKhoa.SelectedValue?.ToString();
                    string mk = txtPassword.Text.Trim();

                    if (string.IsNullOrEmpty(ma)) { MessageBox.Show("Mã Giảng viên không được để trống"); return; }
                    if (TaiKhoanService.AccountExists(ma)) { MessageBox.Show("Mã Giảng viên đã tồn tại trên hệ thống!"); return; }
                    if (string.IsNullOrEmpty(ten)) { MessageBox.Show("Họ tên không được để trống"); return; }
                    if (string.IsNullOrEmpty(mk)) { MessageBox.Show("Mật khẩu không được để trống"); return; }
                    if (string.IsNullOrEmpty(makhoa)) { MessageBox.Show("Phải chọn Khoa"); return; }

                    // Đăng ký với trạng thái "Chờ"
                    TaiKhoanService.AddTeacherAccount(ma, ten, string.IsNullOrEmpty(email) ? null : email, hhhv, makhoa, mk, "Chờ");
                    MessageBox.Show("Đăng ký tài khoản Giảng viên thành công! Tài khoản của bạn đang ở trạng thái 'Chờ' duyệt từ Admin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng ký tài khoản: " + ex.Message);
            }
        }
    }
}
