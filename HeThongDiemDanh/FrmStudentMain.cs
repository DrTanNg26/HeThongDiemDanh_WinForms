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
        private Dictionary<int, string> ketQuaXemThu = new Dictionary<int, string>();

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
            chkXemThu.Enabled = false;
            btnDiemDanh.Enabled = false;
            lblStatus.ForeColor = Color.FromArgb(91, 108, 128);
            lblStatus.Text = "Đang tải danh sách buổi học…";

            try
            {
                // await chờ SQL trả dữ liệu nhưng vẫn cho phép thao tác trên form.
                DataTable dt;
                if (chkXemThu.Checked)
                {
                    dt = TaoBuoiHocMau();
                }
                else
                {
                    dt = await SinhVienDAL.LayBuoiHocDangMoAsync(maSV, lifetime.Token);
                }

                if (this.IsDisposed || lifetime.IsCancellationRequested)
                {
                    dt.Dispose();
                    return;
                }

                // Xóa dữ liệu cũ trước khi gán danh sách vừa tải.
                ClearData();
                dgvBuoiHoc.DataSource = dt;
                DinhDangBangBuoiHoc();

                if (chkXemThu.Checked)
                {
                    lblStatus.Text = "Có 2 buổi học mẫu để thử điểm danh đúng giờ và đi muộn.";
                }
                else if (dt.Rows.Count == 0)
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
                    chkXemThu.Enabled = true;
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

        private DataTable TaoBuoiHocMau()
        {
            // Dữ liệu chỉ nằm trong bộ nhớ. Hàm này không gọi SQL.
            DataTable dt = new DataTable();
            dt.Columns.Add("MaBuoiHoc", typeof(int));
            dt.Columns.Add("TenMon", typeof(string));
            dt.Columns.Add("TenLopHP", typeof(string));
            dt.Columns.Add("GiangVien", typeof(string));
            dt.Columns.Add("PhongHoc", typeof(string));
            dt.Columns.Add("NgayHoc", typeof(DateTime));
            dt.Columns.Add("ThoiGianBatDau", typeof(DateTime));
            dt.Columns.Add("ThoiGianDemNguoc", typeof(int));
            dt.Columns.Add("KetQua", typeof(string));
            string ketQuaDungGio = "Chưa điểm danh";
            string ketQuaDiMuon = "Chưa điểm danh";
            if (ketQuaXemThu.ContainsKey(-1))
            {
                ketQuaDungGio = ketQuaXemThu[-1];
            }
            if (ketQuaXemThu.ContainsKey(-2))
            {
                ketQuaDiMuon = ketQuaXemThu[-2];
            }
            dt.Rows.Add(-1, "Lập trình trực quan", "Mẫu • Đúng giờ", "Giảng viên mẫu", "P502",
                DateTime.Today, DateTime.Now.AddMinutes(-2), 10, ketQuaDungGio);
            dt.Rows.Add(-2, "Cơ sở dữ liệu", "Mẫu • Đi muộn", "Giảng viên mẫu", "P401",
                DateTime.Today, DateTime.Now.AddMinutes(-16), 10, ketQuaDiMuon);
            return dt;
        }

        private async void chkXemThu_CheckedChanged(object? sender, EventArgs e)
        {
            CapNhatHuongDan();
            await LoadBuoiHoc();
        }

        private void CapNhatHuongDan()
        {
            if (chkXemThu.Checked)
            {
                lblSection.Text = "Buổi học mẫu • Không lưu SQL";
                lblNextStep.ForeColor = Color.FromArgb(155, 98, 0);
                lblNextStep.Text = "XEM THỬ: ngày sinh 15/05/2005 hoặc CCCD 1234; OTP UTT234. Kết quả không được tính chuyên cần.";
            }
            else
            {
                lblSection.Text = "Buổi học đang mở";
                lblNextStep.ForeColor = Color.FromArgb(91, 108, 128);
                if (AppConfig.TestMode)
                {
                    lblNextStep.Text = "TEST MẠNG đang bật: bỏ qua kiểm tra cùng mạng; kết quả điểm danh vẫn lưu thật vào SQL.";
                }
                else
                {
                    lblNextStep.Text = "Chọn buổi học, bấm Vào điểm danh để xác thực thông tin và nhập OTP.";
                }
            }
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
            bool xemThu = chkXemThu.Checked;
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

            using (FrmXacThuc frmXacThuc = new FrmXacThuc(maBuoi, thongTin, xemThu))
            {
                if (frmXacThuc.ShowDialog(this) != DialogResult.OK)
                {
                    CapNhatNutDiemDanh();
                    return;
                }
                using (FrmNhapOTP frmOTP = new FrmNhapOTP(maBuoi, tenMon, thongTin.LoaiCauHoi,
                    frmXacThuc.TraLoi, xemThu, maBuoi == -2))
                {
                    frmOTP.ShowDialog(this);
                    if (xemThu && frmOTP.KetQua != "")
                    {
                        ketQuaXemThu[maBuoi] = frmOTP.KetQua;
                    }
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Chạy khi bấm Đăng xuất hoặc dấu X của form.
            lifetime.Cancel();
            clockTimer.Stop();
            ClearData();

            // Form đăng nhập sẽ xóa phiên và hiện lại sau khi form này đóng.
            base.OnFormClosed(e);
        }

        private void lblSection_Click(object sender, EventArgs e)
        {

        }
    }
}
