using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    public partial class UcMoPhien : UserControl
    {
        // Bộ ký tự sinh mã OTP an toàn: Đã loại trừ ký tự dễ nhầm lẫn (0, O, 1, I, L)
        private const string KiTuOtp = "23456789ABCDEFGHJKMNPQRSTUVWXYZ";

        private string? _currentIp;
        private string? _currentSubnet;
        private DataTable? _dtLop;

        public UcMoPhien()
        {
            InitializeComponent();
        }

        private void UcMoPhien_Load(object? sender, EventArgs e)
        {
            try { BuoiHocDAL.TuDongDongPhienQuaHan(); } catch { }
            KiemTraThongTinMang();
            LoadDanhSachLop();
        }

        // 1. Kiểm tra thông tin Wi-Fi / LAN của máy Giảng viên
        public void KiemTraThongTinMang()
        {
            try
            {
                List<ThongTinMang> danhSach = NetworkHelper.LayDanhSachMang();
                if (danhSach != null && danhSach.Count > 0)
                {
                    _currentIp = danhSach[0].DiaChiIP;
                    _currentSubnet = danhSach[0].SubnetMask;

                    lblIP.Text = _currentIp;
                    lblSubnet.Text = _currentSubnet;
                    lblTrangThai.Text = "";
                }
                else
                {
                    _currentIp = null;
                    _currentSubnet = null;

                    // Nếu bật AppConfig.TestMode thì cho phép dùng localhost
                    if (AppConfig.TestMode)
                    {
                        _currentIp = "127.0.0.1";
                        _currentSubnet = "255.255.255.0";
                        lblIP.Text = "127.0.0.1 (Chế độ Test)";
                        lblSubnet.Text = "255.255.255.0";
                        lblTrangThai.Text = "Đang chạy ở chế độ TestMode (chấp nhận test trên 1 máy).";
                    }
                    else
                    {
                        lblIP.Text = "Chưa kết nối Wi-Fi/LAN";
                        lblSubnet.Text = "Không xác định";
                        lblTrangThai.Text = "Cảnh báo: Máy tính chưa kết nối Wi-Fi hoặc mạng LAN!";
                    }
                }
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = "Lỗi kiểm tra mạng: " + ex.Message;
            }
        }

        // 2. Tải danh sách lớp học phần do Giảng viên phụ trách
        public void LoadDanhSachLop()
        {
            try
            {
                string maGV = UserSession.TenDangNhap;
                if (string.IsNullOrEmpty(maGV))
                {
                    lblTrangThai.Text = "Không tìm thấy thông tin giảng viên đăng nhập.";
                    return;
                }

                _dtLop = BuoiHocDAL.LayDanhSachLopTheoGiangVien(maGV);
                cboLopHocPhan.DataSource = _dtLop;
                cboLopHocPhan.DisplayMember = "TenLopHP";
                cboLopHocPhan.ValueMember = "MaLopHP";

                if (_dtLop.Rows.Count > 0)
                {
                    cboLopHocPhan.SelectedIndex = 0;
                    CapNhatThongTinLopDuocChon();
                }
                else
                {
                    lblSiSo.Text = "0";
                    lblTrangThai.Text = "Giảng viên chưa được phân công lớp học phần nào.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách lớp học phần: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboLopHocPhan_SelectedIndexChanged(object? sender, EventArgs e)
        {
            CapNhatThongTinLopDuocChon();
        }

        private void CapNhatThongTinLopDuocChon()
        {
            if (cboLopHocPhan.SelectedValue is string maLopHP && !string.IsNullOrEmpty(maLopHP))
            {
                // Cập nhật sĩ số
                int siSo = BuoiHocDAL.LaySiSoLop(maLopHP);
                lblSiSo.Text = siSo.ToString();

                // Tự động sinh buổi học tiếp theo
                int buoiTiepTheo = BuoiHocDAL.LaySoBuoiTiepTheo(maLopHP);
                nudBuoiHoc.Value = Math.Max(1, Math.Min(nudBuoiHoc.Maximum, buoiTiepTheo));
            }
        }

        private void btnLamMoiMang_Click(object? sender, EventArgs e)
        {
            KiemTraThongTinMang();
        }

        // 3. Xử lý nút "Bắt đầu điểm danh"
        private void btnBatDauDiemDanh_Click(object? sender, EventArgs e)
        {
            // Kiểm tra chọn lớp
            if (cboLopHocPhan.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần cần mở điểm danh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLopHP = cboLopHocPhan.SelectedValue.ToString()!;
            string tenLopHP = cboLopHocPhan.Text;

            // Kiểm tra kết nối mạng Wi-Fi/LAN
            KiemTraThongTinMang();
            if (string.IsNullOrEmpty(_currentIp))
            {
                MessageBox.Show("Không tìm thấy kết nối Wi-Fi/LAN. Vui lòng kết nối mạng trước khi mở phiên điểm danh!", 
                                "Cảnh báo mạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem lớp học phần đã có phiên nào đang mở không
            if (BuoiHocDAL.KiemTraLopDangCoPhienMo(maLopHP))
            {
                MessageBox.Show($"Lớp học phần '{tenLopHP}' hiện đang có một phiên điểm danh đang mở chưa kết thúc.\n" +
                                "Vui lòng đóng phiên hiện tại trước khi tạo phiên mới!", 
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác định thời gian đếm ngược
            int thoiGianDemNguoc = 10;
            if (rdo5Phut.Checked) thoiGianDemNguoc = 5;
            else if (rdo15Phut.Checked) thoiGianDemNguoc = 15;

            int soBuoi = (int)nudBuoiHoc.Value;
            string tenBuoiHoc = $"Buổi {soBuoi}";

            // Sinh mã OTP an toàn 6 ký tự không trùng
            string maOTP = SinhMaOtpKhongTrung();

            try
            {
                // Ghi vào CSDL SQL
                int maBuoiHoc = BuoiHocDAL.TaoPhienDiemDanh(
                    maLopHP,
                    tenBuoiHoc,
                    maOTP,
                    _currentIp,
                    _currentSubnet ?? "",
                    thoiGianDemNguoc
                );

                if (maBuoiHoc <= 0)
                {
                    MessageBox.Show("Không thể tạo phiên điểm danh. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Đã mở phiên điểm danh thành công!\nMã OTP: {maOTP}\nThời gian: {thoiGianDemNguoc} phút.",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mở màn hình Trình chiếu FrmTrinhChieu
                FrmTrinhChieu frmTrinhChieu = new FrmTrinhChieu(maBuoiHoc, tenLopHP, tenBuoiHoc, maOTP);
                frmTrinhChieu.FormClosed += (s, args) =>
                {
                    // Khi đóng màn hình chiếu, làm mới lại thông tin lớp
                    CapNhatThongTinLopDuocChon();
                };
                frmTrinhChieu.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở phiên điểm danh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 4. Hàm sinh OTP 6 ký tự từ bộ ký tự bỏ 0/O, 1/I và kiểm tra không trùng
        private string SinhMaOtpKhongTrung()
        {
            string otp;
            int soLanThu = 0;
            do
            {
                otp = SinhChuoiNgauNhien(6);
                soLanThu++;
                if (soLanThu > 20) break; // Tránh loop vô tận
            }
            while (BuoiHocDAL.KiemTraTrungOTP(otp));

            return otp;
        }

        private static string SinhChuoiNgauNhien(int length)
        {
            char[] chars = new char[length];
            byte[] randomBytes = new byte[length];
            RandomNumberGenerator.Fill(randomBytes);

            for (int i = 0; i < length; i++)
            {
                chars[i] = KiTuOtp[randomBytes[i] % KiTuOtp.Length];
            }

            return new string(chars);
        }
    }
}
