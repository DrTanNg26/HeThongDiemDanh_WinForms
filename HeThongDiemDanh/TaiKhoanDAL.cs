using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh
{
    public static class TaiKhoanDAL
    {
        public static DataTable LayDanhSach(string keyword = null, string vaiTro = null, string trangThai = null)
        {
            string sql = @"SELECT tk.TenDangNhap, tk.VaiTro, tk.TrangThai,
                              COALESCE(sv.HoTen, gv.HoTen, ad.HoTen) AS HoTen,
                              COALESCE(sv.Email, gv.Email, ad.Email) AS Email
                           FROM TaiKhoan tk
                           LEFT JOIN SinhVien sv ON sv.MaSV = tk.TenDangNhap
                           LEFT JOIN GiangVien gv ON gv.MaGV = tk.TenDangNhap
                           LEFT JOIN Admin ad ON ad.MaAD = tk.TenDangNhap
                           WHERE 1=1";

            var parameters = new System.Collections.Generic.List<SqlParameter>();

            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND (tk.TenDangNhap LIKE @kw OR COALESCE(sv.HoTen, gv.HoTen, ad.HoTen) LIKE @kw)";
                parameters.Add(new SqlParameter("@kw", "%" + keyword + "%"));
            }

            if (!string.IsNullOrEmpty(vaiTro) && vaiTro != "Tất cả")
            {
                sql += " AND tk.VaiTro = @vt";
                parameters.Add(new SqlParameter("@vt", vaiTro));
            }

            if (!string.IsNullOrEmpty(trangThai) && trangThai != "Tất cả")
            {
                sql += " AND tk.TrangThai = @tt";
                parameters.Add(new SqlParameter("@tt", trangThai));
            }

            sql += " ORDER BY tk.TenDangNhap";

            return DatabaseConnection.ExecuteQuery(sql, parameters.ToArray());
        }

        public static int ThayDoiTrangThai(string tenDangNhap, string trangThai)
        {
            string sql = "UPDATE TaiKhoan SET TrangThai = @tt WHERE TenDangNhap = @tk";
            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@tt", trangThai),
                new SqlParameter("@tk", tenDangNhap)
            });
        }

        public static int DatLaiMatKhau(string tenDangNhap, string hashMatKhau)
        {
            string sql = "UPDATE TaiKhoan SET MatKhau = @pwd WHERE TenDangNhap = @tk";
            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@pwd", hashMatKhau),
                new SqlParameter("@tk", tenDangNhap)
            });
        }

        public static DataRow LayTheoTen(string tenDangNhap)
        {
            string sql = @"SELECT tk.TenDangNhap, tk.VaiTro, tk.TrangThai, tk.MatKhau,
                              COALESCE(sv.HoTen, gv.HoTen, ad.HoTen) AS HoTen,
                              COALESCE(sv.Email, gv.Email, ad.Email) AS Email
                           FROM TaiKhoan tk
                           LEFT JOIN SinhVien sv ON sv.MaSV = tk.TenDangNhap
                           LEFT JOIN GiangVien gv ON gv.MaGV = tk.TenDangNhap
                           LEFT JOIN Admin ad ON ad.MaAD = tk.TenDangNhap
                           WHERE tk.TenDangNhap = @tk";

            var dt = DatabaseConnection.ExecuteQuery(sql, new[] { new SqlParameter("@tk", tenDangNhap) });
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public static DataRow LayChiTietSinhVien(string maSV)
        {
            string sql = @"SELECT sv.*, tk.TrangThai 
                           FROM SinhVien sv 
                           INNER JOIN TaiKhoan tk ON sv.MaSV = tk.TenDangNhap 
                           WHERE sv.MaSV = @ma";
            var dt = DatabaseConnection.ExecuteQuery(sql, new[] { new SqlParameter("@ma", maSV) });
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public static DataRow LayChiTietGiangVien(string maGV)
        {
            string sql = @"SELECT gv.*, tk.TrangThai 
                           FROM GiangVien gv 
                           INNER JOIN TaiKhoan tk ON gv.MaGV = tk.TenDangNhap 
                           WHERE gv.MaGV = @ma";
            var dt = DatabaseConnection.ExecuteQuery(sql, new[] { new SqlParameter("@ma", maGV) });
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public static DataRow LayChiTietAdmin(string maAD)
        {
            string sql = @"SELECT ad.*, tk.TrangThai 
                           FROM Admin ad 
                           INNER JOIN TaiKhoan tk ON ad.MaAD = tk.TenDangNhap 
                           WHERE ad.MaAD = @ma";
            var dt = DatabaseConnection.ExecuteQuery(sql, new[] { new SqlParameter("@ma", maAD) });
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
    }
}
