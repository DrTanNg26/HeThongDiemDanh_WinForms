using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh
{
    public static class LopHanhChinhDAL
    {
        public static DataTable LayDanhSach()
        {
            // Join with Khoa to display TenKhoa
            string sql = @"SELECT l.MaLop, l.TenLop, l.MaKhoa, k.TenKhoa
                           FROM LopHanhChinh l
                           LEFT JOIN Khoa k ON k.MaKhoa = l.MaKhoa
                           ORDER BY l.MaLop";
            return DatabaseConnection.ExecuteQuery(sql);
        }

        public static int Them(string maLop, string tenLop, string maKhoa)
        {
            string sql = "INSERT INTO LopHanhChinh(MaLop, TenLop, MaKhoa) VALUES(@ma, @ten, @makhoa)";
            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@ma", maLop),
                new SqlParameter("@ten", tenLop),
                new SqlParameter("@makhoa", maKhoa)
            });
        }

        public static int Sua(string maLop, string tenLop, string maKhoa)
        {
            string sql = "UPDATE LopHanhChinh SET TenLop = @ten, MaKhoa = @makhoa WHERE MaLop = @ma";
            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@ten", tenLop),
                new SqlParameter("@makhoa", maKhoa),
                new SqlParameter("@ma", maLop)
            });
        }

        public static int Xoa(string maLop)
        {
            string sql = "DELETE FROM LopHanhChinh WHERE MaLop = @ma";
            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@ma", maLop)
            });
        }

        public static bool HasDependencies(string maLop)
        {
            try
            {
                object o = DatabaseConnection.ExecuteScalar("SELECT COUNT(1) FROM ChiTietLopHP WHERE MaLop = @ma", new[] { new SqlParameter("@ma", maLop) });
                int c = Convert.ToInt32(o ?? 0);
                return c > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
