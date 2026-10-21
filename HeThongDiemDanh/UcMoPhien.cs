using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    public partial class UcMoPhien : UserControl
    {
        private const string KiTuOtp = "23456789ABCDEFGHJKMNPQRSTUVWXYZ";
        private string? ipHienTai;
        private string? maskHienTai;
        private string maGV = "";
        private bool dangCapNhat;
        private bool dangThaoTac;
        private bool daDung;
        private DataTable? bangLop;
        private List<ThongTinBuoiHoc> cacPhien = new List<ThongTinBuoiHoc>();
        private Dictionary<int, FrmTrinhChieu> cacManHinh = new Dictionary<int, FrmTrinhChieu>();

        public UcMoPhien()
        {
            InitializeComponent();
        }

        private async void UcMoPhien_Load(object? sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode) return;
            if (!UserSession.LaGiangVien || !UserSession.DaDangNhap) return;
            maGV = UserSession.TenDangNhap;
            KiemTraThongTinMang();
            LoadDanhSachLop();
            timerPhien.Start();
            await CapNhatPhienAsync();
        }

        public void KiemTraThongTinMang()
        {
            ipHienTai = null;
            maskHienTai = null;
            try
            {
                List<ThongTinMang> danhSach = NetworkHelper.LayDanhSachMang();
                if (danhSach.Count > 0)
                {
                    ipHienTai = danhSach[0].DiaChiIP;
                    maskHienTai = danhSach[0].SubnetMask;
                }
                lblIP.Text = ipHienTai ?? "Chưa kết nối Wi-Fi/LAN";
                lblSubnet.Text = maskHienTai ?? "Chưa xác định";
            }
            catch (Exception)
            {
                lblTrangThai.Text = "Chưa đọc được mạng. Kiểm tra Wi-Fi/LAN và thử lại.";
            }
        }

        public void LoadDanhSachLop()
        {
            if (maGV == "" || daDung) return;
            try
            {
                string lopCu = Convert.ToString(cboLopHocPhan.SelectedValue) ?? "";
                DataTable dt = BuoiHocDAL.LayDanhSachLopTheoGiangVien(maGV);
                DataTable? cu = bangLop;
                bangLop = dt;
                cboLopHocPhan.DisplayMember = "TenLopHP";
                cboLopHocPhan.ValueMember = "MaLopHP";
                cboLopHocPhan.DataSource = dt;
                cu?.Dispose();
                foreach (DataRow dong in dt.Rows)
                {
                    if (Convert.ToString(dong["MaLopHP"]) == lopCu) cboLopHocPhan.SelectedValue = lopCu;
                }
                CapNhatThongTinLopDuocChon();
            }
            catch (Exception)
            {
                lblTrangThai.Text = "Chưa tải được lớp học phần. Bấm Làm mới mạng để thử lại.";
            }
        }

        private void cboLopHocPhan_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode) return;
            CapNhatThongTinLopDuocChon();
        }

        private void CapNhatThongTinLopDuocChon()
        {
            DataRowView? lop = cboLopHocPhan.SelectedItem as DataRowView;
            lblSiSo.Text = lop == null ? "0" : Convert.ToString(lop["SiSo"]);
            if (lop != null && maGV != "")
            {
                try
                {
                    int buoi = BuoiHocDAL.LaySoBuoiTiepTheo(Convert.ToString(lop["MaLopHP"]) ?? "");
                    nudBuoiHoc.Value = Math.Max(nudBuoiHoc.Minimum, Math.Min(nudBuoiHoc.Maximum, buoi));
                }
                catch (Exception) { lblTrangThai.Text = "Chưa lấy được số buổi. Hãy kiểm tra kết nối."; }
            }
            CapNhatNut();
        }

        private ThongTinBuoiHoc? PhienDuocChon()
        {
            string maLop = Convert.ToString(cboLopHocPhan.SelectedValue) ?? "";
            foreach (ThongTinBuoiHoc phien in cacPhien)
            {
                if (phien.MaLopHP == maLop) return phien;
            }
            return null;
        }

        private void CapNhatNut()
        {
            ThongTinBuoiHoc? phien = PhienDuocChon();
            btnBatDauDiemDanh.Enabled = !dangThaoTac && phien == null && cboLopHocPhan.SelectedItem != null;
            btnTrinhChieu.Enabled = !dangThaoTac && phien != null;
            btnKetThucPhien.Enabled = !dangThaoTac && phien != null;
            btnTrinhChieu.Text = phien != null && cacManHinh.ContainsKey(phien.MaBuoiHoc) ? "Tắt trình chiếu" : "Bật trình chiếu";
            if (phien != null)
            {
                lblPhien.Text = phien.TenBuoiHoc + " đang mở • Tự kết thúc sau " + phien.ThoiGianDemNguoc
                    + " phút, lúc " + phien.ThoiGianBatDau.AddMinutes(phien.ThoiGianDemNguoc).ToString("HH:mm:ss") + " (giờ máy chủ).";
            }
            else lblPhien.Text = "Lớp đang chọn chưa có phiên mở. Chọn thời gian rồi bấm Bắt đầu điểm danh.";
        }

        private async void timerPhien_Tick(object? sender, EventArgs e)
        {
            await CapNhatPhienAsync();
        }

        private async Task CapNhatPhienAsync()
        {
            if (dangCapNhat || dangThaoTac || daDung || maGV == "") return;
            dangCapNhat = true;
            try
            {
                List<ThongTinBuoiHoc> danhSach = new List<ThongTinBuoiHoc>();
                int soDong = 0;
                await Task.Run(() =>
                {
                    soDong = BuoiHocDAL.TuDongDongPhienQuaHan(maGV);
                    danhSach = BuoiHocDAL.LayCacPhienDangMo(maGV);
                });
                if (IsDisposed || daDung) return;
                cacPhien = danhSach;
                List<int> canTat = new List<int>();
                foreach (int maBuoi in cacManHinh.Keys)
                {
                    bool conMo = false;
                    foreach (ThongTinBuoiHoc phien in danhSach)
                    {
                        if (phien.MaBuoiHoc == maBuoi) conMo = true;
                    }
                    if (!conMo) canTat.Add(maBuoi);
                }
                foreach (int maBuoi in canTat) cacManHinh[maBuoi].Close();
                lblTrangThai.Text = soDong > 0 ? "Đã tự động kết thúc phiên hết giờ và chốt các kết quả còn lại." : "";
                CapNhatNut();
            }
            catch (Exception)
            {
                if (!IsDisposed && !daDung) lblTrangThai.Text = "Chưa đồng bộ được phiên. Hệ thống sẽ tự thử lại khi có kết nối.";
            }
            finally { dangCapNhat = false; }
        }

        private async void btnLamMoiMang_Click(object? sender, EventArgs e)
        {
            KiemTraThongTinMang();
            LoadDanhSachLop();
            await CapNhatPhienAsync();
        }

        private async void btnBatDauDiemDanh_Click(object? sender, EventArgs e)
        {
            if (dangThaoTac || cboLopHocPhan.SelectedValue is not string maLop) return;
            KiemTraThongTinMang();
            if (ipHienTai == null || maskHienTai == null)
            {
                lblTrangThai.Text = "Hãy kết nối Wi-Fi/LAN phòng học trước khi mở phiên.";
                return;
            }
            int soPhut = 10;
            if (rdo5Phut.Checked) soPhut = 5;
            else if (rdo15Phut.Checked) soPhut = 15;
            string tenLop = cboLopHocPhan.Text;
            string tenBuoi = "Buổi " + nudBuoiHoc.Value;
            string ip = ipHienTai;
            string mask = maskHienTai;
            string otp = "";
            int maBuoi = 0;
            dangThaoTac = true;
            cboLopHocPhan.Enabled = false;
            CapNhatNut();
            try
            {
                await Task.Run(() =>
                {
                    BuoiHocDAL.TuDongDongPhienQuaHan(maGV);
                    if (BuoiHocDAL.KiemTraLopDangCoPhienMo(maLop))
                        throw new InvalidOperationException("Lớp đang có phiên mở. Bạn có thể bật trình chiếu hoặc kết thúc phiên đó.");
                    otp = SinhMaOtpKhongTrung();
                    maBuoi = BuoiHocDAL.TaoPhienDiemDanh(maLop, tenBuoi, otp, ip, mask, soPhut);
                });
                if (IsDisposed || daDung) return;
                MoTrinhChieu(maBuoi, tenLop, tenBuoi, otp);
            }
            catch (InvalidOperationException ex) { if (!IsDisposed) lblTrangThai.Text = ex.Message; }
            catch (Exception) { if (!IsDisposed) lblTrangThai.Text = "Chưa mở được phiên. Kiểm tra kết nối máy chủ rồi thử lại."; }
            finally
            {
                dangThaoTac = false;
                if (!IsDisposed && !daDung) { cboLopHocPhan.Enabled = true; CapNhatNut(); }
            }
            await CapNhatPhienAsync();
        }

        private void MoTrinhChieu(int maBuoi, string tenLop, string tenBuoi, string otp)
        {
            if (cacManHinh.ContainsKey(maBuoi)) { cacManHinh[maBuoi].Activate(); return; }
            FrmTrinhChieu frm = new FrmTrinhChieu(maBuoi, tenLop, tenBuoi, otp);
            cacManHinh.Add(maBuoi, frm);
            frm.FormClosed += (s, e) =>
            {
                cacManHinh.Remove(maBuoi);
                if (!IsDisposed && !daDung) CapNhatNut();
            };
            frm.Show(FindForm());
            CapNhatNut();
        }

        private void btnTrinhChieu_Click(object? sender, EventArgs e)
        {
            ThongTinBuoiHoc? phien = PhienDuocChon();
            if (phien == null) return;
            if (cacManHinh.ContainsKey(phien.MaBuoiHoc)) cacManHinh[phien.MaBuoiHoc].Close();
            else MoTrinhChieu(phien.MaBuoiHoc, phien.TenLopHP, phien.TenBuoiHoc, phien.MaOTP);
        }

        private async void btnKetThucPhien_Click(object? sender, EventArgs e)
        {
            ThongTinBuoiHoc? phien = PhienDuocChon();
            if (phien == null || dangThaoTac) return;
            if (MessageBox.Show(this, "Kết thúc " + phien.TenBuoiHoc + " và chốt vắng cho sinh viên chưa điểm danh?",
                "Kết thúc phiên", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            dangThaoTac = true;
            CapNhatNut();
            try
            {
                await Task.Run(() => BuoiHocDAL.DongBuoiHoc(phien.MaBuoiHoc));
            }
            catch (Exception) { if (!IsDisposed) lblTrangThai.Text = "Chưa kết thúc được phiên. Kiểm tra kết nối và thử lại."; }
            finally { dangThaoTac = false; }
            await CapNhatPhienAsync();
        }

        private static string SinhMaOtpKhongTrung()
        {
            for (int lan = 0; lan < 20; lan++)
            {
                char[] kyTu = new char[6];
                for (int i = 0; i < kyTu.Length; i++) kyTu[i] = KiTuOtp[RandomNumberGenerator.GetInt32(KiTuOtp.Length)];
                string otp = new string(kyTu);
                if (!BuoiHocDAL.KiemTraTrungOTP(otp)) return otp;
            }
            throw new InvalidOperationException("Chưa tạo được mã OTP mới. Vui lòng thử lại.");
        }

        public void DungTheoDoi()
        {
            if (daDung) return;
            daDung = true;
            timerPhien.Stop();
            foreach (FrmTrinhChieu frm in new List<FrmTrinhChieu>(cacManHinh.Values)) frm.Close();
            cacManHinh.Clear();
            cboLopHocPhan.DataSource = null;
            bangLop?.Dispose();
            bangLop = null;
        }
    }
}
