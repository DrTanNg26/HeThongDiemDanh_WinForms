using System;
using System.Data;
using System.Configuration;
using Microsoft.Data.SqlClient; // Nếu dự án dùng NuGet System.Data.SqlClient thì sửa lại dòng này nhé

namespace HeThongDiemDanh // Thay bằng đúng tên Namespace trong dự án của bạn
{
    public static class DatabaseConnection
    {
        // 1. Khai báo chuỗi kết nối (Lấy trực tiếp từ App.config hoặc dán thẳng chuỗi kết nối từ máy bạn)
        private static string connectionString = @"Data Source=DESKTOP-KVDPUMF\SQLEXPRESS;
                                                Initial Catalog=HeThongDiemDanh;
                                                User ID=AdminApp;
                                                Password=admin123;
                                                Encrypt=True;
                                                TrustServerCertificate=True;";

        // 2. Hàm lấy đối tượng kết nối SqlConnection
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // 3. Hàm thực thi câu lệnh SELECT (Trả về DataTable chứa dữ liệu)
        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi truy vấn CSDL: " + ex.Message);
                }
            }

            return dataTable;
        }

        // 4. Hàm thực thi INSERT, UPDATE, DELETE (Trả về số dòng bị ảnh hưởng trong SQL)
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            int rowsAffected = 0;

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi thực thi CSDL: " + ex.Message);
                }
            }

            return rowsAffected;
        }

        // 5. Hàm lấy 1 giá trị đơn lẻ (Dùng cho COUNT, SUM, MAX hoặc SELECT SCALAR)
        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            object result = null;

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        result = cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi lấy dữ liệu CSDL: " + ex.Message);
                }
            }
            return result;
        }
    }
}