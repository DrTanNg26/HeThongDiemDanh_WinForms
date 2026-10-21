using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    public partial class FrmGiangVienMain : Form
    {
        public FrmGiangVienMain()
        {
            InitializeComponent();
        }

        private void FrmGiangVienMain_Load(object? sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode) return;
            if (!UserSession.DaDangNhap || !UserSession.LaGiangVien)
            {
                MessageBox.Show(this, "Vui lòng đăng nhập bằng tài khoản giảng viên.", "Thông báo");
                Close();
                return;
            }
            lblGiangVien.Text = "Xin chào: " + UserSession.HoTen;
        }

        private void btnDangXuat_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Close();
        }

        // Làm mới dữ liệu khi chuyển sang tab Điều chỉnh / Tổng kết
        private void TabMain_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabSuaThuCong)
                _ucSuaThuCong.TaiDanhSachLop();   // Nạp lại lớp HP vào UC Điều chỉnh thủ công
            else if (tabMain.SelectedTab == tabTongKet)
                _ucTongKet.TaiDanhSachLop();       // Nạp lại lớp HP vào UC Tổng kết
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _ucMoPhien.DungTheoDoi();
            UserSession.Clear();
            base.OnFormClosed(e);
        }
    }
}
