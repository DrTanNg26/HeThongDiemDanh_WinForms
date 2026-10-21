using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    // TV4 – Tab 3 Giảng viên: Tổng kết chuyên cần & xuất báo cáo Excel
    // Nút "Tổng kết" chỉ bật khi tất cả buổi học của lớp đã đóng
    public partial class UcTongKet : UserControl
    {
        public UcTongKet()
        {
            InitializeComponent();

            // Gắn sự kiện
            btnTaiLai.Click += (s, e) => Load_();
            btnTK.Click += BtnTK_Click;
            btnXLS.Click += (s, e) => { var m = Lop; if (m != "") ExportService.ChonFileVaXuatExcel(m, this); };
            cboLop.SelectedIndexChanged += (s, e) => Load_();
            dgv.CellFormatting += ToMau;
            Load += (s, e) => TaiDanhSachLop();
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

        // Kiểm tra trạng thái lớp, tải bảng tổng kết và thống kê nhanh
        public void Load_()
        {
            string m = Lop;
            if (string.IsNullOrEmpty(m)) { dgv.DataSource = null; return; }
            try
            {
                bool coMo = ChuyenCanDAL.CoBuoiHocDangMo(m);
                btnTK.Enabled = !coMo;
                lblTT.Text = coMo ? "Còn buổi đang mở – cần đóng trước" : "Tất cả buổi đã đóng";
                lblTT.ForeColor = coMo ? Color.DarkRed : Color.DarkGreen;

                var dt = ChuyenCanDAL.LayBangTongKet(m);
                dgv.DataSource = dt;
                FmtGrid();

                int tot = dt.Rows.Count, dat = 0, cb = 0, cam = 0;
                foreach (DataRow r in dt.Rows)
                {
                    double tl = r["TyLeChuyenCan"] != DBNull.Value ? Convert.ToDouble(r["TyLeChuyenCan"]) : 0;
                    bool bc = r["BiCamThi"] != DBNull.Value && Convert.ToBoolean(r["BiCamThi"]);
                    if (bc || tl < AppConfig.NguongCamThi) cam++;
                    else if (tl < AppConfig.NguongCanhBaoCamThi) cb++;
                    else dat++;
                }
                lblStat.Text = $"Tổng SV: {tot}  |  Đạt: {dat}  Cảnh báo: {cb}  Cấm thi: {cam}";
                lblStat.ForeColor = cam > 0 ? Color.DarkRed : SystemColors.ControlText;
            }
            catch (Exception ex) { Err(ex); }
        }

        // Ẩn cột nội bộ, định dạng và đặt header tiếng Việt rõ ràng
        private void FmtGrid()
        {
            void H(string c) { if (dgv.Columns.Contains(c)) dgv.Columns[c].Visible = false; }
            void F(string c, int w = 0, string fmt = null, bool center = false, string header = null)
            {
                if (!dgv.Columns.Contains(c)) return;
                var col = dgv.Columns[c];
                if (w > 0) col.Width = w;
                if (fmt != null) col.DefaultCellStyle.Format = fmt;
                if (header != null) col.HeaderText = header;
                if (center) col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            H("MaLopHP"); H("BiCamThi");
            F("STT", 45, null, true, "STT");
            F("MaSV", 85, null, true, "Mã SV");
            F("HoTen", 150, null, false, "Họ và tên");
            F("NgaySinh", 90, "dd/MM/yyyy", true, "Ngày sinh");
            F("LopHanhChinh", 85, null, true, "Lớp HC");
            F("TongSoBuoi", 75, null, true, "Tổng buổi");
            F("SoBuoiCoMat", 70, null, true, "Có mặt");
            F("SoBuoiDiMuon", 70, null, true, "Đi muộn");
            F("SoBuoiVang", 65, null, true, "Vắng");
            F("SoBuoiNghiPhep", 75, null, true, "Nghỉ phép");
            F("TyLeChuyenCan", 85, "0.0\\%", true, "Tỷ lệ (%)");
            F("DiemChuyenCan", 70, "0.0", true, "Điểm CC");
            F("KetQua", 85, null, true, "Kết quả");
            F("NgayTongKet", 90, "dd/MM/yyyy", true, "Ngày TK");
        }

        // Gọi SP tổng kết học kỳ (MERGE → chạy lại không ghi trùng)
        private void BtnTK_Click(object sender, EventArgs e)
        {
            string m = Lop; if (string.IsNullOrEmpty(m)) return;
            if (MessageBox.Show($"Tổng kết lớp [{m}]?\nKết quả lưu vào KetQuaChuyenCan.", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                ChuyenCanDAL.ThucHienTongKetChuyenCan(m);
                MessageBox.Show("Tổng kết thành công!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Load_();
            }
            catch (Exception ex) { Err(ex); }
        }

        // Tô nền hàng & format hiển thị (tỷ lệ %, màu kết quả)
        private void ToMau(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv.Rows.Count) return;
            var row = dgv.Rows[e.RowIndex];
            var cells = row.Cells;
            string col = dgv.Columns[e.ColumnIndex].Name;

            // Hiển thị ký hiệu % rõ ràng cho cột Tỷ lệ chuyên cần
            if (col == "TyLeChuyenCan" && e.Value != null && double.TryParse(e.Value.ToString(), out double tlVal))
            {
                e.Value = $"{tlVal:0.0}%";
                e.FormattingApplied = true;
            }

            // Màu chữ nổi bật cho cột Kết quả
            if (col == "KetQua" && e.Value != null)
            {
                string kq = e.Value.ToString();
                e.CellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
                e.CellStyle.ForeColor = kq == "Cấm thi" ? Color.DarkRed :
                                        kq == "Cảnh báo" ? Color.DarkOrange : Color.DarkGreen;
            }

            // Tô nền hàng: đỏ nhạt = cấm thi, vàng nhạt = cảnh báo
            bool bc = cells["BiCamThi"].Value != null && cells["BiCamThi"].Value != DBNull.Value
                        && Convert.ToBoolean(cells["BiCamThi"].Value);
            double tl = cells["TyLeChuyenCan"].Value != null && cells["TyLeChuyenCan"].Value != DBNull.Value
                        ? Convert.ToDouble(cells["TyLeChuyenCan"].Value) : 100;
            if (bc || tl < AppConfig.NguongCamThi)
                row.DefaultCellStyle.BackColor = Color.MistyRose;
            else if (tl < AppConfig.NguongCanhBaoCamThi)
                row.DefaultCellStyle.BackColor = Color.LightYellow;
        }

        private string Lop => (cboLop.SelectedItem as CI)?.Value ?? "";
        private static void Err(Exception ex) =>
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
