using System;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
            // Set admin name from session
            lblAdminName.Text = string.IsNullOrEmpty(UserSession.HoTen) ? UserSession.TenDangNhap : UserSession.HoTen;
            // Hook load for initial data
            this.Load += FrmAdmin_Load;
        }

        private void FrmAdmin_Load(object? sender, EventArgs e)
        {
            // Load Khoa list immediately so admin sees data
            try
            {
                LoadKhoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Khoa: " + ex.Message);
            }
        }

        // MON HOC
        private void dgvMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvMonHoc.Rows[e.RowIndex];
            if (row.Cells["MaMon"].Value != null)
                txtMaMon.Text = row.Cells["MaMon"].Value.ToString();
            if (row.Cells["TenMon"].Value != null)
                txtTenMon.Text = row.Cells["TenMon"].Value.ToString();
            if (row.Cells["SoTinChi"].Value != null)
                txtSoTinChi.Text = row.Cells["SoTinChi"].Value.ToString();
            txtMaMon.Enabled = false;
        }

        private void btnMonThem_Click(object sender, EventArgs e)
        {
            txtMaMon.Text = string.Empty;
            txtTenMon.Text = string.Empty;
            txtSoTinChi.Text = string.Empty;
            txtMaMon.Enabled = true;
            txtMaMon.Focus();
        }

        private void btnMonLuu_Click(object sender, EventArgs e)
        {
            string ma = txtMaMon.Text.Trim();
            string ten = txtTenMon.Text.Trim();
            if (!int.TryParse(txtSoTinChi.Text.Trim(), out int soTin))
            {
                MessageBox.Show("Số tín chỉ phải là số nguyên"); return;
            }
            if (soTin < 1 || soTin > 10) { MessageBox.Show("Số tín chỉ phải trong khoảng 1-10"); return; }
            if (string.IsNullOrEmpty(ma)) { MessageBox.Show("Mã môn không được để trống"); return; }
            if (ma.Contains(" ")) { MessageBox.Show("Mã môn không được chứa khoảng trắng"); return; }
            if (string.IsNullOrEmpty(ten)) { MessageBox.Show("Tên môn không được để trống"); return; }

            try
            {
                bool exists = false;
                foreach (DataGridViewRow r in dgvMonHoc.Rows)
                {
                    if (r.Cells["MaMon"].Value != null && r.Cells["MaMon"].Value.ToString() == ma)
                    { exists = true; break; }
                }

                if (!exists)
                {
                    MonHocDAL.Them(ma, ten, soTin);
                    MessageBox.Show("Thêm Môn học thành công.");
                }
                else
                {
                    MonHocDAL.Sua(ma, ten, soTin);
                    MessageBox.Show("Cập nhật Môn học thành công.");
                }

                LoadMonHoc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu Môn học: " + ex.Message);
            }
        }

        private void btnMonXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaMon.Text.Trim();
            if (string.IsNullOrEmpty(ma)) { MessageBox.Show("Chọn Môn để xóa"); return; }

            try
            {
                if (MonHocDAL.HasDependencies(ma))
                {
                    MessageBox.Show("Không thể xóa Môn vì đã có Lớp học phần sử dụng môn này.");
                    return;
                }

                var confirm = MessageBox.Show($"Bạn có chắc muốn xóa Môn '{ma}' không?", "Xác nhận", MessageBoxButtons.YesNo);
                if (confirm != DialogResult.Yes) return;

                MonHocDAL.Xoa(ma);
                MessageBox.Show("Xóa thành công.");
                LoadMonHoc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa Môn: " + ex.Message);
            }
        }

        private void LoadMonHoc()
        {
            var dt = MonHocDAL.LayDanhSach();
            dgvMonHoc.DataSource = dt;
            if (dgvMonHoc.Columns.Contains("MaMon")) dgvMonHoc.Columns["MaMon"].HeaderText = "Mã môn";
            if (dgvMonHoc.Columns.Contains("TenMon")) dgvMonHoc.Columns["TenMon"].HeaderText = "Tên môn";
            if (dgvMonHoc.Columns.Contains("SoTinChi")) dgvMonHoc.Columns["SoTinChi"].HeaderText = "Số tín chỉ";
            txtMaMon.Text = string.Empty;
            txtTenMon.Text = string.Empty;
            txtSoTinChi.Text = string.Empty;
            txtMaMon.Enabled = true;
        }

        // TÀI KHOẢN
        private void LoadTaiKhoan()
        {
            string kw = txtTkSearch.Text.Trim();
            string role = cmbRoleFilter.SelectedItem?.ToString();
            string status = cmbStatusFilter.SelectedItem?.ToString();
            var dt = TaiKhoanDAL.LayDanhSach(string.IsNullOrEmpty(kw) ? null : kw,
                                            role == "Tất cả" ? null : role,
                                            status == "Tất cả" ? null : status);
            dgvTaiKhoan.DataSource = dt;
            // adjust headers
            if (dgvTaiKhoan.Columns.Contains("TenDangNhap")) dgvTaiKhoan.Columns["TenDangNhap"].HeaderText = "Mã";
            if (dgvTaiKhoan.Columns.Contains("HoTen")) dgvTaiKhoan.Columns["HoTen"].HeaderText = "Họ tên";
            if (dgvTaiKhoan.Columns.Contains("VaiTro")) dgvTaiKhoan.Columns["VaiTro"].HeaderText = "Vai trò";
            if (dgvTaiKhoan.Columns.Contains("TrangThai")) dgvTaiKhoan.Columns["TrangThai"].HeaderText = "Trạng thái";
            if (dgvTaiKhoan.Columns.Contains("Email")) dgvTaiKhoan.Columns["Email"].HeaderText = "Email";

            btnTaiKhoanEdit.Enabled = false;
            btnTaiKhoanLockUnlock.Enabled = false;
            btnTaiKhoanResetPwd.Enabled = false;
        }

        private void btnTkSearch_Click(object sender, EventArgs e)
        {
            LoadTaiKhoan();
        }

        private void dgvTaiKhoan_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count == 0)
            {
                btnTaiKhoanEdit.Enabled = false;
                btnTaiKhoanLockUnlock.Enabled = false;
                btnTaiKhoanResetPwd.Enabled = false;
                return;
            }

            var row = dgvTaiKhoan.SelectedRows[0];
            string ten = row.Cells["TenDangNhap"].Value?.ToString();
            string trangThai = row.Cells["TrangThai"].Value?.ToString();
            btnTaiKhoanEdit.Enabled = true;
            btnTaiKhoanLockUnlock.Enabled = true;
            btnTaiKhoanResetPwd.Enabled = true;

            btnTaiKhoanLockUnlock.Text = trangThai == "Bị khóa" ? "Mở khóa" : "Khóa";
            // disable lock button if selected is current admin
            if (ten == UserSession.TenDangNhap)
            {
                btnTaiKhoanLockUnlock.Enabled = false;
            }
        }

        private void btnTaiKhoanLockUnlock_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count == 0) return;
            var row = dgvTaiKhoan.SelectedRows[0];
            string ten = row.Cells["TenDangNhap"].Value?.ToString();
            string trangThai = row.Cells["TrangThai"].Value?.ToString();
            if (string.IsNullOrEmpty(ten)) return;

            if (ten == UserSession.TenDangNhap)
            {
                MessageBox.Show("Bạn không thể khóa chính tài khoản của mình.");
                return;
            }

            string newState = trangThai == "Bị khóa" ? "Hoạt động" : "Bị khóa";
            var confirm = MessageBox.Show($"Bạn có chắc muốn đổi trạng thái tài khoản '{ten}' thành '{newState}' không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            try
            {
                TaiKhoanDAL.ThayDoiTrangThai(ten, newState);
                MessageBox.Show("Cập nhật trạng thái thành công.");
                LoadTaiKhoan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái: " + ex.Message);
            }
        }

        private void btnTaiKhoanResetPwd_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count == 0) return;
            var row = dgvTaiKhoan.SelectedRows[0];
            string ten = row.Cells["TenDangNhap"].Value?.ToString();
            if (string.IsNullOrEmpty(ten)) return;

            var confirm = MessageBox.Show($"Bạn có chắc muốn đặt lại mật khẩu tài khoản '{ten}' về giá trị mặc định không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            try
            {
                string defaultPwdHash = PasswordHelper.BamSHA256("123456");
                TaiKhoanDAL.DatLaiMatKhau(ten, defaultPwdHash);
                MessageBox.Show("Mật khẩu đã được đặt lại về mặc định (123456).");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đặt lại mật khẩu: " + ex.Message);
            }
        }

        private void btnTaiKhoanAdd_Click(object sender, EventArgs e)
        {
            using (var f = new FrmThemSuaTaiKhoan(false))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadTaiKhoan();
                }
            }
        }

        private void btnTaiKhoanEdit_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count == 0) return;
            var row = dgvTaiKhoan.SelectedRows[0];
            string ten = row.Cells["TenDangNhap"].Value?.ToString();
            // For now open the same form in Edit mode (load data not implemented yet)
            using (var f = new FrmThemSuaTaiKhoan(true))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadTaiKhoan();
                }
            }
        }

        private void LoadKhoa()
        {
            var dt = KhoaDAL.LayDanhSach();
            dgvKhoa.DataSource = dt;
            // Adjust column headers if present
            if (dgvKhoa.Columns.Contains("MaKhoa")) dgvKhoa.Columns["MaKhoa"].HeaderText = "Mã khoa";
            if (dgvKhoa.Columns.Contains("TenKhoa")) dgvKhoa.Columns["TenKhoa"].HeaderText = "Tên khoa";
            ClearKhoaFields();
        }

        // Danh mục selector changed
        private void cmbDanhMucSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sel = cmbDanhMucSelector.SelectedItem?.ToString();
            if (sel == "Lớp hành chính")
            {
                // show lop controls
                dgvKhoa.Visible = false;
                dgvLopHanhChinh.Visible = true;

                // hide Khoa controls
                lblMaKhoa.Visible = false;
                txtMaKhoa.Visible = false;
                lblTenKhoa.Visible = false;
                txtTenKhoa.Visible = false;
                btnKhoaThem.Visible = false;
                btnKhoaLuu.Visible = false;
                btnKhoaXoa.Visible = false;

                // show Lop controls
                lblMaLop.Visible = true;
                txtMaLop.Visible = true;
                lblTenLop.Visible = true;
                txtTenLop.Visible = true;
                lblKhoaForLop.Visible = true;
                cboKhoaForLop.Visible = true;
                btnLopThem.Visible = true;
                btnLopLuu.Visible = true;
                btnLopXoa.Visible = true;

                LoadLopHanhChinh();
                LoadComboKhoaForLop();
            }
            else if (sel == "Khoa")
            {
                // show Khoa
                dgvKhoa.Visible = true;
                dgvLopHanhChinh.Visible = false;

                lblMaKhoa.Visible = true;
                txtMaKhoa.Visible = true;
                lblTenKhoa.Visible = true;
                txtTenKhoa.Visible = true;
                btnKhoaThem.Visible = true;
                btnKhoaLuu.Visible = true;
                btnKhoaXoa.Visible = true;

                lblMaLop.Visible = false;
                txtMaLop.Visible = false;
                lblTenLop.Visible = false;
                txtTenLop.Visible = false;
                lblKhoaForLop.Visible = false;
                cboKhoaForLop.Visible = false;
                btnLopThem.Visible = false;
                btnLopLuu.Visible = false;
                btnLopXoa.Visible = false;

                LoadKhoa();
            }
            else
            {
                // default: hide lop controls and show Khoa
                cmbDanhMucSelector.SelectedIndex = 0;
            }
        }

        private void LoadComboKhoaForLop()
        {
            var dt = KhoaDAL.LayDanhSach();
            cboKhoaForLop.DataSource = dt;
            cboKhoaForLop.DisplayMember = "TenKhoa";
            cboKhoaForLop.ValueMember = "MaKhoa";
        }

        private void LoadLopHanhChinh()
        {
            var dt = LopHanhChinhDAL.LayDanhSach();
            dgvLopHanhChinh.DataSource = dt;
            if (dgvLopHanhChinh.Columns.Contains("MaLop")) dgvLopHanhChinh.Columns["MaLop"].HeaderText = "Mã lớp";
            if (dgvLopHanhChinh.Columns.Contains("TenLop")) dgvLopHanhChinh.Columns["TenLop"].HeaderText = "Tên lớp";
            if (dgvLopHanhChinh.Columns.Contains("TenKhoa")) dgvLopHanhChinh.Columns["TenKhoa"].HeaderText = "Khoa";
            ClearLopFields();
        }

        private void ClearLopFields()
        {
            txtMaLop.Text = string.Empty;
            txtTenLop.Text = string.Empty;
            txtMaLop.Enabled = true;
        }

        private void dgvLopHanhChinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvLopHanhChinh.Rows[e.RowIndex];
            if (row.Cells["MaLop"].Value != null)
                txtMaLop.Text = row.Cells["MaLop"].Value.ToString();
            if (row.Cells["TenLop"].Value != null)
                txtTenLop.Text = row.Cells["TenLop"].Value.ToString();
            if (row.Cells["MaKhoa"].Value != null)
                cboKhoaForLop.SelectedValue = row.Cells["MaKhoa"].Value.ToString();
            txtMaLop.Enabled = false;
        }

        private void btnLopThem_Click(object sender, EventArgs e)
        {
            ClearLopFields();
            txtMaLop.Focus();
        }

        private void btnLopLuu_Click(object sender, EventArgs e)
        {
            string ma = txtMaLop.Text.Trim();
            string ten = txtTenLop.Text.Trim();
            string makhoa = cboKhoaForLop.SelectedValue?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(ma)) { MessageBox.Show("Mã lớp không được để trống"); return; }
            if (ma.Contains(" ")) { MessageBox.Show("Mã lớp không được chứa khoảng trắng"); return; }
            if (string.IsNullOrEmpty(ten)) { MessageBox.Show("Tên lớp không được để trống"); return; }
            if (string.IsNullOrEmpty(makhoa)) { MessageBox.Show("Phải chọn Khoa cho Lớp"); return; }

            try
            {
                bool exists = false;
                foreach (DataGridViewRow r in dgvLopHanhChinh.Rows)
                {
                    if (r.Cells["MaLop"].Value != null && r.Cells["MaLop"].Value.ToString() == ma)
                    { exists = true; break; }
                }

                if (!exists)
                {
                    LopHanhChinhDAL.Them(ma, ten, makhoa);
                    MessageBox.Show("Thêm Lớp hành chính thành công.");
                }
                else
                {
                    LopHanhChinhDAL.Sua(ma, ten, makhoa);
                    MessageBox.Show("Cập nhật Lớp hành chính thành công.");
                }

                LoadLopHanhChinh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu Lớp: " + ex.Message);
            }
        }

        private void btnLopXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaLop.Text.Trim();
            if (string.IsNullOrEmpty(ma)) { MessageBox.Show("Chọn Lớp để xóa"); return; }

            try
            {
                if (LopHanhChinhDAL.HasDependencies(ma))
                {
                    MessageBox.Show("Không thể xóa Lớp vì đã có liên quan (ví dụ: sinh viên hoặc buổi học).");
                    return;
                }

                var confirm = MessageBox.Show($"Bạn có chắc muốn xóa Lớp '{ma}' không?", "Xác nhận", MessageBoxButtons.YesNo);
                if (confirm != DialogResult.Yes) return;

                LopHanhChinhDAL.Xoa(ma);
                MessageBox.Show("Xóa thành công.");
                LoadLopHanhChinh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa Lớp: " + ex.Message);
            }
        }

        private void ClearKhoaFields()
        {
            txtMaKhoa.Text = string.Empty;
            txtTenKhoa.Text = string.Empty;
            txtMaKhoa.Enabled = true;
        }

        private void dgvKhoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvKhoa.Rows[e.RowIndex];
            if (row.Cells["MaKhoa"].Value != null)
                txtMaKhoa.Text = row.Cells["MaKhoa"].Value.ToString();
            if (row.Cells["TenKhoa"].Value != null)
                txtTenKhoa.Text = row.Cells["TenKhoa"].Value.ToString();
            txtMaKhoa.Enabled = false; // primary key cannot be edited here
        }

        private void btnKhoaThem_Click(object sender, EventArgs e)
        {
            ClearKhoaFields();
            txtMaKhoa.Focus();
        }

        private void btnKhoaLuu_Click(object sender, EventArgs e)
        {
            string ma = txtMaKhoa.Text.Trim();
            string ten = txtTenKhoa.Text.Trim();
            if (string.IsNullOrEmpty(ma)) { MessageBox.Show("Mã khoa không được để trống"); return; }
            if (ma.Contains(" ")) { MessageBox.Show("Mã khoa không được chứa khoảng trắng"); return; }
            if (string.IsNullOrEmpty(ten)) { MessageBox.Show("Tên khoa không được để trống"); return; }

            try
            {
                // Determine insert or update by checking if MaKhoa exists in grid's datasource
                bool exists = false;
                foreach (DataGridViewRow r in dgvKhoa.Rows)
                {
                    if (r.Cells["MaKhoa"].Value != null && r.Cells["MaKhoa"].Value.ToString() == ma)
                    {
                        exists = true; break;
                    }
                }

                if (!exists)
                {
                    KhoaDAL.Them(ma, ten);
                    MessageBox.Show("Thêm Khoa thành công.");
                }
                else
                {
                    KhoaDAL.Sua(ma, ten);
                    MessageBox.Show("Cập nhật Khoa thành công.");
                }

                LoadKhoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu Khoa: " + ex.Message);
            }
        }

        private void btnKhoaXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaKhoa.Text.Trim();
            if (string.IsNullOrEmpty(ma)) { MessageBox.Show("Chọn Khoa để xóa"); return; }

            try
            {
                if (KhoaDAL.HasDependencies(ma))
                {
                    MessageBox.Show("Không thể xóa Khoa vì đã có Lớp hành chính hoặc Giảng viên gắn với Khoa này.");
                    return;
                }

                var confirm = MessageBox.Show($"Bạn có chắc muốn xóa Khoa '{ma}' không?", "Xác nhận", MessageBoxButtons.YesNo);
                if (confirm != DialogResult.Yes) return;

                KhoaDAL.Xoa(ma);
                MessageBox.Show("Xóa thành công.");
                LoadKhoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa Khoa: " + ex.Message);
            }
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear session and return to login (Form1)
            try
            {
                UserSession.Clear();
            }
            catch
            {
                // ignore
            }

            // Show login form again
            var login = new Form1();
            login.Show();

            this.Close();
        }

        private void lblAdminName_Click(object sender, EventArgs e)
        {

        }

        private void cboKhoaForLop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbRoleFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void headerPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
