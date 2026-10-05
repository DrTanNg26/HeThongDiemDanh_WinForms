using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh
{
    public static class ChiTietLopHPDAL
    {
        // Lấy danh sách sinh viên ĐÃ THUỘC lớp học phần
        public static DataTable LaySinhVienTrongLop(string maLopHP)
        {
            string sql = @"SELECT sv.MaSV, sv.HoTen, sv.MaLop AS LopHanhChinh, k.TenKhoa
                           FROM ChiTietLopHP ct
                           INNER JOIN SinhVien sv ON ct.MaSV = sv.MaSV
                           LEFT JOIN Khoa k ON sv.MaKhoa = k.MaKhoa
                           WHERE ct.MaLopHP = @maLHP
                           ORDER BY sv.MaSV";

            return DatabaseConnection.ExecuteQuery(sql, new[] { new SqlParameter("@maLHP", maLopHP) });
        }

        // Lấy danh sách sinh viên CHƯA THUỘC lớp học phần (để chọn gán vào)
        public static DataTable LaySinhVienNgoaiLop(string maLopHP, string keyword = null)
        {
            string sql = @"SELECT sv.MaSV, sv.HoTen, sv.MaLop AS LopHanhChinh, k.TenKhoa
                           FROM SinhVien sv
                           LEFT JOIN Khoa k ON sv.MaKhoa = k.MaKhoa
                           WHERE sv.MaSV NOT IN (SELECT MaSV FROM ChiTietLopHP WHERE MaLopHP = @maLHP)";

            var parameters = new System.Collections.Generic.List<SqlParameter>
            {
                new SqlParameter("@maLHP", maLopHP)
            };

            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND (sv.MaSV LIKE @kw OR sv.HoTen LIKE @kw OR sv.MaLop LIKE @kw)";
                parameters.Add(new SqlParameter("@kw", "%" + keyword + "%"));
            }

            sql += " ORDER BY sv.MaSV";

            return DatabaseConnection.ExecuteQuery(sql, parameters.ToArray());
        }

        // Thêm Sinh viên vào Lớp học phần
        public static int ThemSinhVienVaoLop(string maLopHP, string maSV)
        {
            string sql = "INSERT INTO ChiTietLopHP (MaLopHP, MaSV) VALUES (@maLHP, @maSV)";
            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@maLHP", maLopHP),
                new SqlParameter("@maSV", maSV)
            });
        }

        // Xóa Sinh viên khỏi Lớp học phần (nếu chưa có bản ghi điểm danh)
        public static int XoaSinhVienKhoiLop(string maLopHP, string maSV)
        {
            string sql = "DELETE FROM ChiTietLopHP WHERE MaLopHP = @maLHP AND MaSV = @maSV";
            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@maLHP", maLopHP),
                new SqlParameter("@maSV", maSV)
            });
        }

        // Kiểm tra xem sinh viên trong lớp HP đã có bản ghi điểm danh nào chưa
        public static bool DaCoDiemDanh(string maLopHP, string maSV)
        {
            string sql = @"SELECT COUNT(1) 
                           FROM DiemDanh dd
                           INNER JOIN BuoiHoc bh ON dd.MaBuoiHoc = bh.MaBuoiHoc
                           WHERE bh.MaLopHP = @maLHP AND dd.MaSV = @maSV";

            object res = DatabaseConnection.ExecuteScalar(sql, new[]
            {
                new SqlParameter("@maLHP", maLopHP),
                new SqlParameter("@maSV", maSV)
            });

            return Convert.ToInt32(res ?? 0) > 0;
        }
    }
}
