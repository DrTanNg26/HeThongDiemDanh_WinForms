using System;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    public partial class FrmThemSuaTaiKhoan : Form
    {
        public bool IsEdit { get; private set; }
        public string TenDangNhapEdit { get; private set; }

        public FrmThemSuaTaiKhoan(bool isEdit = false, string tenDangNhap = null)
        {
            InitializeComponent();
            IsEdit = isEdit;
            TenDangNhapEdit = tenDangNhap;
            LoadCombos();

            if (IsEdit && !string.IsNullOrEmpty(TenDangNhapEdit))
            {
                this.Text = "Sửa tài khoản";
                btnSave.Text = "Cập nhật";
                cmbVaiTro.Enabled = false;
                LoadDataForEdit();
            }
            else
            {
                this.Text = "Thêm tài khoản mới";
                btnSave.Text = "Lưu";
                cmbVaiTro.SelectedIndex = 0;
                cmbVaiTro_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        private void LoadDataForEdit()
        {
            try
            {
                var rowTK = TaiKhoanDAL.LayTheoTen(TenDangNhapEdit);
                if (rowTK == null) return;

                string role = rowTK["VaiTro"].ToString();
                cmbVaiTro.SelectedItem = role;
                cmbVaiTro_SelectedIndexChanged(this, EventArgs.Empty);

                if (role == "SinhVien")
                {
                    var rowSV = TaiKhoanDAL.LayChiTietSinhVien(TenDangNhapEdit);
                    if (rowSV != null)
                    {
                        txtSVMa.Text = rowSV["MaSV"].ToString();
                        txtSVMa.Enabled = false;
                        txtSVHoTen.Text = rowSV["HoTen"].ToString();
                        txtSVEmail.Text = rowSV["Email"] != DBNull.Value ? rowSV["Email"].ToString() : "";
                        if (rowSV["NgaySinh"] != DBNull.Value) dtpSVNgaySinh.Value = Convert.ToDateTime(rowSV["NgaySinh"]);
                        txtSVCCCD.Text = rowSV["SoCCCD"].ToString();
                        if (rowSV["MaLop"] != DBNull.Value) cboSVMaLop.SelectedValue = rowSV["MaLop"].ToString();
                        if (rowSV["MaKhoa"] != DBNull.Value) cboSVMaKhoa.SelectedValue = rowSV["MaKhoa"].ToString();
                    }
                }
                else if (role == "GiangVien")
                {
                    var rowGV = TaiKhoanDAL.LayChiTietGiangVien(TenDangNhapEdit);
                    if (rowGV != null)
                    {
                        txtGVMa.Text = rowGV["MaGV"].ToString();
                        txtGVMa.Enabled = false;
                        txtGVHoTen.Text = rowGV["HoTen"].ToString();
                        txtGVEmail.Text = rowGV["Email"] != DBNull.Value ? rowGV["Email"].ToString() : "";
                        txtGVHocHam.Text = rowGV["HocHamHocVi"] != DBNull.Value ? rowGV["HocHamHocVi"].ToString() : "";
                        if (rowGV["MaKhoa"] != DBNull.Value) cboGVMaKhoa.SelectedValue = rowGV["MaKhoa"].ToString();
                    }
                }
                else if (role == "Admin")
                {
                    var rowAD = TaiKhoanDAL.LayChiTietAdmin(TenDangNhapEdit);
                    if (rowAD != null)
                    {
                        txtADMa.Text = rowAD["MaAD"].ToString();
                        txtADMa.Enabled = false;
                        txtADHoTen.Text = rowAD["HoTen"].ToString();
                        txtADEmail.Text = rowAD["Email"] != DBNull.Value ? rowAD["Email"].ToString() : "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu tài khoản: " + ex.Message);
            }
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
                    if (string.IsNullOrEmpty(ten)) { MessageBox.Show("Họ tên không được để trống"); return; }
                    if (string.IsNullOrEmpty(cccd) || cccd.Length < 12) { MessageBox.Show("Số CCCD phải đủ 12 số"); return; }
                    if (string.IsNullOrEmpty(malop)) { MessageBox.Show("Phải chọn Lớp hành chính"); return; }

                    if (!IsEdit)
                    {
                        if (TaiKhoanService.AccountExists(ma)) { MessageBox.Show("Mã đã tồn tại"); return; }
                        TaiKhoanService.AddStudentAccount(ma, ten, string.IsNullOrEmpty(email) ? null : email, ns, cccd, malop, makhoa, "123456");
                        MessageBox.Show("Thêm Sinh viên thành công.");
                    }
                    else
                    {
                        TaiKhoanService.UpdateStudentAccount(ma, ten, string.IsNullOrEmpty(email) ? null : email, ns, cccd, malop, makhoa);
                        TaiKhoanDAL.ThayDoiTrangThai(ma, "Hoạt động");
                        MessageBox.Show("Duyệt và Cập nhật Sinh viên thành công! Trạng thái tài khoản đã đổi sang 'Hoạt động'.");
                    }
                }
                else if (role == "GiangVien")
                {
                    string ma = txtGVMa.Text.Trim();
                    string ten = txtGVHoTen.Text.Trim();
                    string email = txtGVEmail.Text.Trim();
                    string hhhv = txtGVHocHam.Text.Trim();
                    string makhoa = cboGVMaKhoa.SelectedValue?.ToString();

                    if (string.IsNullOrEmpty(ma)) { MessageBox.Show("Mã GV không được để trống"); return; }
                    if (string.IsNullOrEmpty(ten)) { MessageBox.Show("Họ tên không được để trống"); return; }
                    if (string.IsNullOrEmpty(makhoa)) { MessageBox.Show("Phải chọn Khoa"); return; }

                    if (!IsEdit)
                    {
                        if (TaiKhoanService.AccountExists(ma)) { MessageBox.Show("Mã đã tồn tại"); return; }
                        TaiKhoanService.AddTeacherAccount(ma, ten, string.IsNullOrEmpty(email) ? null : email, hhhv, makhoa, "123456");
                        MessageBox.Show("Thêm Giảng viên thành công.");
                    }
                    else
                    {
                        TaiKhoanService.UpdateTeacherAccount(ma, ten, string.IsNullOrEmpty(email) ? null : email, hhhv, makhoa);
                        TaiKhoanDAL.ThayDoiTrangThai(ma, "Hoạt động");
                        MessageBox.Show("Duyệt và Cập nhật Giảng viên thành công! Trạng thái tài khoản đã đổi sang 'Hoạt động'.");
                    }
                }
                else if (role == "Admin")
                {
                    string ma = txtADMa.Text.Trim();
                    string ten = txtADHoTen.Text.Trim();
                    string email = txtADEmail.Text.Trim();

                    if (string.IsNullOrEmpty(ma)) { MessageBox.Show("Mã Admin không được để trống"); return; }
                    if (string.IsNullOrEmpty(ten)) { MessageBox.Show("Họ tên không được để trống"); return; }

                    if (!IsEdit)
                    {
                        if (TaiKhoanService.AccountExists(ma)) { MessageBox.Show("Mã đã tồn tại"); return; }
                        TaiKhoanService.AddAdminAccount(ma, ten, string.IsNullOrEmpty(email) ? null : email, "123456");
                        MessageBox.Show("Thêm Admin thành công.");
                    }
                    else
                    {
                        TaiKhoanService.UpdateAdminAccount(ma, ten, string.IsNullOrEmpty(email) ? null : email);
                        TaiKhoanDAL.ThayDoiTrangThai(ma, "Hoạt động");
                        MessageBox.Show("Duyệt và Cập nhật Admin thành công! Trạng thái tài khoản đã đổi sang 'Hoạt động'.");
                    }
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
