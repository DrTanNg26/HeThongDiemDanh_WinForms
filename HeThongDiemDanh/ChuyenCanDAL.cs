using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh
{
    // TV4 – DAL cho UcLichSu (SV), UcSuaThuCong và UcTongKet (GV)
    public static class ChuyenCanDAL
    {
        // ── Sinh viên ────────────────────────────────────────────────

        // Danh sách học kỳ (NamHoc) SV đã học
        public static DataTable LayDanhSachHocKySinhVien(string maSV) => DB(
            "SELECT DISTINCT lhp.NamHoc FROM LopHocPhan lhp " +
            "JOIN ChiTietLopHP ct ON ct.MaLopHP=lhp.MaLopHP WHERE ct.MaSV=@maSV ORDER BY lhp.NamHoc DESC",
            P("@maSV", maSV));

        // Danh sách môn học / lớp HP của SV, lọc thêm theo học kỳ nếu cần
        public static DataTable LayDanhSachMonHocSinhVien(string maSV, string namHoc = null)
        {
            string sql = "SELECT lhp.MaLopHP,lhp.TenLopHP,mh.TenMon,lhp.NamHoc FROM LopHocPhan lhp " +
                         "JOIN MonHoc mh ON mh.MaMon=lhp.MaMon JOIN ChiTietLopHP ct ON ct.MaLopHP=lhp.MaLopHP " +
                         "WHERE ct.MaSV=@maSV";
            var p = new List<SqlParameter>(P("@maSV", maSV));
            if (!string.IsNullOrEmpty(namHoc) && namHoc != "Tất cả")
            { sql += " AND lhp.NamHoc=@nh"; p.Add(new SqlParameter("@nh", namHoc)); }
            return DB(sql + " ORDER BY mh.TenMon", p.ToArray());
        }

        // Lịch sử điểm danh từng buổi ĐÃ ĐÓNG — lọc lớp HP / học kỳ
        public static DataTable LayLichSuDiemDanhSinhVien(string maSV, string maLopHP = null, string namHoc = null)
        {
            string sql =
                "SELECT ROW_NUMBER() OVER(ORDER BY bh.NgayHoc,bh.MaBuoiHoc) AS STT," +
                "mh.TenMon,lhp.TenLopHP,lhp.MaLopHP,lhp.NamHoc,bh.MaBuoiHoc,bh.TenBuoiHoc,bh.NgayHoc," +
                "dd.ThoiGianQuet,ISNULL(dd.TrangThai,N'Vắng') AS TrangThai,dd.IP_SV,dd.GhiChu,dd.NguoiSua,dd.ThoiGianSua " +
                "FROM BuoiHoc bh JOIN LopHocPhan lhp ON lhp.MaLopHP=bh.MaLopHP " +
                "JOIN MonHoc mh ON mh.MaMon=lhp.MaMon " +
                "JOIN ChiTietLopHP ct ON ct.MaLopHP=lhp.MaLopHP AND ct.MaSV=@maSV " +
                "LEFT JOIN DiemDanh dd ON dd.MaBuoiHoc=bh.MaBuoiHoc AND dd.MaSV=@maSV " +
                "WHERE bh.TrangThai=N'DaDong'";
            var p = new List<SqlParameter>(P("@maSV", maSV));
            if (!string.IsNullOrEmpty(maLopHP) && maLopHP != "Tất cả")
            { sql += " AND lhp.MaLopHP=@lhp"; p.Add(new SqlParameter("@lhp", maLopHP)); }
            if (!string.IsNullOrEmpty(namHoc) && namHoc != "Tất cả")
            { sql += " AND lhp.NamHoc=@nh"; p.Add(new SqlParameter("@nh", namHoc)); }
            return DB(sql + " ORDER BY bh.NgayHoc,bh.MaBuoiHoc", p.ToArray());
        }

        // ── Giảng viên ───────────────────────────────────────────────

        // Lớp HP do GV phụ trách (maGV rỗng = tất cả)
        public static DataTable LayDanhSachLopGiangVien(string maGV) => DB(
            "SELECT lhp.MaLopHP,lhp.TenLopHP,mh.TenMon,lhp.NamHoc FROM LopHocPhan lhp " +
            "JOIN MonHoc mh ON mh.MaMon=lhp.MaMon " +
            "WHERE (@maGV IS NULL OR @maGV='' OR lhp.MaGV=@maGV) ORDER BY lhp.NamHoc DESC,lhp.TenLopHP",
            P("@maGV", string.IsNullOrEmpty(maGV) ? (object)DBNull.Value : maGV));

        // Buổi học đã đóng của lớp HP (để GV điều chỉnh thủ công)
        public static DataTable LayDanhSachBuoiHocDaDong(string maLopHP) => DB(
            "SELECT MaBuoiHoc,TenBuoiHoc,NgayHoc FROM BuoiHoc " +
            "WHERE MaLopHP=@ma AND TrangThai=N'DaDong' ORDER BY NgayHoc,MaBuoiHoc",
            P("@ma", maLopHP));

        // Danh sách điểm danh tất cả SV trong 1 buổi (ISNULL → Vắng)
        public static DataTable LayDanhSachDiemDanhTheoBuoi(int maBuoi, string maLopHP) => DB(
            "SELECT ROW_NUMBER() OVER(ORDER BY sv.MaSV) AS STT,sv.MaSV,sv.HoTen,sv.MaLop AS LopHanhChinh," +
            "ISNULL(dd.TrangThai,N'Vắng') AS TrangThai,dd.ThoiGianQuet,dd.IP_SV,dd.GhiChu,dd.NguoiSua,dd.ThoiGianSua " +
            "FROM ChiTietLopHP ct JOIN SinhVien sv ON sv.MaSV=ct.MaSV " +
            "LEFT JOIN DiemDanh dd ON dd.MaSV=ct.MaSV AND dd.MaBuoiHoc=@mb " +
            "WHERE ct.MaLopHP=@lhp ORDER BY sv.MaSV",
            new[] { new SqlParameter("@mb", maBuoi), new SqlParameter("@lhp", maLopHP) });

        // UPSERT điểm danh thủ công — bắt buộc có lý do (GhiChu) + NguoiSua + ThoiGianSua
        public static int CapNhatDiemDanhThuCong(int maBuoi, string maSV, string tt, string ghiChu, string nguoiSua) =>
            DatabaseConnection.ExecuteNonQuery(
                "IF EXISTS(SELECT 1 FROM DiemDanh WHERE MaBuoiHoc=@mb AND MaSV=@sv) " +
                "UPDATE DiemDanh SET TrangThai=@tt,GhiChu=@gc,NguoiSua=@ns,ThoiGianSua=GETDATE() WHERE MaBuoiHoc=@mb AND MaSV=@sv; " +
                "ELSE INSERT INTO DiemDanh(MaBuoiHoc,MaSV,TrangThai,GhiChu,NguoiSua,ThoiGianSua) VALUES(@mb,@sv,@tt,@gc,@ns,GETDATE());",
                new[] { new SqlParameter("@mb",maBuoi), new SqlParameter("@sv",maSV),
                        new SqlParameter("@tt",tt), new SqlParameter("@gc",(object)ghiChu??DBNull.Value),
                        new SqlParameter("@ns",(object)nguoiSua??DBNull.Value) });

        // Kiểm tra lớp còn buổi nào đang mở (TrangThai='Mo') không
        public static bool CoBuoiHocDangMo(string maLopHP) =>
            Convert.ToInt32(DatabaseConnection.ExecuteScalar(
                "SELECT COUNT(1) FROM BuoiHoc WHERE MaLopHP=@ma AND TrangThai=N'Mo'", P("@ma", maLopHP)) ?? 0) > 0;

        // Gọi SP tổng kết — MERGE nên chạy lại nhiều lần không trùng dữ liệu
        public static void ThucHienTongKetChuyenCan(string maLopHP) =>
            DatabaseConnection.ExecuteNonQuery("EXEC sp_TongKetChuyenCan @MaLopHP=@ma", P("@ma", maLopHP));

        // Lấy bảng tổng kết; nếu chưa tổng kết → tính tạm để xem trước
        public static DataTable LayBangTongKet(string maLopHP)
        {
            var dt = DB(
                "SELECT ROW_NUMBER() OVER(ORDER BY sv.MaSV) AS STT,kq.MaSV,sv.HoTen,sv.NgaySinh,sv.MaLop AS LopHanhChinh," +
                "kq.TongSoBuoi,kq.SoBuoiCoMat,kq.SoBuoiDiMuon,kq.SoBuoiVang,kq.SoBuoiNghiPhep," +
                "kq.TyLeChuyenCan,kq.DiemChuyenCan,kq.BiCamThi," +
                "CASE WHEN kq.BiCamThi=1 OR kq.TyLeChuyenCan<50.0 THEN N'Cấm thi' " +
                "WHEN kq.TyLeChuyenCan<70.0 THEN N'Cảnh báo' ELSE N'Đạt' END AS KetQua," +
                "kq.NgayTongKet " +
                "FROM KetQuaChuyenCan kq JOIN SinhVien sv ON sv.MaSV=kq.MaSV WHERE kq.MaLopHP=@ma ORDER BY sv.MaSV",
                P("@ma", maLopHP));
            if (dt.Rows.Count > 0) return dt;

            // Tính tạm: % = (CoMat + Muon*0.5) / (Tong - NghiPhep) * 100
            dt = DB(
                ";WITH T AS(SELECT ct.MaSV,(SELECT COUNT(*) FROM BuoiHoc WHERE MaLopHP=@ma AND TrangThai=N'DaDong') AS Tong," +
                "ISNULL(SUM(CASE WHEN dd.TrangThai=N'Có mặt' THEN 1 ELSE 0 END),0) AS CoMat," +
                "ISNULL(SUM(CASE WHEN dd.TrangThai=N'Đi muộn' THEN 1 ELSE 0 END),0) AS Muon," +
                "ISNULL(SUM(CASE WHEN bh.MaBuoiHoc IS NOT NULL AND (dd.TrangThai IS NULL OR dd.TrangThai=N'Vắng') THEN 1 ELSE 0 END),0) AS Vang," +
                "ISNULL(SUM(CASE WHEN dd.TrangThai=N'Nghỉ có phép' THEN 1 ELSE 0 END),0) AS Phep " +
                "FROM ChiTietLopHP ct LEFT JOIN BuoiHoc bh ON bh.MaLopHP=ct.MaLopHP AND bh.TrangThai=N'DaDong' " +
                "LEFT JOIN DiemDanh dd ON dd.MaBuoiHoc=bh.MaBuoiHoc AND dd.MaSV=ct.MaSV WHERE ct.MaLopHP=@ma GROUP BY ct.MaSV)" +
                "SELECT ROW_NUMBER() OVER(ORDER BY sv.MaSV) AS STT,t.MaSV,sv.HoTen,sv.NgaySinh,sv.MaLop AS LopHanhChinh," +
                "t.Tong AS TongSoBuoi,t.CoMat AS SoBuoiCoMat,t.Muon AS SoBuoiDiMuon,t.Vang AS SoBuoiVang,t.Phep AS SoBuoiNghiPhep," +
                "CASE WHEN(t.Tong-t.Phep)>0 THEN ROUND(((t.CoMat+t.Muon*0.5)/(t.Tong-t.Phep))*100.0,2) ELSE 100.0 END AS TyLeChuyenCan," +
                "CAST(0 AS FLOAT) AS DiemChuyenCan,CAST(0 AS BIT) AS BiCamThi," +
                "CAST(N'Chưa tổng kết' AS NVARCHAR(20)) AS KetQua,CAST(NULL AS DATETIME) AS NgayTongKet " +
                "FROM T t JOIN SinhVien sv ON sv.MaSV=t.MaSV ORDER BY sv.MaSV", P("@ma", maLopHP));

            foreach (DataRow r in dt.Rows)
            {
                double tl = r["TyLeChuyenCan"] != DBNull.Value ? Convert.ToDouble(r["TyLeChuyenCan"]) : 0;
                r["DiemChuyenCan"] = TinhDiemThang10(tl);
                bool cam = tl < AppConfig.NguongCamThi;
                r["BiCamThi"] = cam;
                r["KetQua"] = cam ? "Cấm thi" : tl < AppConfig.NguongCanhBaoCamThi ? "Cảnh báo" : "Đạt";
            }
            return dt;
        }

        // Quy đổi % → điểm 10: ≥90→10 | ≥80→9 | ≥70→8 | ≥50→6 | <50→0 (cấm thi)
        public static double TinhDiemThang10(double tl) =>
            tl >= AppConfig.Nguong90 ? 10 : tl >= AppConfig.Nguong80 ? 9 :
            tl >= AppConfig.Nguong70 ? 8  : tl >= AppConfig.NguongCamThi ? 6 : 0;

        // Thông tin lớp HP đầy đủ (môn, GV, khoa) — dùng cho tiêu đề Excel
        public static DataRow LayThongTinChiTietLopHP(string maLopHP)
        {
            var dt = DB(
                "SELECT lhp.*,mh.TenMon,gv.HoTen AS TenGV,gv.HocHamHocVi,k.TenKhoa FROM LopHocPhan lhp " +
                "JOIN MonHoc mh ON mh.MaMon=lhp.MaMon JOIN GiangVien gv ON gv.MaGV=lhp.MaGV " +
                "JOIN Khoa k ON k.MaKhoa=gv.MaKhoa WHERE lhp.MaLopHP=@ma", P("@ma", maLopHP));
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        // Helper ngắn gọn
        private static DataTable DB(string sql, SqlParameter[] p = null) =>
            DatabaseConnection.ExecuteQuery(sql, p);
        private static SqlParameter[] P(string n, object v) => new[] { new SqlParameter(n, v) };
    }
}
