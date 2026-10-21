using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh
{
    // Helper kết nối SQL Server — đọc connection string từ App.config
    // Khi đổi máy: chỉ cần sửa App.config, không rebuild
    public static class DatabaseConnection
    {
        private static string _cs = null;

        // Lấy connection string (ưu tiên runtime nếu đã được cập nhật qua FrmCauHinhKetNoi)
        public static string ConnectionString
        {
            get => _cs ??= ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString
                           ?? throw new InvalidOperationException("Thiếu DefaultConnection trong App.config");
            set => _cs = value;   // Cho phép ghi đè lúc runtime (khi người dùng đổi server)
        }

        public static SqlConnection GetConnection() => new SqlConnection(ConnectionString);

        // Kiểm tra kết nối — gọi khi khởi động
        public static bool KiemTraKetNoi(out string error)
        {
            try { using var c = GetConnection(); c.Open(); error = null; return true; }
            catch (Exception ex) { error = ex.Message; return false; }
        }

        // Trả DataTable — dùng cho SELECT
        public static DataTable ExecuteQuery(string sql, SqlParameter[] p = null)
        {
            using var conn = GetConnection();
            using var cmd  = new SqlCommand(sql, conn);
            if (p != null) cmd.Parameters.AddRange(p);
            try { conn.Open(); var dt = new DataTable(); new SqlDataAdapter(cmd).Fill(dt); return dt; }
            catch (SqlException ex) { throw new Exception($"Lỗi SQL ({ex.Number}): {ex.Message}"); }
        }

        // Trả số dòng bị ảnh hưởng — dùng cho INSERT/UPDATE/DELETE
        public static int ExecuteNonQuery(string sql, SqlParameter[] p = null)
        {
            using var conn = GetConnection();
            using var cmd  = new SqlCommand(sql, conn);
            if (p != null) cmd.Parameters.AddRange(p);
            try { conn.Open(); return cmd.ExecuteNonQuery(); }
            catch (SqlException ex)
            {
                if (ex.Number is 2627 or 2601) throw new Exception("TRUNG_LAP");
                throw new Exception($"Lỗi SQL ({ex.Number}): {ex.Message}");
            }
        }

        // Trả 1 giá trị đơn — dùng cho COUNT, MAX, v.v.
        public static object ExecuteScalar(string sql, SqlParameter[] p = null)
        {
            using var conn = GetConnection();
            using var cmd  = new SqlCommand(sql, conn);
            if (p != null) cmd.Parameters.AddRange(p);
            try { conn.Open(); return cmd.ExecuteScalar(); }
            catch (SqlException ex) { throw new Exception($"Lỗi SQL ({ex.Number}): {ex.Message}"); }
        }
    }
}
