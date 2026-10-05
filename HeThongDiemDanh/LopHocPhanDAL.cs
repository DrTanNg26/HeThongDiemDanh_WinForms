using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh
{
    public static class LopHocPhanDAL
    {
        public static DataTable LayDanhSach()
        {
            string sql = @"SELECT lhp.MaLopHP, lhp.TenLopHP, lhp.MaMon, mh.TenMon,
                               lhp.MaGV, gv.HoTen AS TenGV, lhp.PhongHoc, lhp.NamHoc, lhp.TongSoBuoi
                        FROM LopHocPhan lhp
                        INNER JOIN MonHoc mh ON lhp.MaMon = mh.MaMon
                        INNER JOIN GiangVien gv ON lhp.MaGV = gv.MaGV
                        ORDER BY lhp.MaLopHP";

            return DatabaseConnection.ExecuteQuery(sql);
        }

        public static int Them(string maLopHP, string tenLopHP, string maMon, string maGV, string phongHoc, string namHoc)
        {
            string sql = @"INSERT INTO LopHocPhan (MaLopHP, TenLopHP, MaMon, MaGV, PhongHoc, NamHoc, TongSoBuoi)
                           VALUES (@maLHP, @tenLHP, @maMon, @maGV, @phong, @namHoc, 0)";

            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@maLHP", maLopHP),
                new SqlParameter("@tenLHP", tenLopHP),
                new SqlParameter("@maMon", maMon),
                new SqlParameter("@maGV", maGV),
                new SqlParameter("@phong", string.IsNullOrEmpty(phongHoc) ? (object)DBNull.Value : phongHoc),
                new SqlParameter("@namHoc", namHoc)
            });
        }

        public static int Sua(string maLopHP, string tenLopHP, string maMon, string maGV, string phongHoc, string namHoc)
        {
            string sql = @"UPDATE LopHocPhan
                           SET TenLopHP = @tenLHP, MaMon = @maMon, MaGV = @maGV, PhongHoc = @phong, NamHoc = @namHoc
                           WHERE MaLopHP = @maLHP";

            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@maLHP", maLopHP),
                new SqlParameter("@tenLHP", tenLopHP),
                new SqlParameter("@maMon", maMon),
                new SqlParameter("@maGV", maGV),
                new SqlParameter("@phong", string.IsNullOrEmpty(phongHoc) ? (object)DBNull.Value : phongHoc),
                new SqlParameter("@namHoc", namHoc)
            });
        }

        public static int Xoa(string maLopHP)
        {
            string sql = "DELETE FROM LopHocPhan WHERE MaLopHP = @maLHP";
            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@maLHP", maLopHP)
            });
        }

        public static bool HasDependencies(string maLopHP)
        {
            string sql = @"SELECT COUNT(1) FROM
                           (
                               SELECT MaLopHP FROM ChiTietLopHP WHERE MaLopHP = @ma
                               UNION ALL
                               SELECT MaLopHP FROM BuoiHoc WHERE MaLopHP = @ma
                           ) t";

            object res = DatabaseConnection.ExecuteScalar(sql, new[]
            {
                new SqlParameter("@ma", maLopHP)
            });

            return Convert.ToInt32(res ?? 0) > 0;
        }

        public static DataTable LayDanhSachGiangVien()
        {
            string sql = "SELECT MaGV, HoTen FROM GiangVien ORDER BY HoTen";
            return DatabaseConnection.ExecuteQuery(sql);
        }
    }
}
