using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ClosedXML.Excel;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh
{
    // TV4 – Xuất báo cáo ma trận điểm danh ra Excel (ClosedXML)
    public static class ExportService
    {
        // Mở SaveFileDialog → xuất → hỏi mở ngay
        public static bool ChonFileVaXuatExcel(string maLopHP, IWin32Window owner = null)
        {
            if (string.IsNullOrEmpty(maLopHP)) { Warn("Vui lòng chọn lớp HP!"); return false; }
            using var sfd = new SaveFileDialog {
                Filter = "Excel (*.xlsx)|*.xlsx",
                FileName = $"DiemDanh_{maLopHP}_{DateTime.Now:yyyyMMdd_HHmm}.xlsx",
                Title = "Chọn nơi lưu file Excel"
            };
            if (sfd.ShowDialog(owner) != DialogResult.OK) return false;
            try {
                XuatExcel(maLopHP, sfd.FileName);
                if (MBox("Xuất thành công!\nMở file ngay?", "Xong", true) == DialogResult.Yes)
                    Process.Start(new ProcessStartInfo { FileName = sfd.FileName, UseShellExecute = true });
                return true;
            }
            catch (IOException) { Err("File đang mở trong ứng dụng khác.\nĐóng file rồi thử lại!"); }
            catch (Exception ex) { Err("Lỗi xuất Excel: " + ex.Message); }
            return false;
        }

        // Tạo file Excel: tiêu đề + ma trận buổi (x/m/v/p) + cột tổng kết
        public static void XuatExcel(string maLopHP, string filePath)
        {
            // Lấy dữ liệu
            var dr    = ChuyenCanDAL.LayThongTinChiTietLopHP(maLopHP);
            var dtB   = ChuyenCanDAL.LayDanhSachBuoiHocDaDong(maLopHP);
            var dtSV  = ChuyenCanDAL.LayBangTongKet(maLopHP);
            string lop  = dr?["TenLopHP"]?.ToString() ?? maLopHP;
            string mon  = dr?["TenMon"]?.ToString()   ?? "";
            string gv   = dr?["TenGV"]?.ToString()    ?? "";
            string khoa = dr?["TenKhoa"]?.ToString()  ?? "";
            string ky   = dr?["NamHoc"]?.ToString()   ?? "";
            string hh   = dr?["HocHamHocVi"] is DBNull ? "" : dr?["HocHamHocVi"]?.ToString() ?? "";

            // Tra cứu nhanh trạng thái: "MaBuoi_MaSV" → TrangThai
            var dd = DatabaseConnection.ExecuteQuery(
                "SELECT dd.MaBuoiHoc,dd.MaSV,dd.TrangThai FROM DiemDanh dd " +
                "JOIN BuoiHoc bh ON bh.MaBuoiHoc=dd.MaBuoiHoc WHERE bh.MaLopHP=@ma AND bh.TrangThai=N'DaDong'",
                new[] { new SqlParameter("@ma", maLopHP) });
            var dict = new Dictionary<string, string>();
            foreach (DataRow r in dd.Rows) dict[$"{r["MaBuoiHoc"]}_{r["MaSV"]}"] = r["TrangThai"]?.ToString() ?? "";

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("DiemDanh");

            // Tiêu đề (dòng 1-5)
            SetRow(ws, 1, "TRƯỜNG ĐẠI HỌC CÔNG NGHỆ GIAO THÔNG VẬN TẢI", bold: true);
            SetRow(ws, 2, $"Khoa: {khoa}", bold: true);
            SetRow(ws, 3, "BẢNG ĐIỂM DANH & ĐÁNH GIÁ CHUYÊN CẦN SINH VIÊN", bold: true, size: 13);
            SetRow(ws, 4, $"Lớp HP: {lop} ({maLopHP})  |  Môn: {mon}  |  Học kỳ: {ky}");
            SetRow(ws, 5, $"Giảng viên: {(hh != "" ? hh + ". " : "")}{gv}  |  Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}");

            // Header bảng (dòng 7)
            const int HDR = 7;
            int c = 1;
            foreach (var h in new[] { "STT", "Mã SV", "Họ và Tên", "Ngày sinh", "Lớp HC" })
                ws.Cell(HDR, c++).Value = h;
            for (int i = 0; i < dtB.Rows.Count; i++)
                ws.Cell(HDR, c++).Value = $"B{i+1} ({Convert.ToDateTime(dtB.Rows[i]["NgayHoc"]):dd/MM})";
            foreach (var h in new[] { "Tổng buổi", "Có mặt", "Đi muộn", "Có phép", "Số vắng", "Tỷ lệ (%)", "Điểm CC", "Kết quả" })
                ws.Cell(HDR, c++).Value = h;
            int total = c - 1;

            // Merge + căn giữa tiêu đề
            for (int row = 1; row <= 5; row++)
                ws.Range(row, 1, row, total).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // Style header bảng
            ws.Range(HDR, 1, HDR, total).Style
              .Font.SetBold().Font.SetFontColor(XLColor.White)
              .Fill.SetBackgroundColor(XLColor.FromHtml("#17375E"))
              .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // Dữ liệu sinh viên
            int row2 = HDR + 1;
            for (int i = 0; i < dtSV.Rows.Count; i++, row2++)
            {
                var r = dtSV.Rows[i];
                string sv = r["MaSV"].ToString();
                double tl = Val(r, "TyLeChuyenCan"), dm = Val(r, "DiemChuyenCan");
                bool cam  = r["BiCamThi"] != DBNull.Value && Convert.ToBoolean(r["BiCamThi"]);

                int cc = 1;
                var cSTT  = ws.Cell(row2, cc++); cSTT.Value  = i + 1;                                      cSTT.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                var cMaSV = ws.Cell(row2, cc++); cMaSV.Value = sv;                                         cMaSV.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(row2, cc++).Value = r["HoTen"].ToString();
                var cNS   = ws.Cell(row2, cc++); cNS.Value   = r["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(r["NgaySinh"]).ToString("dd/MM/yyyy") : ""; cNS.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                var cLop  = ws.Cell(row2, cc++); cLop.Value  = r["LopHanhChinh"].ToString();             cLop.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Cột buổi: x=Có mặt | m=Muộn | p=Phép | v=Vắng
                for (int b = 0; b < dtB.Rows.Count; b++)
                {
                    string tt = dict.TryGetValue($"{dtB.Rows[b]["MaBuoiHoc"]}_{sv}", out var vv) ? vv : "Vắng";
                    var cell  = ws.Cell(row2, cc++);
                    cell.Value = tt switch { "Có mặt" => "x", "Đi muộn" => "m", "Nghỉ có phép" => "p", _ => "v" };
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                // Cột tổng kết: Tổng buổi | Có mặt | Đi muộn | Có phép | Số vắng | Tỷ lệ (%) | Điểm CC | Kết quả
                var cTong  = ws.Cell(row2, cc++); cTong.Value  = (int)Val(r, "TongSoBuoi");     cTong.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                var cCoMat = ws.Cell(row2, cc++); cCoMat.Value = (int)Val(r, "SoBuoiCoMat");    cCoMat.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                var cMuon  = ws.Cell(row2, cc++); cMuon.Value  = (int)Val(r, "SoBuoiDiMuon");   cMuon.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                var cPhep  = ws.Cell(row2, cc++); cPhep.Value  = (int)Val(r, "SoBuoiNghiPhep"); cPhep.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                var cVang  = ws.Cell(row2, cc++); cVang.Value  = (int)Val(r, "SoBuoiVang");     cVang.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                var cTyLe  = ws.Cell(row2, cc++); cTyLe.Value  = $"{tl:0.0}%";                  cTyLe.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                var cDiem  = ws.Cell(row2, cc++); cDiem.Value  = Math.Round(dm, 1);             cDiem.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Ghi chú + tô màu dòng
                var gc = ws.Cell(row2, cc++);
                gc.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                if (cam || tl < AppConfig.NguongCamThi) {
                    gc.Value = "CẤM THI"; gc.Style.Font.SetBold().Font.SetFontColor(XLColor.Red);
                    ws.Range(row2, 1, row2, total).Style.Fill.SetBackgroundColor(XLColor.FromHtml("#FFCCCC"));
                } else if (tl < AppConfig.NguongCanhBaoCamThi) {
                    gc.Value = "Cảnh báo"; gc.Style.Font.SetBold().Font.SetFontColor(XLColor.DarkOrange);
                    ws.Range(row2, 1, row2, total).Style.Fill.SetBackgroundColor(XLColor.FromHtml("#FFFACD"));
                } else {
                    gc.Value = "Đạt"; gc.Style.Font.SetBold().Font.SetFontColor(XLColor.DarkGreen);
                }
            }

            // Kẻ viền + chú thích + căn cột + freeze header
            ws.Range(HDR, 1, row2-1, total).Style
              .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
              .Border.SetInsideBorder(XLBorderStyleValues.Thin);
            ws.Cell(row2+1, 1).Value = "Chú thích:  x=Có mặt  |  m=Đi muộn  |  v=Vắng  |  p=Nghỉ có phép";
            ws.Cell(row2+1, 1).Style.Font.Italic = true;
            ws.Columns().AdjustToContents();
            ws.SheetView.FreezeRows(HDR);
            wb.SaveAs(filePath);
        }

        // ── Helper ───────────────────────────────────────────────────
        private static void SetRow(IXLWorksheet ws, int r, string v, bool bold=false, int size=0)
        { ws.Cell(r,1).Value=v; if(bold)ws.Cell(r,1).Style.Font.Bold=true; if(size>0)ws.Cell(r,1).Style.Font.FontSize=size; }

        private static double Val(DataRow r, string col) =>
            r[col] != DBNull.Value ? Convert.ToDouble(r[col]) : 0;

        private static void Warn(string m) => MessageBox.Show(m, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        private static void Err(string m)  => MessageBox.Show(m, "Lỗi",       MessageBoxButtons.OK, MessageBoxIcon.Error);
        private static DialogResult MBox(string m, string t, bool yesno) =>
            MessageBox.Show(m, t, yesno ? MessageBoxButtons.YesNo : MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
