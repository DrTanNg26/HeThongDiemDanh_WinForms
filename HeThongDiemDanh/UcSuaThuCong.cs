using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    // TV4 – Tab 2 Giảng viên: Sửa thủ công trạng thái điểm danh (buổi đã đóng)
    // Bắt buộc nhập lý do; lưu NguoiSua + ThoiGianSua vào GhiChu
    public partial class UcSuaThuCong : UserControl
    {
        private string _maSV = "";  // MaSV đang được chọn để sửa

        public UcSuaThuCong()
        {
            InitializeComponent();

            // Gắn sự kiện
            btnTaiLai.Click                  += (s, e) => LoadSV();
            cboLop.SelectedIndexChanged      += (s, e) => LoadBuoi();
            cboBuoi.SelectedIndexChanged     += (s, e) => LoadSV();
            dgv.CellFormatting               += ToMau;
            dgv.SelectionChanged             += OnSelect;
            btnLuu.Click                     += Luu;
            Load                             += (s, e) => TaiDanhSachLop();
        }

        // Nạp lớp HP của giảng viên đăng nhập
        public void TaiDanhSachLop()
        {
            try
            {
                var dt = ChuyenCanDAL.LayDanhSachLopGiangVien(
                    UserSession.LaGiangVien ? UserSession.TenDangNhap : "");
                cboLop.Items.Clear();
                foreach (DataRow r in dt.Rows)
                    cboLop.Items.Add(new CI($"{r["TenLopHP"]} – {r["TenMon"]}", r["MaLopHP"].ToString()));
                if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 0;
            }
            catch (Exception ex) { Err(ex); }
        }

        // Nạp danh sách buổi đã đóng của lớp đang chọn
        private void LoadBuoi()
        {
            if (string.IsNullOrEmpty(Lop)) return;
            try
            {
                var dt = ChuyenCanDAL.LayDanhSachBuoiHocDaDong(Lop);
                cboBuoi.Items.Clear();
                foreach (DataRow r in dt.Rows)
                    cboBuoi.Items.Add(new CI(
                        $"{r["TenBuoiHoc"]} ({Convert.ToDateTime(r["NgayHoc"]):dd/MM})",
                        r["MaBuoiHoc"].ToString()));
                if (cboBuoi.Items.Count > 0) cboBuoi.SelectedIndex = 0;
                else { dgv.DataSource = null; lblChon.Text = "Lớp này chưa có buổi học nào đã đóng."; }
            }
            catch (Exception ex) { Err(ex); }
        }

        // Nạp danh sách điểm danh của buổi đang chọn
        public void LoadSV()
        {
            if (!int.TryParse((cboBuoi.SelectedItem as CI)?.Value, out int mb)) return;
            try
            {
                dgv.DataSource = ChuyenCanDAL.LayDanhSachDiemDanhTheoBuoi(mb, Lop);
                void F(string c, int w = 0, string fmt = null, bool center = false)
                {
                    if (!dgv.Columns.Contains(c)) return;
                    var col = dgv.Columns[c];
                    if (w > 0)     col.Width = w;
                    if (fmt != null) col.DefaultCellStyle.Format = fmt;
                    if (center)    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                F("STT",          45,  null,           true);
                F("MaSV",         90,  null,           true);
                F("LopHanhChinh", 90,  null,           true);
                F("ThoiGianQuet",  0,  "HH:mm:ss",    true);
                F("ThoiGianSua",   0,  "dd/MM HH:mm", true);
            }
            catch (Exception ex) { Err(ex); }
        }

        // Khi chọn dòng → điền sẵn trạng thái & lý do cũ vào form sửa
        private void OnSelect(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) { _maSV = ""; Reset(); return; }
            var r = dgv.SelectedRows[0];
            _maSV        = r.Cells["MaSV"].Value?.ToString() ?? "";
            lblChon.Text = $"Đang sửa: [{_maSV}] {r.Cells["HoTen"].Value}  –  Hiện: {r.Cells["TrangThai"].Value}";
            lblChon.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            lblChon.ForeColor = SystemColors.ControlText;
            cbTT.SelectedItem = r.Cells["TrangThai"].Value?.ToString();
            txtLyDo.Text      = r.Cells["GhiChu"].Value?.ToString() ?? "";
        }

        // Lưu: validate → cập nhật DB → làm mới, giữ chọn dòng vừa sửa
        private void Luu(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_maSV))
                { Warn("Chưa chọn sinh viên!"); return; }
            if (!int.TryParse((cboBuoi.SelectedItem as CI)?.Value, out int mb))
                { Warn("Chưa chọn buổi!"); return; }
            if (string.IsNullOrEmpty(txtLyDo.Text.Trim()))
                { Warn("Bắt buộc nhập lý do!"); txtLyDo.Focus(); return; }
            try
            {
                ChuyenCanDAL.CapNhatDiemDanhThuCong(
                    mb, _maSV, cbTT.SelectedItem?.ToString() ?? "Có mặt",
                    txtLyDo.Text.Trim(), UserSession.TenDangNhap);
                MessageBox.Show($"Đã cập nhật [{_maSV}]", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                string keep = _maSV;
                LoadSV();
                foreach (DataGridViewRow row in dgv.Rows)
                    if (row.Cells["MaSV"].Value?.ToString() == keep) { row.Selected = true; break; }
            }
            catch (Exception ex) { Err(ex); }
        }

        // Tô màu chữ ô TrangThai
        private void ToMau(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv.Columns[e.ColumnIndex].Name != "TrangThai" || e.Value == null) return;
            e.CellStyle.ForeColor = e.Value.ToString() switch
            {
                "Có mặt"       => Color.DarkGreen,
                "Đi muộn"     => Color.DarkOrange,
                "Nghỉ có phép" => Color.SteelBlue,
                _               => Color.DarkRed
            };
            e.CellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
        }

        // Reset panel sửa về trạng thái ban đầu
        private void Reset()
        {
            lblChon.Text      = "→ Chọn sinh viên cần sửa";
            lblChon.Font      = new Font("Segoe UI", 9f, FontStyle.Italic);
            lblChon.ForeColor = Color.Gray;
            txtLyDo.Clear();
        }

        private string Lop => (cboLop.SelectedItem as CI)?.Value ?? "";
        private static void Err(Exception ex) =>
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        private static void Warn(string m) =>
            MessageBox.Show(m, "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        // ComboItem: hiển thị text, lưu value ẩn
        private class CI
        {
            public string Value { get; }
            readonly string _t;
            public CI(string t, string v) { _t = t; Value = v; }
            public override string ToString() => _t;
        }
    }
}
