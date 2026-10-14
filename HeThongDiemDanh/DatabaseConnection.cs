using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh
{
    public static class DatabaseConnection
    {
        // Đọc từ App.config — chỉ cần sửa App.config khi đổi máy, không cần build lại
        private static readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString
            ?? throw new InvalidOperationException(
                "Không tìm thấy chuỗi kết nối 'DefaultConnection' trong App.config.");


        // Lấy đối tượng kết nối (dùng nội bộ)
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        
        // Kiểm tra kết nối — gọi khi khởi động app để báo lỗi sớm
        public static bool KiemTraKetNoi()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // SELECT — trả về DataTable
        public static DataTable ExecuteQuery(string sql, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                ThemThamSo(cmd, parameters);
                try
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        da.Fill(dt);
                }
                catch (SqlException ex)
                {
                    throw new Exception($"Lỗi SQL ({ex.Number}): {ex.Message}");
                }
            }

            return dt;
        }

        // INSERT / UPDATE / DELETE — trả về số dòng bị ảnh hưởng
        public static int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                ThemThamSo(cmd, parameters);
                try
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    // Lỗi 2627 / 2601: vi phạm UNIQUE (điểm danh trùng)
                    if (ex.Number == 2627 || ex.Number == 2601)
                        throw new Exception("TRUNG_LAP");

                    throw new Exception($"Lỗi SQL ({ex.Number}): {ex.Message}");
                }
            }
        }

        // Lấy 1 giá trị đơn — dùng cho COUNT, MAX, EXISTS...
        public static object ExecuteScalar(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                ThemThamSo(cmd, parameters);
                try
                {
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    throw new Exception($"Lỗi SQL ({ex.Number}): {ex.Message}");
                }
            }
        }

        // Hàm nội bộ: gán tham số vào SqlCommand
        private static void ThemThamSo(SqlCommand cmd, SqlParameter[] parameters)
        {
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);
        }
    }
}