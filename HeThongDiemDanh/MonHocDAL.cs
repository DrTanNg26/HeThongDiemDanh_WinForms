using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh
{
    public static class MonHocDAL
    {
        public static DataTable LayDanhSach()
        {
            string sql = "SELECT MaMon, TenMon, SoTinChi FROM MonHoc ORDER BY MaMon";
            return DatabaseConnection.ExecuteQuery(sql);
        }

        public static int Them(string maMon, string tenMon, int soTin)
        {
            string sql = "INSERT INTO MonHoc(MaMon, TenMon, SoTinChi) VALUES(@ma, @ten, @so)";
            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@ma", maMon),
                new SqlParameter("@ten", tenMon),
                new SqlParameter("@so", soTin)
            });
        }

        public static int Sua(string maMon, string tenMon, int soTin)
        {
            string sql = "UPDATE MonHoc SET TenMon = @ten, SoTinChi = @so WHERE MaMon = @ma";
            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@ten", tenMon),
                new SqlParameter("@so", soTin),
                new SqlParameter("@ma", maMon)
            });
        }

        public static int Xoa(string maMon)
        {
            string sql = "DELETE FROM MonHoc WHERE MaMon = @ma";
            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@ma", maMon)
            });
        }

        public static bool HasDependencies(string maMon)
        {
            try
            {
                object o = DatabaseConnection.ExecuteScalar("SELECT COUNT(1) FROM LopHocPhan WHERE MaMon = @ma", new[] { new SqlParameter("@ma", maMon) });
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
