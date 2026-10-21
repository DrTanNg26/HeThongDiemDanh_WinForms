using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    public partial class FrmTrinhChieu : Form
    {
        private int maBuoiHoc;
        private bool dangTai;
        private bool dangThaoTac;
        private bool daDongBuoi;
        public int MaBuoiHoc { get { return maBuoiHoc; } }

        // Constructor không tham số để Visual Studio mở Designer.
        public FrmTrinhChieu()
        {
            InitializeComponent();
        }

        public FrmTrinhChieu(int maBuoi, string tenLop, string tenBuoi, string otp) : this()
        {
            maBuoiHoc = maBuoi;
            Text = "Trình chiếu - " + tenLop + " - " + tenBuoi;
            lblTitle.Text = tenLop + " • " + tenBuoi;
            lblOtp.Text = otp;
        }

        private async void FrmTrinhChieu_Load(object? sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode || maBuoiHoc <= 0) return;
            timerCapNhat.Start();
            await CapNhatAsync();
        }

        private async void timerCapNhat_Tick(object? sender, EventArgs e)
        {
            await CapNhatAsync();
        }

        private async Task CapNhatAsync()
        {
            if (dangTai || dangThaoTac || IsDisposed || daDongBuoi) return;
            dangTai = true;
            DataTable? danhSach = null;
            try
            {
                ThongTinBuoiHoc? buoi = null;
                DateTime gioServer = DateTime.MinValue;
                await Task.Run(() =>
                {
                    // SQL tự quyết định đã hết hạn chưa; không tin đồng hồ máy giảng viên.
                    BuoiHocDAL.DongBuoiHoc(maBuoiHoc, true);
                    buoi = BuoiHocDAL.LayThongTinBuoiHoc(maBuoiHoc);
                    gioServer = BuoiHocDAL.LayGioServer();
                    danhSach = BuoiHocDAL.LayDanhSachDiemDanhRealtime(maBuoiHoc);
                });
                if (IsDisposed || !IsHandleCreated) return;
                if (buoi == null || buoi.TrangThai != "Mo")
                {
                    daDongBuoi = true;
                    Close();
                    return;
                }
                int giay = Math.Max(0, (int)Math.Ceiling((buoi.ThoiGianBatDau.AddMinutes(buoi.ThoiGianDemNguoc) - gioServer).TotalSeconds));
                lblCountdown.Text = (giay / 60).ToString("00") + ":" + (giay % 60).ToString("00");
                lblCountdown.ForeColor = giay < 120 ? Color.Firebrick : Color.ForestGreen;
                lblTrangThai.Text = giay == 0 ? "Hết giờ, đang chốt phiên…" : "Phiên tự kết thúc khi đồng hồ về 00:00.";
                lblTrangThai.ForeColor = Color.DarkSlateGray;
                lblSiSo.Text = "Sĩ số lớp: " + buoi.TongSiSo;
                int coMat = 0;
                if (danhSach != null)
                {
                    foreach (DataRow dong in danhSach.Rows)
                    {
                        string trangThai = Convert.ToString(dong["TrangThai"]) ?? "";
                        if (trangThai == "Có mặt" || trangThai == "Đi muộn") coMat++;
                    }
                    DataTable? cu = dgvRealtime.DataSource as DataTable;
                    dgvRealtime.DataSource = danhSach;
                    danhSach = null;
                    if (cu != null) cu.Dispose();
                    foreach (DataGridViewColumn cot in dgvRealtime.Columns)
                    {
                        if (cot.Name == "MaSV") cot.HeaderText = "Mã sinh viên";
                        else if (cot.Name == "HoTen") cot.HeaderText = "Họ và tên";
                        else if (cot.Name == "GioCheckIn") cot.HeaderText = "Giờ điểm danh";
                        else if (cot.Name == "TrangThai") cot.HeaderText = "Kết quả";
                    }
                }
                lblCoMat.Text = "Đã điểm danh: " + coMat;
                lblChuaDiemDanh.Text = "Chưa điểm danh: " + Math.Max(0, buoi.TongSiSo - coMat);
            }
            catch (Exception)
            {
                if (!IsDisposed)
                {
                    lblTrangThai.Text = "Chưa kết nối được máy chủ. Đang thử lại…";
                    lblTrangThai.ForeColor = Color.Firebrick;
                    lblCountdown.Text = "--:--";
                }
                // Giữ timer để thử lại khi máy chủ có kết nối; vẫn cho phép tắt cửa sổ.
            }
            finally
            {
                danhSach?.Dispose();
                dangTai = false;
            }
        }

        private async void btnDongBuoiHoc_Click(object? sender, EventArgs e)
        {
            if (dangThaoTac || daDongBuoi) return;
            if (MessageBox.Show(this, "Kết thúc buổi học và ghi Vắng cho sinh viên chưa điểm danh?",
                "Kết thúc buổi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            dangThaoTac = true;
            btnDongBuoiHoc.Enabled = false;
            btnHuyPhien.Enabled = false;
            try
            {
                await Task.Run(() => BuoiHocDAL.DongBuoiHoc(maBuoiHoc));
                daDongBuoi = true;
                if (!IsDisposed) Close();
            }
            catch (Exception)
            {
                if (!IsDisposed) lblTrangThai.Text = "Chưa kết thúc được phiên. Kiểm tra máy chủ và thử lại.";
            }
            finally
            {
                dangThaoTac = false;
                if (!IsDisposed) { btnDongBuoiHoc.Enabled = true; btnHuyPhien.Enabled = true; }
            }
        }

        private async void btnHuyPhien_Click(object? sender, EventArgs e)
        {
            if (dangThaoTac || daDongBuoi) return;
            if (MessageBox.Show(this, "Hủy phiên sẽ XÓA buổi học và toàn bộ kết quả của buổi này. Bạn có chắc chắn?",
                "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            dangThaoTac = true;
            btnDongBuoiHoc.Enabled = false;
            btnHuyPhien.Enabled = false;
            try
            {
                bool daHuy = await Task.Run(() => BuoiHocDAL.HuyPhienDiemDanh(maBuoiHoc));
                if (!IsDisposed)
                {
                    if (daHuy) { daDongBuoi = true; Close(); }
                    else lblTrangThai.Text = "Phiên đã được đóng hoặc không còn tồn tại.";
                }
            }
            catch (Exception)
            {
                if (!IsDisposed) lblTrangThai.Text = "Chưa hủy được phiên. Kiểm tra máy chủ và thử lại.";
            }
            finally
            {
                dangThaoTac = false;
                if (!IsDisposed) { btnDongBuoiHoc.Enabled = true; btnHuyPhien.Enabled = true; }
            }
        }

        private void FrmTrinhChieu_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Tắt trình chiếu chỉ đóng cửa sổ. Form giảng viên tiếp tục theo dõi hạn phiên.
            timerCapNhat.Stop();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            DataTable? cu = dgvRealtime.DataSource as DataTable;
            dgvRealtime.DataSource = null;
            cu?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
