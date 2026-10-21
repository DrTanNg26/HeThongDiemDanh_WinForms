using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    public partial class FrmStudentMain : Form
    {
        // Dùng để dừng việc tải SQL khi đóng form.
        // Tên lifetime cũng được dùng trong hàm Dispose của Designer.
        private readonly CancellationTokenSource lifetime = new CancellationTokenSource();
        private bool dangTaiDuLieu = false;
        private string maSV = "";
        private Dictionary<int, TrangThaiXacThuc> cacLanXacThuc = new Dictionary<int, TrangThaiXacThuc>();
        private bool dangTaiLichSu = false;
        private DataTable? bangLichSu;

        public FrmStudentMain()
        {
            InitializeComponent();
        }

        private async void FrmStudentMain_Load(object? sender, EventArgs e)
        {
            // Không tải dữ liệu khi đang thiết kế giao diện trong Visual Studio.
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            if (!UserSession.DaDangNhap || !UserSession.LaSinhVien)
            {
                MessageBox.Show(this, "Vui lòng đăng nhập bằng tài khoản sinh viên.", "Thông báo");
                this.Close();
                return;
            }

            LoadThongTinSinhVien();
            CapNhatHuongDan();
            CapNhatDongHo();
            clockTimer.Start();
            await LoadBuoiHoc();
        }

        private void LoadThongTinSinhVien()
        {
            maSV = UserSession.TenDangNhap;
            lblHoTen.Text = "Xin chào, " + UserSession.HoTen;
            lblThongTin.Text = "Mã sinh viên: " + maSV;
            lblThongTin.Text += "    •    Lớp: " + UserSession.MaLopHanhChinh;
            lblThongTin.Text += "    •    Khoa: " + UserSession.MaKhoa;

            if (string.IsNullOrWhiteSpace(UserSession.Email))
            {
                lblEmail.Text = "Chưa cập nhật email";
            }
            else
            {
                lblEmail.Text = UserSession.Email;
            }
        }

        private void CapNhatDongHo()
        {
            // Giờ máy chỉ dùng để hiển thị. Nghiệp vụ điểm danh dùng giờ SQL Server.
            lblClock.Text = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
        }

        private void clockTimer_Tick(object? sender, EventArgs e)
        {
            CapNhatDongHo();
        }

        private async void btnRefresh_Click(object? sender, EventArgs e)
        {
            await LoadBuoiHoc();
        }

        private async Task LoadBuoiHoc()
        {
            // Không tải chồng lên nhau hoặc tải tiếp sau khi đóng form.
            if (dangTaiDuLieu || lifetime.IsCancellationRequested || string.IsNullOrEmpty(maSV))
            {
                return;
            }

            dangTaiDuLieu = true;
            btnRefresh.Enabled = false;
            btnDiemDanh.Enabled = false;
            lblStatus.ForeColor = Color.FromArgb(91, 108, 128);
            lblStatus.Text = "Đang tải danh sách buổi học…";

            try
            {
                // await chờ SQL trả dữ liệu nhưng vẫn cho phép thao tác trên form.
                DataTable dt = await SinhVienDAL.LayBuoiHocDangMoAsync(maSV, lifetime.Token);

                if (this.IsDisposed || lifetime.IsCancellationRequested)
                {
                    dt.Dispose();
                    return;
                }

                // Xóa dữ liệu cũ trước khi gán danh sách vừa tải.
                ClearData();
                dgvBuoiHoc.DataSource = dt;
                DinhDangBangBuoiHoc();

                if (dt.Rows.Count == 0)
                {
                    lblStatus.Text = "Hiện tại không có môn học nào đang mở điểm danh. Bấm Làm mới để kiểm tra lại.";
                }
                else
                {
                    lblStatus.Text = "Có " + dt.Rows.Count + " buổi học đang mở trong các lớp của bạn.";
                }
            }
            catch (OperationCanceledException) when (lifetime.IsCancellationRequested)
            {
                // Đóng form thì hủy tải dữ liệu, không cần báo lỗi.
            }
            catch (Exception)
            {
                if (!this.IsDisposed && !lifetime.IsCancellationRequested)
                {
                    ClearData();
                    lblStatus.ForeColor = Color.Firebrick;
                    lblStatus.Text = "Không tải được buổi học. Vui lòng kiểm tra kết nối máy chủ và bấm Làm mới.";
                }
            }
            finally
            {
                // Bật lại nút Làm mới dù tải thành công hay gặp lỗi.
                dangTaiDuLieu = false;
                if (!this.IsDisposed && !lifetime.IsCancellationRequested)
                {
                    btnRefresh.Enabled = true;
                    CapNhatNutDiemDanh();
                }
            }
        }

        private void DinhDangBangBuoiHoc()
        {
            // Giữ mã buổi trong dữ liệu để xử lý, chỉ hiện các cột sinh viên cần đọc.
            foreach (DataGridViewColumn cot in dgvBuoiHoc.Columns)
            {
                if (cot.Name == "MaBuoiHoc" || cot.Name == "NgayHoc" || cot.Name == "ThoiGianDemNguoc")
                {
                    cot.Visible = false;
                }
                else if (cot.Name == "TenMon")
                {
                    cot.HeaderText = "Môn học";
                }
                else if (cot.Name == "TenLopHP")
                {
                    cot.HeaderText = "Lớp học phần";
                }
                else if (cot.Name == "GiangVien")
                {
                    cot.HeaderText = "Giảng viên";
                }
                else if (cot.Name == "PhongHoc")
                {
                    cot.HeaderText = "Phòng";
                }
                else if (cot.Name == "ThoiGianBatDau")
                {
                    cot.HeaderText = "Giờ mở";
                    cot.DefaultCellStyle.Format = "dd/MM HH:mm";
                }
                else if (cot.Name == "KetQua")
                {
                    cot.HeaderText = "Điểm danh";
                }
            }
        }

        private void CapNhatHuongDan()
        {
            lblNextStep.Text = "Chọn buổi học để xác thực và nhập OTP. Máy sinh viên phải cùng mạng Wi-Fi/LAN với giảng viên.";
        }

        private void dgvBuoiHoc_CurrentCellChanged(object? sender, EventArgs e)
        {
            CapNhatNutDiemDanh();
        }

        private void CapNhatNutDiemDanh()
        {
            btnDiemDanh.Enabled = false;
            if (dangTaiDuLieu || dgvBuoiHoc.CurrentRow == null)
            {
                return;
            }
            DataRowView? dong = dgvBuoiHoc.CurrentRow.DataBoundItem as DataRowView;
            if (dong == null)
            {
                return;
            }
            int maBuoi = Convert.ToInt32(dong["MaBuoiHoc"]);
            if (cacLanXacThuc.ContainsKey(maBuoi) && cacLanXacThuc[maBuoi].DaBiKhoa())
            {
                btnDiemDanh.Text = "Đã khóa xác thực";
            }
            else if (dong["KetQua"].ToString() != "Chưa điểm danh")
            {
                btnDiemDanh.Text = "Đã điểm danh";
            }
            else
            {
                btnDiemDanh.Text = "Vào điểm danh";
                btnDiemDanh.Enabled = true;
            }
        }

        private async void btnDiemDanh_Click(object? sender, EventArgs e)
        {
            if (dgvBuoiHoc.CurrentRow == null || !btnDiemDanh.Enabled)
            {
                return;
            }
            DataRowView? dong = dgvBuoiHoc.CurrentRow.DataBoundItem as DataRowView;
            if (dong == null)
            {
                return;
            }
            int maBuoi = Convert.ToInt32(dong["MaBuoiHoc"]);
            string tenMon = Convert.ToString(dong["TenMon"]) ?? "Buổi học";
            if (!cacLanXacThuc.ContainsKey(maBuoi))
            {
                cacLanXacThuc.Add(maBuoi, new TrangThaiXacThuc());
            }
            TrangThaiXacThuc thongTin = cacLanXacThuc[maBuoi];
            if (thongTin.DaBiKhoa())
            {
                CapNhatNutDiemDanh();
                return;
            }

            using (FrmXacThuc frmXacThuc = new FrmXacThuc(maBuoi, thongTin))
            {
                if (frmXacThuc.ShowDialog(this) != DialogResult.OK)
                {
                    CapNhatNutDiemDanh();
                    return;
                }
                using (FrmNhapOTP frmOTP = new FrmNhapOTP(maBuoi, tenMon, thongTin.LoaiCauHoi,
                    frmXacThuc.TraLoi))
                {
                    frmOTP.ShowDialog(this);
                }
            }
            // Kể cả khi gặp lỗi mạng, tải lại để biết SQL đã ghi nhận hay chưa.
            await LoadBuoiHoc();
        }

        private void ClearData()
        {
            // Bỏ liên kết với bảng rồi giải phóng dữ liệu cũ.
            DataTable? dtCu = dgvBuoiHoc.DataSource as DataTable;
            dgvBuoiHoc.DataSource = null;

            if (dtCu != null)
            {
                dtCu.Dispose();
            }
        }

        private void btnLogout_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private async void tabs_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (tabs.SelectedTab == tabLichSu)
            {
                await LoadLichSu();
            }
        }

        private async void btnLamMoiLichSu_Click(object? sender, EventArgs e)
        {
            await LoadLichSu();
        }

        private async Task LoadLichSu()
        {
            if (dangTaiLichSu || lifetime.IsCancellationRequested || string.IsNullOrEmpty(maSV))
            {
                return;
            }
            dangTaiLichSu = true;
            btnLamMoiLichSu.Enabled = false;
            cboLopLichSu.Enabled = false;
            cboTrangThaiLichSu.Enabled = false;
            lblHistory.ForeColor = Color.FromArgb(91, 108, 128);
            lblHistory.Text = "Đang tải lịch sử điểm danh…";
            string lopDaChon = Convert.ToString(cboLopLichSu.SelectedValue) ?? "";
            try
            {
                DataTable dt = await SinhVienDAL.LayLichSuDiemDanhAsync(maSV, lifetime.Token);
                if (this.IsDisposed || lifetime.IsCancellationRequested)
                {
                    dt.Dispose();
                    return;
                }
                XoaBangLichSu();
                bangLichSu = dt;
                dgvLichSu.DataSource = bangLichSu.DefaultView;
                LoadBoLocLop(lopDaChon);
                DinhDangBangLichSu();
                LocLichSu();
            }
            catch (OperationCanceledException) when (lifetime.IsCancellationRequested)
            {
                // Đóng form thì dừng truy vấn, không hiện thông báo lỗi.
            }
            catch (Exception)
            {
                if (!this.IsDisposed && !lifetime.IsCancellationRequested)
                {
                    XoaBangLichSu();
                    lblHistory.ForeColor = Color.Firebrick;
                    lblHistory.Text = "Không tải được lịch sử. Kiểm tra kết nối máy chủ và bấm Làm mới.";
                }
            }
            finally
            {
                dangTaiLichSu = false;
                if (!this.IsDisposed && !lifetime.IsCancellationRequested)
                {
                    btnLamMoiLichSu.Enabled = true;
                    cboLopLichSu.Enabled = bangLichSu != null;
                    cboTrangThaiLichSu.Enabled = bangLichSu != null;
                }
            }
        }

        private void LoadBoLocLop(string lopDaChon)
        {
            DataTable danhSach = new DataTable();
            danhSach.Columns.Add("MaLopHP");
            danhSach.Columns.Add("TenHienThi");
            danhSach.Rows.Add("", "Tất cả lớp học phần");
            HashSet<string> cacMaLop = new HashSet<string>();
            if (bangLichSu != null)
            {
                foreach (DataRow dong in bangLichSu.Rows)
                {
                    string maLop = Convert.ToString(dong["MaLopHP"]) ?? "";
                    if (cacMaLop.Add(maLop))
                    {
                        danhSach.Rows.Add(maLop, dong["TenLopHP"] + " (" + maLop + ")");
                    }
                }
            }
            DataTable? bangCu = cboLopLichSu.DataSource as DataTable;
            cboLopLichSu.DisplayMember = "TenHienThi";
            cboLopLichSu.ValueMember = "MaLopHP";
            cboLopLichSu.DataSource = danhSach;
            if (bangCu != null) bangCu.Dispose();
            if (cacMaLop.Contains(lopDaChon))
            {
                cboLopLichSu.SelectedValue = lopDaChon;
            }
            else
            {
                cboLopLichSu.SelectedIndex = 0;
            }
        }

        private void cboLocLichSu_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (!dangTaiLichSu)
            {
                LocLichSu();
            }
        }

        private void LocLichSu()
        {
            if (bangLichSu == null) return;
            lblHistory.ForeColor = Color.FromArgb(91, 108, 128);
            string maLop = Convert.ToString(cboLopLichSu.SelectedValue) ?? "";
            string dieuKien = "";
            if (maLop != "")
            {
                dieuKien = "MaLopHP = '" + maLop.Replace("'", "''") + "'";
            }
            if (cboTrangThaiLichSu.SelectedIndex > 0)
            {
                if (dieuKien != "") dieuKien += " AND ";
                dieuKien += "TrangThai = '" + cboTrangThaiLichSu.Text.Replace("'", "''") + "'";
            }
            bangLichSu.DefaultView.RowFilter = dieuKien;

            int coMat = 0, diMuon = 0, vang = 0, nghiPhep = 0;
            foreach (DataRowView dong in bangLichSu.DefaultView)
            {
                string trangThai = Convert.ToString(dong["TrangThai"]) ?? "";
                if (trangThai == "Có mặt") coMat++;
                else if (trangThai == "Đi muộn") diMuon++;
                else if (trangThai == "Vắng") vang++;
                else if (trangThai == "Nghỉ có phép") nghiPhep++;
            }
            if (bangLichSu.Rows.Count == 0)
            {
                lblHistory.Text = "Bạn chưa có kết quả điểm danh được ghi nhận.";
            }
            else if (bangLichSu.DefaultView.Count == 0)
            {
                lblHistory.Text = "Không có kết quả phù hợp với bộ lọc đang chọn.";
            }
            else
            {
                lblHistory.Text = "Đang xem " + bangLichSu.DefaultView.Count + " kết quả • Có mặt: " + coMat
                    + " • Đi muộn: " + diMuon + " • Vắng: " + vang + " • Nghỉ có phép: " + nghiPhep;
            }
        }

        private void DinhDangBangLichSu()
        {
            foreach (DataGridViewColumn cot in dgvLichSu.Columns)
            {
                cot.MinimumWidth = 110;
                if (cot.Name == "MaLopHP") cot.Visible = false;
                else if (cot.Name == "TenLopHP") cot.HeaderText = "Lớp học phần";
                else if (cot.Name == "TenMon") cot.HeaderText = "Môn học";
                else if (cot.Name == "TenBuoiHoc") cot.HeaderText = "Buổi học";
                else if (cot.Name == "TrangThai") cot.HeaderText = "Kết quả";
                else if (cot.Name == "GhiChu") cot.HeaderText = "Ghi chú";
                else if (cot.Name == "NgayHoc")
                {
                    cot.HeaderText = "Ngày học";
                    cot.DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                else if (cot.Name == "ThoiGianQuet")
                {
                    cot.HeaderText = "Lúc điểm danh";
                    cot.MinimumWidth = 160;
                    cot.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    cot.DefaultCellStyle.NullValue = "—";
                }
            }
        }

        private void XoaBangLichSu()
        {
            dgvLichSu.DataSource = null;
            if (bangLichSu != null)
            {
                bangLichSu.Dispose();
                bangLichSu = null;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Chạy khi bấm Đăng xuất hoặc dấu X của form.
            lifetime.Cancel();
            clockTimer.Stop();
            ClearData();
            XoaBangLichSu();

            DataTable? cacLop = cboLopLichSu.DataSource as DataTable;
            cboLopLichSu.DataSource = null;
            if (cacLop != null) cacLop.Dispose();

            // Form đăng nhập sẽ xóa phiên và hiện lại sau khi form này đóng.
            base.OnFormClosed(e);
        }

        private void lblSection_Click(object sender, EventArgs e)
        {

        }
    }
}
