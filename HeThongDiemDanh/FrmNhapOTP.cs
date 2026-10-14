using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    public partial class FrmNhapOTP : Form
    {
        private int maBuoiHoc;
        private string loaiCauHoi = "";
        private string traLoiXacThuc = "";
        private bool xemThu;
        private bool thuDiMuon;
        private bool dangGui;
        public string KetQua { get; private set; } = "";

        public FrmNhapOTP()
        {
            InitializeComponent();
        }

        public FrmNhapOTP(int maBuoi, string tenMon, string loai, string traLoi,
            bool cheDoXemThu, bool diMuon) : this()
        {
            maBuoiHoc = maBuoi;
            loaiCauHoi = loai;
            traLoiXacThuc = traLoi;
            xemThu = cheDoXemThu;
            thuDiMuon = diMuon;
            lblMonHoc.Text = tenMon;
            if (xemThu)
            {
                lblHuongDan.Text = "XEM THỬ • Mã mẫu: UTT234.\r\nKết quả chỉ lưu tạm trên màn hình, không ghi vào SQL.";
            }
            else if (AppConfig.TestMode)
            {
                lblHuongDan.Text = "CHẾ ĐỘ TEST MẠNG • Bỏ qua kiểm tra cùng mạng.\r\nĐiểm danh vẫn được lưu thật vào cơ sở dữ liệu.";
            }
            else
            {
                lblHuongDan.Text = "Nhập mã giảng viên cung cấp.\r\nThiết bị cần kết nối cùng mạng phòng học.";
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
            btnDong.Enabled = false;
            txtOTP.Enabled = false;
            this.UseWaitCursor = true;
            lblThongBao.ForeColor = Color.FromArgb(91, 108, 128);
            lblThongBao.Text = "Đang kiểm tra và ghi nhận điểm danh…";
            try
            {
                if (xemThu)
                {
                    if (otp != "UTT234")
                    {
                        throw new InvalidOperationException("Mã chưa đúng. Mã xem thử là UTT234.");
                    }
                    if (thuDiMuon)
                    {
                        KetQua = "Đi muộn";
                    }
                    else
                    {
                        KetQua = "Có mặt";
                    }
                }
                else
                {
                    // SQL kiểm tra lại phiên, thông tin xác thực, OTP, IP và điểm danh trùng.
                    KetQua = await DiemDanhDAL.GhiNhanDiemDanhAsync(UserSession.TenDangNhap,
                        maBuoiHoc, otp, loaiCauHoi, traLoiXacThuc, CancellationToken.None);
                }

                if (KetQua == "Có mặt")
                {
                    lblThongBao.ForeColor = Color.FromArgb(24, 117, 75);
                }
                else
                {
                    lblThongBao.ForeColor = Color.FromArgb(155, 98, 0);
                }
                if (xemThu)
                {
                    lblThongBao.Text = "Kết quả xem thử: " + KetQua + ". Không lưu SQL.";
                }
                else
                {
                    lblThongBao.Text = "Điểm danh thành công: " + KetQua + ".";
                }
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
    }
}
