using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh
{
    public static class KhoaDAL
    {
        public static DataTable LayDanhSach()
        {
            string sql = "SELECT MaKhoa, TenKhoa FROM Khoa ORDER BY MaKhoa";
            return DatabaseConnection.ExecuteQuery(sql);
        }

        public static int Them(string maKhoa, string tenKhoa)
        {
            string sql = "INSERT INTO Khoa(MaKhoa, TenKhoa) VALUES(@ma, @ten)";
            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@ma", maKhoa),
                new SqlParameter("@ten", tenKhoa)
            });
        }

        public static int Sua(string maKhoa, string tenKhoa)
        {
            string sql = "UPDATE Khoa SET TenKhoa = @ten WHERE MaKhoa = @ma";
            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@ten", tenKhoa),
                new SqlParameter("@ma", maKhoa)
            });
        }

        public static int Xoa(string maKhoa)
        {
            string sql = "DELETE FROM Khoa WHERE MaKhoa = @ma";
            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@ma", maKhoa)
            });
        }

        public static bool HasDependencies(string maKhoa)
        {
            // Check LopHanhChinh
            try
            {
                object o1 = DatabaseConnection.ExecuteScalar("SELECT COUNT(1) FROM LopHanhChinh WHERE MaKhoa = @ma", new[] { new SqlParameter("@ma", maKhoa) });
                int c1 = Convert.ToInt32(o1 ?? 0);
                if (c1 > 0) return true;

                object o2 = DatabaseConnection.ExecuteScalar("SELECT COUNT(1) FROM GiangVien WHERE MaKhoa = @ma", new[] { new SqlParameter("@ma", maKhoa) });
                int c2 = Convert.ToInt32(o2 ?? 0);
                if (c2 > 0) return true;
            }
            catch
            {
                // If the referenced tables don't exist, assume no dependencies (or let caller handle SQL errors)
            }

            return false;
        }
    }
}
