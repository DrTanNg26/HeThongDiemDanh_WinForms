using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh
{
    public static class TaiKhoanService
    {
        public static bool AccountExists(string tenDangNhap)
        {
            object o = DatabaseConnection.ExecuteScalar("SELECT COUNT(1) FROM TaiKhoan WHERE TenDangNhap = @tk", new[] { new SqlParameter("@tk", tenDangNhap) });
            return Convert.ToInt32(o ?? 0) > 0;
        }

        public static void AddStudentAccount(string maSV, string hoTen, string email, DateTime ngaySinh, string soCCCD, string maLop, string maKhoa, string plainPassword, string trangThai = "Hoạt động")
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    try
                    {
                        // Insert into TaiKhoan
                        cmd.CommandText = "INSERT INTO TaiKhoan(TenDangNhap, MatKhau, VaiTro, TrangThai) VALUES(@tk, @pwd, @vt, @tt)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@tk", maSV));
                        cmd.Parameters.Add(new SqlParameter("@pwd", PasswordHelper.BamSHA256(plainPassword)));
                        cmd.Parameters.Add(new SqlParameter("@vt", "SinhVien"));
                        cmd.Parameters.Add(new SqlParameter("@tt", trangThai));
                        cmd.ExecuteNonQuery();

                        // Insert into SinhVien
                        cmd.CommandText = "INSERT INTO SinhVien(MaSV, HoTen, Email, NgaySinh, SoCCCD, MaLop, MaKhoa) VALUES(@ma, @ten, @email, @ns, @cccd, @malop, @makhoa)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@ma", maSV));
                        cmd.Parameters.Add(new SqlParameter("@ten", hoTen));
                        cmd.Parameters.Add(new SqlParameter("@email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email));
                        cmd.Parameters.Add(new SqlParameter("@ns", ngaySinh));
                        cmd.Parameters.Add(new SqlParameter("@cccd", soCCCD));
                        cmd.Parameters.Add(new SqlParameter("@malop", maLop));
                        cmd.Parameters.Add(new SqlParameter("@makhoa", maKhoa));
                        cmd.ExecuteNonQuery();

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void AddTeacherAccount(string maGV, string hoTen, string email, string hocHamHocVi, string maKhoa, string plainPassword, string trangThai = "Hoạt động")
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    try
                    {
                        cmd.CommandText = "INSERT INTO TaiKhoan(TenDangNhap, MatKhau, VaiTro, TrangThai) VALUES(@tk, @pwd, @vt, @tt)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@tk", maGV));
                        cmd.Parameters.Add(new SqlParameter("@pwd", PasswordHelper.BamSHA256(plainPassword)));
                        cmd.Parameters.Add(new SqlParameter("@vt", "GiangVien"));
                        cmd.Parameters.Add(new SqlParameter("@tt", trangThai));
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "INSERT INTO GiangVien(MaGV, HoTen, Email, HocHamHocVi, MaKhoa) VALUES(@ma, @ten, @email, @hhhv, @makhoa)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@ma", maGV));
                        cmd.Parameters.Add(new SqlParameter("@ten", hoTen));
                        cmd.Parameters.Add(new SqlParameter("@email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email));
                        cmd.Parameters.Add(new SqlParameter("@hhhv", string.IsNullOrEmpty(hocHamHocVi) ? (object)DBNull.Value : hocHamHocVi));
                        cmd.Parameters.Add(new SqlParameter("@makhoa", maKhoa));
                        cmd.ExecuteNonQuery();

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void AddAdminAccount(string maAD, string hoTen, string email, string plainPassword)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    try
                    {
                        cmd.CommandText = "INSERT INTO TaiKhoan(TenDangNhap, MatKhau, VaiTro, TrangThai) VALUES(@tk, @pwd, @vt, @tt)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@tk", maAD));
                        cmd.Parameters.Add(new SqlParameter("@pwd", PasswordHelper.BamSHA256(plainPassword)));
                        cmd.Parameters.Add(new SqlParameter("@vt", "Admin"));
                        cmd.Parameters.Add(new SqlParameter("@tt", "Hoạt động"));
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "INSERT INTO Admin(MaAD, HoTen, Email) VALUES(@ma, @ten, @email)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@ma", maAD));
                        cmd.Parameters.Add(new SqlParameter("@ten", hoTen));
                        cmd.Parameters.Add(new SqlParameter("@email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email));
                        cmd.ExecuteNonQuery();

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void UpdateStudentAccount(string maSV, string hoTen, string email, DateTime ngaySinh, string soCCCD, string maLop, string maKhoa)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    try
                    {
                        cmd.CommandText = "UPDATE SinhVien SET HoTen = @ten, Email = @email, NgaySinh = @ns, SoCCCD = @cccd, MaLop = @malop, MaKhoa = @makhoa WHERE MaSV = @ma";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@ma", maSV));
                        cmd.Parameters.Add(new SqlParameter("@ten", hoTen));
                        cmd.Parameters.Add(new SqlParameter("@email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email));
                        cmd.Parameters.Add(new SqlParameter("@ns", ngaySinh));
                        cmd.Parameters.Add(new SqlParameter("@cccd", soCCCD));
                        cmd.Parameters.Add(new SqlParameter("@malop", maLop));
                        cmd.Parameters.Add(new SqlParameter("@makhoa", maKhoa));
                        cmd.ExecuteNonQuery();

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void UpdateTeacherAccount(string maGV, string hoTen, string email, string hocHamHocVi, string maKhoa)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    try
                    {
                        cmd.CommandText = "UPDATE GiangVien SET HoTen = @ten, Email = @email, HocHamHocVi = @hhhv, MaKhoa = @makhoa WHERE MaGV = @ma";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@ma", maGV));
                        cmd.Parameters.Add(new SqlParameter("@ten", hoTen));
                        cmd.Parameters.Add(new SqlParameter("@email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email));
                        cmd.Parameters.Add(new SqlParameter("@hhhv", string.IsNullOrEmpty(hocHamHocVi) ? (object)DBNull.Value : hocHamHocVi));
                        cmd.Parameters.Add(new SqlParameter("@makhoa", maKhoa));
                        cmd.ExecuteNonQuery();

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void UpdateAdminAccount(string maAD, string hoTen, string email)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    try
                    {
                        cmd.CommandText = "UPDATE Admin SET HoTen = @ten, Email = @email WHERE MaAD = @ma";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@ma", maAD));
                        cmd.Parameters.Add(new SqlParameter("@ten", hoTen));
                        cmd.Parameters.Add(new SqlParameter("@email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email));
                        cmd.ExecuteNonQuery();

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
