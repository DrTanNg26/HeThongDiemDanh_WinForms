using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    public partial class FrmXacThuc : Form
    {
        private int maBuoiHoc;
        private bool xemThu;
        private bool dangKiemTra;
        private TrangThaiXacThuc trangThai = new TrangThaiXacThuc();
        public string TraLoi { get; private set; } = "";

        public FrmXacThuc()
        {
            InitializeComponent();
        }

        public FrmXacThuc(int maBuoi, TrangThaiXacThuc thongTin, bool cheDoXemThu) : this()
        {
            maBuoiHoc = maBuoi;
            trangThai = thongTin;
            xemThu = cheDoXemThu;
            LoadCauHoi();
        }

        private void LoadCauHoi()
        {
            if (trangThai.LoaiCauHoi == "NgaySinh")
            {
                lblCauHoi.Text = "Nhập ngày sinh của bạn (dd/MM/yyyy)";
                txtTraLoi.MaxLength = 10;
                txtTraLoi.PlaceholderText = "Ví dụ: 15/05/2005";
            }
            else
            {
                lblCauHoi.Text = "Nhập 4 số cuối CCCD của bạn";
                txtTraLoi.MaxLength = 4;
                txtTraLoi.PlaceholderText = "4 chữ số";
            }

            if (xemThu)
            {
                lblHuongDan.Text = "XEM THỬ • Ngày sinh mẫu: 15/05/2005; CCCD mẫu: 1234.\r\nKhông sử dụng thông tin cá nhân thật.";
            }
            else
            {
                lblHuongDan.Text = "Trả lời bằng thông tin đã đăng ký trong hồ sơ sinh viên.\r\nSai 3 lần sẽ khóa điểm danh buổi này trong phiên đăng nhập.";
            }
            HienThiSoLanThu();
        }

        private void HienThiSoLanThu()
        {
            if (trangThai.DaBiKhoa())
            {
                lblThongBao.Text = "Đã sai 3 lần. Buổi học này đã bị khóa xác thực.";
                txtTraLoi.Enabled = false;
                btnTiepTuc.Enabled = false;
            }
            else
            {
                lblThongBao.Text = "Còn " + (3 - trangThai.SoLanSai) + " lần xác thực.";
            }
        }

        private async void btnTiepTuc_Click(object? sender, EventArgs e)
        {
            if (dangKiemTra || trangThai.DaBiKhoa())
            {
                return;
            }
            string traLoi = txtTraLoi.Text.Trim();
            if (traLoi == "")
            {
                lblThongBao.Text = "Vui lòng nhập câu trả lời.";
                txtTraLoi.Focus();
                return;
            }

            dangKiemTra = true;
            btnTiepTuc.Enabled = false;
            btnHuy.Enabled = false;
            this.UseWaitCursor = true;
            try
            {
                bool hopLe;
                if (xemThu)
                {
                    hopLe = KiemTraDuLieuMau(traLoi);
                }
                else
                {
                    hopLe = await DiemDanhDAL.KiemTraXacThucAsync(UserSession.TenDangNhap,
                        maBuoiHoc, trangThai.LoaiCauHoi, traLoi, CancellationToken.None);
                }

                if (hopLe)
                {
                    TraLoi = traLoi;
                    dangKiemTra = false;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    trangThai.GhiNhanTraLoiSai();
                    HienThiSoLanThu();
                    txtTraLoi.SelectAll();
                    txtTraLoi.Focus();
                }
            }
            catch (InvalidOperationException ex)
            {
                lblThongBao.Text = ex.Message;
            }
            catch (Exception)
            {
                lblThongBao.Text = "Chưa xác thực được. Kiểm tra kết nối và thử lại.";
            }
            finally
            {
                dangKiemTra = false;
                if (!this.IsDisposed)
                {
                    this.UseWaitCursor = false;
                    btnHuy.Enabled = true;
                    btnTiepTuc.Enabled = !trangThai.DaBiKhoa();
                }
            }
        }

        private bool KiemTraDuLieuMau(string traLoi)
        {
            if (trangThai.LoaiCauHoi == "NgaySinh")
            {
                DateTime ngaySinh;
                bool dungDinhDang = DateTime.TryParseExact(traLoi, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out ngaySinh);
                return dungDinhDang && ngaySinh == new DateTime(2005, 5, 15);
            }
            return traLoi == "1234";
        }

        private void btnHuy_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmXacThuc_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Chờ truy vấn xong để không bỏ qua kết quả xác thực đang xử lý.
            if (dangKiemTra)
            {
                e.Cancel = true;
            }
        }
    }
}
