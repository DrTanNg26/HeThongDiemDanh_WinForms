using System;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    public partial class FrmGiangVienMain : Form
    {
        private UcMoPhien _ucMoPhien;

        public FrmGiangVienMain()
        {
            InitializeComponent();
            _ucMoPhien = new UcMoPhien();
            _ucMoPhien.Dock = DockStyle.Fill;
            tabMoPhien.Controls.Add(_ucMoPhien);
        }

        private void FrmGiangVienMain_Load(object? sender, EventArgs e)
        {
            if (!UserSession.DaDangNhap || !UserSession.LaGiangVien)
            {
                MessageBox.Show("Vui lòng đăng nhập bằng tài khoản giảng viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            lblGiangVien.Text = $"Xin chào: {UserSession.HoTen} ({UserSession.TenDangNhap})";

            // Task 5.3: Tự động dọn dẹp các phiên quá hạn (> 4 tiếng) trong CSDL
            try { BuoiHocDAL.TuDongDongPhienQuaHan(); } catch { }

            // Task 5.1: Kiểm tra khôi phục phiên nếu có phiên mở trước đó chưa đóng
            KiemTraKhoiPhucPhien();
        }

        private void KiemTraKhoiPhucPhien()
        {
            try
            {
                var phienDangMo = BuoiHocDAL.LayPhienDangMoCuaGiangVien(UserSession.TenDangNhap);
                if (phienDangMo != null)
                {
                    DialogResult result = MessageBox.Show(
                        $"Phát hiện buổi học '{phienDangMo.TenBuoiHoc}' của lớp '{phienDangMo.TenLopHP}' đang mở chưa kết thúc.\n" +
                        "Bạn có muốn mở lại màn hình trình chiếu phiên điểm danh này không?",
                        "Khôi phục phiên điểm danh",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        FrmTrinhChieu frm = new FrmTrinhChieu(
                            phienDangMo.MaBuoiHoc,
                            phienDangMo.TenLopHP,
                            phienDangMo.TenBuoiHoc,
                            phienDangMo.MaOTP
                        );
                        frm.FormClosed += (s, args) =>
                        {
                            _ucMoPhien.LoadDanhSachLop();
                        };
                        frm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                // Không ngắt luồng chính nếu lỗi kiểm tra khôi phục
                System.Diagnostics.Debug.WriteLine("Lỗi khôi phục phiên: " + ex.Message);
            }
        }

        private void btnDangXuat_Click(object? sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                UserSession.Clear();
                this.Close();
            }
        }
    }
}
