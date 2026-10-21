using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    public partial class FrmNhapOTP : Form
    {
        private int maBuoiHoc;
        private string maSV = "";
        private string loaiCauHoi = "";
        private string traLoiXacThuc = "";
        private bool dangGui;
        public string KetQua { get; private set; } = "";

        public FrmNhapOTP()
        {
            InitializeComponent();
        }

        public FrmNhapOTP(int maBuoi, string tenMon, string loai, string traLoi, string maSinhVien = "") : this()
        {
            maBuoiHoc = maBuoi;
            loaiCauHoi = loai;
            traLoiXacThuc = traLoi;
            maSV = !string.IsNullOrEmpty(maSinhVien) ? maSinhVien : UserSession.TenDangNhap;
            lblMonHoc.Text = tenMon;
            lblHuongDan.Text = "Nhập mã giảng viên cung cấp.\r\nThiết bị cần kết nối cùng mạng Wi-Fi/LAN của phòng học.";
        }

        private async void btnKiemTraMang_Click(object? sender, EventArgs e)
        {
            if (dangGui || KetQua != "")
            {
                return;
            }
            dangGui = true;
            btnXacNhan.Enabled = false;
            btnKiemTraMang.Enabled = false;
            btnDong.Enabled = false;
            this.UseWaitCursor = true;
            lblThongBao.ForeColor = Color.FromArgb(91, 108, 128);
            lblThongBao.Text = "Đang kiểm tra mạng của buổi học…";
            try
            {
                ThongTinMang mang = await DiemDanhDAL.KiemTraMangAsync(maBuoiHoc, CancellationToken.None);
                lblThongBao.ForeColor = Color.FromArgb(24, 117, 75);
                lblThongBao.Text = "Cùng mạng với giảng viên. IP của bạn: " + mang.DiaChiIP
                    + " (" + mang.TenCardMang + ").";
            }
            catch (InvalidOperationException ex)
            {
                lblThongBao.ForeColor = Color.Firebrick;
                lblThongBao.Text = ex.Message;
            }
            catch (Exception)
            {
                lblThongBao.ForeColor = Color.Firebrick;
                lblThongBao.Text = "Chưa kiểm tra được mạng. Kiểm tra kết nối và thử lại.";
            }
            finally
            {
                dangGui = false;
                this.UseWaitCursor = false;
                btnXacNhan.Enabled = true;
                btnKiemTraMang.Enabled = true;
                btnDong.Enabled = true;
            }
        }

        private async void btnXacNhan_Click(object? sender, EventArgs e)
        {
            if (dangGui || KetQua != "")
            {
                return;
            }
            string otp = txtOTP.Text.Trim().ToUpperInvariant();
            if (otp.Length < 5 || otp.Length > 6)
            {
                lblThongBao.Text = "Vui lòng nhập mã OTP gồm 5 hoặc 6 ký tự.";
                txtOTP.Focus();
                return;
            }

            dangGui = true;
            btnXacNhan.Enabled = false;
            btnKiemTraMang.Enabled = false;
            btnDong.Enabled = false;
            txtOTP.Enabled = false;
            this.UseWaitCursor = true;
            lblThongBao.ForeColor = Color.FromArgb(91, 108, 128);
            lblThongBao.Text = "Đang kiểm tra và ghi nhận điểm danh…";
            try
            {
                // Kiểm tra lại mạng ngay khi gửi, kể cả khi đã bấm Kiểm tra mạng trước đó.
                string maGhiNhan = !string.IsNullOrEmpty(maSV) ? maSV : UserSession.TenDangNhap;
                KetQua = await DiemDanhDAL.GhiNhanDiemDanhAsync(maGhiNhan,
                    maBuoiHoc, otp, loaiCauHoi, traLoiXacThuc, CancellationToken.None);

                if (KetQua == "Có mặt")
                {
                    lblThongBao.ForeColor = Color.FromArgb(24, 117, 75);
                }
                else
                {
                    lblThongBao.ForeColor = Color.FromArgb(155, 98, 0);
                }
                lblThongBao.Text = "Điểm danh thành công: " + KetQua + ".";
                btnDong.Text = "Đóng";
                timerDong.Start();
            }
            catch (InvalidOperationException ex)
            {
                lblThongBao.ForeColor = Color.Firebrick;
                lblThongBao.Text = ex.Message;
            }
            catch (Exception)
            {
                lblThongBao.ForeColor = Color.Firebrick;
                lblThongBao.Text = "Chưa nhận được kết quả. Kiểm tra kết nối, làm mới danh sách trước khi thử lại.";
            }
            finally
            {
                dangGui = false;
                this.UseWaitCursor = false;
                btnDong.Enabled = true;
                if (KetQua == "")
                {
                    btnXacNhan.Enabled = true;
                    btnKiemTraMang.Enabled = true;
                    txtOTP.Enabled = true;
                }
            }
        }

        private void btnDong_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void timerDong_Tick(object? sender, EventArgs e)
        {
            timerDong.Stop();
            this.Close();
        }

        private void FrmNhapOTP_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Không đóng giữa lúc gửi để tránh người dùng gửi lặp vì chưa biết kết quả.
            if (dangGui)
            {
                e.Cancel = true;
                return;
            }
            timerDong.Stop();
            traLoiXacThuc = "";
        }

        private void txtOTP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
