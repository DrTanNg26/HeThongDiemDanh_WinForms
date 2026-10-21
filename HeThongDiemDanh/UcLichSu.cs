using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    // TV4 – Tab 2 Sinh viên: Xem lịch sử điểm danh & thống kê chuyên cần
    public partial class UcLichSu : UserControl
    {
        public UcLichSu()
        {
            InitializeComponent();

            // Gắn sự kiện
            btnTaiLai.Click += (s, e) => Load_();
            cboHocKy.SelectedIndexChanged += (s, e) => { LoadMon(); Load_(); };
            cboMonHoc.SelectedIndexChanged += (s, e) => Load_();
            dgv.CellFormatting += DgvCellFormatting;
            Load += (s, e) => LoadBoLoc();
        }

        // Nạp danh sách học kỳ — gọi khi tab được kích hoạt
        public void LoadBoLoc()
        {
            try
            {
                var dt = ChuyenCanDAL.LayDanhSachHocKySinhVien(UserSession.TenDangNhap);
                cboHocKy.Items.Clear();
                cboHocKy.Items.Add("Tất cả");
                foreach (DataRow r in dt.Rows) cboHocKy.Items.Add(r["NamHoc"]);
                if (cboHocKy.Items.Count > 0) cboHocKy.SelectedIndex = 0;
            }
            catch (Exception ex) { Err(ex); }
        }

        // Nạp môn học theo học kỳ đã chọn
        private void LoadMon()
        {
            try
            {
                var dt = ChuyenCanDAL.LayDanhSachMonHocSinhVien(
                    UserSession.TenDangNhap, cboHocKy.SelectedItem?.ToString());
                cboMonHoc.Items.Clear();
                cboMonHoc.Items.Add(new CI("Tất cả môn học", "Tất cả"));
                foreach (DataRow r in dt.Rows)
                    cboMonHoc.Items.Add(new CI($"{r["TenMon"]} ({r["TenLopHP"]})", r["MaLopHP"]?.ToString()));
                if (cboMonHoc.Items.Count > 0) cboMonHoc.SelectedIndex = 0;
            }
            catch (Exception ex) { Err(ex); }
        }

        // Tải dữ liệu, tính thống kê và hiển thị cảnh báo
        public void Load_()
        {
            try
            {
                var dt = ChuyenCanDAL.LayLichSuDiemDanhSinhVien(
                    UserSession.TenDangNhap,
                    (cboMonHoc.SelectedItem as CI)?.Value ?? "Tất cả",
                    cboHocKy.SelectedItem?.ToString());
                dgv.DataSource = dt;
                FmtGrid();

                int tong = dt.Rows.Count, cm = 0, muon = 0, vang = 0, phep = 0;
                foreach (DataRow r in dt.Rows)
                    switch (r["TrangThai"]?.ToString())
                    {
                        case "Có mặt": cm++; break;
                        case "Đi muộn": muon++; break;
                        case "Nghỉ có phép": phep++; break;
                        default: vang++; break;
                    }

                int tinh = tong - phep;
                double tl = tinh > 0 ? Math.Round((cm + muon * .5) / tinh * 100, 1) : 100;
                double dm = ChuyenCanDAL.TinhDiemThang10(tl);
                lblStat.Text = $"Tổng: {tong}  |  Có mặt: {cm}  Muộn: {muon}  Vắng: {vang}  Phép: {phep}" +
                               $"  |  Tỷ lệ: {tl:N1}%  |  Điểm CC: {dm:0.0}";

                if (tl < AppConfig.NguongCamThi) ShowWarn($"BỊ CẤM THI: Tỷ lệ {tl:N1}% < {AppConfig.NguongCamThi}%!");
                else if (tl < AppConfig.NguongCanhBaoCamThi) ShowWarn($"CẢNH BÁO: Tỷ lệ {tl:N1}% – nguy cơ cấm thi!");
                else lblCanhBao.Visible = false;
            }
            catch (Exception ex) { Err(ex); }
        }

        // Ẩn cột không cần thiết, căn chỉnh và định dạng cột
        private void FmtGrid()
        {
            void H(string c) { if (dgv.Columns.Contains(c)) dgv.Columns[c].Visible = false; }
            void F(string c, int w = 0, string fmt = null, bool center = false)
            {
                if (!dgv.Columns.Contains(c)) return;
                var col = dgv.Columns[c];
                if (w > 0) col.Width = w;
                if (fmt != null) col.DefaultCellStyle.Format = fmt;
                if (center) col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            H("MaBuoiHoc"); H("MaLopHP");
            F("STT", 45, null, true);
            F("NgayHoc", 0, "dd/MM/yyyy", true);
            F("ThoiGianQuet", 0, "HH:mm:ss", true);
        }

        // Tô màu chữ cột TrangThai theo giá trị
        private void DgvCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv.Columns[e.ColumnIndex].Name != "TrangThai" || e.Value == null) return;
            e.CellStyle.ForeColor = e.Value.ToString() switch
            {
                "Có mặt" => Color.DarkGreen,
                "Đi muộn" => Color.DarkOrange,
                "Nghỉ có phép" => Color.SteelBlue,
                _ => Color.DarkRed
            };
            e.CellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
        }

        private void ShowWarn(string m) { lblCanhBao.Text = m; lblCanhBao.Visible = true; }
        private static void Err(Exception ex) =>
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

        // ComboItem: hiển thị text, lưu value ẩn (MaLopHP)
        private class CI
        {
            public string Value { get; }
            readonly string _t;
            public CI(string t, string v) { _t = t; Value = v; }
            public override string ToString() => _t;
        }

        private void lblCanhBao_Click(object sender, EventArgs e)
        {

        }
    }
}
