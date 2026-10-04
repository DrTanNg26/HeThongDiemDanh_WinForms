using System;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    public partial class FrmThemSuaTaiKhoan : Form
    {
        public bool IsEdit { get; private set; }

        public FrmThemSuaTaiKhoan(bool isEdit = false)
        {
            InitializeComponent();
            IsEdit = isEdit;
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
                // ignore load errors here
            }
        }

        private void cmbVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sel = cmbVaiTro.SelectedItem?.ToString();
            pnlSinhVien.Visible = sel == "SinhVien";
            pnlGiangVien.Visible = sel == "GiangVien";
            pnlAdmin.Visible = sel == "Admin";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
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

                    if (string.IsNullOrEmpty(ma)) { MessageBox.Show("Mã SV không được để trống"); return; }
                    if (TaiKhoanService.AccountExists(ma)) { MessageBox.Show("Mã đã tồn tại"); return; }
                    if (string.IsNullOrEmpty(ten)) { MessageBox.Show("Họ tên không được để trống"); return; }
                    if (string.IsNullOrEmpty(cccd) || cccd.Length < 12) { MessageBox.Show("Số CCCD phải đủ 12 số"); return; }
                    if (string.IsNullOrEmpty(malop)) { MessageBox.Show("Phải chọn Lớp hành chính"); return; }

                    // default password
                    TaiKhoanService.AddStudentAccount(ma, ten, string.IsNullOrEmpty(email) ? null : email, ns, cccd, malop, makhoa, "123456");
                    MessageBox.Show("Thêm Sinh viên thành công.");
                }
                else if (role == "GiangVien")
                {
                    string ma = txtGVMa.Text.Trim();
                    string ten = txtGVHoTen.Text.Trim();
                    string email = txtGVEmail.Text.Trim();
                    string hhhv = txtGVHocHam.Text.Trim();
                    string makhoa = cboGVMaKhoa.SelectedValue?.ToString();

                    if (string.IsNullOrEmpty(ma)) { MessageBox.Show("Mã GV không được để trống"); return; }
                    if (TaiKhoanService.AccountExists(ma)) { MessageBox.Show("Mã đã tồn tại"); return; }
                    if (string.IsNullOrEmpty(ten)) { MessageBox.Show("Họ tên không được để trống"); return; }
                    if (string.IsNullOrEmpty(makhoa)) { MessageBox.Show("Phải chọn Khoa"); return; }

                    TaiKhoanService.AddTeacherAccount(ma, ten, string.IsNullOrEmpty(email) ? null : email, hhhv, makhoa, "123456");
                    MessageBox.Show("Thêm Giảng viên thành công.");
                }
                else if (role == "Admin")
                {
                    string ma = txtADMa.Text.Trim();
                    string ten = txtADHoTen.Text.Trim();
                    string email = txtADEmail.Text.Trim();

                    if (string.IsNullOrEmpty(ma)) { MessageBox.Show("Mã Admin không được để trống"); return; }
                    if (TaiKhoanService.AccountExists(ma)) { MessageBox.Show("Mã đã tồn tại"); return; }
                    if (string.IsNullOrEmpty(ten)) { MessageBox.Show("Họ tên không được để trống"); return; }

                    TaiKhoanService.AddAdminAccount(ma, ten, string.IsNullOrEmpty(email) ? null : email, "123456");
                    MessageBox.Show("Thêm Admin thành công.");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu tài khoản: " + ex.Message);
            }
        }
    }
}
