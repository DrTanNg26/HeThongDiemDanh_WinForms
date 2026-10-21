using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh
{
    // Kết quả trả về khi kết thúc buổi học
    public class KetQuaDongBuoiHoc
    {
        public int MaBuoiHoc { get; set; }
        public string MaLopHP { get; set; } = string.Empty;
        public int TongSiSo { get; set; }
        public int SoCoMat { get; set; }
        public int SoDiMuon { get; set; }
        public int SoVang { get; set; }
        public int SoNghiPhep { get; set; }
        public int TongDaDiemDanh => SoCoMat + SoDiMuon;
    }

    // Thông tin cơ bản của một phiên điểm danh
    public class ThongTinBuoiHoc
    {
        public int MaBuoiHoc { get; set; }
        public string MaLopHP { get; set; } = string.Empty;
        public string TenLopHP { get; set; } = string.Empty;
        public string TenBuoiHoc { get; set; } = string.Empty;
        public DateTime NgayHoc { get; set; }
        public string MaOTP { get; set; } = string.Empty;
        public string IP_GV { get; set; } = string.Empty;
        public string? SubnetMask_GV { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public int ThoiGianDemNguoc { get; set; }
        public string TrangThai { get; set; } = string.Empty;
        public int TongSiSo { get; set; }
    }

    /// <summary>
    /// Xử lý dữ liệu Buổi học & Điểm danh cho Giảng viên (Thành viên 2)
    /// </summary>
    public static class BuoiHocDAL
    {
        // Bộ đếm ở form chính gọi hàm này kể cả khi trình chiếu đang tắt.
        public static int TuDongDongPhienQuaHan(string? maGV = null)
        {
            string sql = "SELECT bh.MaBuoiHoc FROM dbo.BuoiHoc bh ";
            sql += "JOIN dbo.LopHocPhan lhp ON lhp.MaLopHP=bh.MaLopHP ";
            sql += "WHERE bh.TrangThai=N'Mo' AND DATEADD(MINUTE,bh.ThoiGianDemNguoc,bh.ThoiGianBatDau)<=GETDATE() ";
            sql += "AND (@MaGV IS NULL OR lhp.MaGV=@MaGV)";
            using (DataTable dt = DatabaseConnection.ExecuteQuery(sql, new[]
            {
                new SqlParameter("@MaGV", SqlDbType.VarChar, 10) { Value = (object?)maGV ?? DBNull.Value }
            }))
            {
                foreach (DataRow dong in dt.Rows)
                {
                    // SQL kiểm tra lại hạn dưới khóa; không đóng sớm theo đồng hồ máy cá nhân.
                    DongBuoiHoc(Convert.ToInt32(dong["MaBuoiHoc"]), true);
                }
                return dt.Rows.Count;
            }
        }

        public static List<ThongTinBuoiHoc> LayCacPhienDangMo(string maGV)
        {
            string sql = "SELECT bh.*, lhp.TenLopHP, ";
            sql += "(SELECT COUNT(*) FROM ChiTietLopHP ct WHERE ct.MaLopHP=bh.MaLopHP) AS TongSiSo ";
            sql += "FROM BuoiHoc bh JOIN LopHocPhan lhp ON lhp.MaLopHP=bh.MaLopHP ";
            sql += "WHERE lhp.MaGV=@MaGV AND bh.TrangThai=N'Mo' ORDER BY bh.ThoiGianBatDau DESC";
            List<ThongTinBuoiHoc> danhSach = new List<ThongTinBuoiHoc>();
            using (DataTable dt = DatabaseConnection.ExecuteQuery(sql, new[] { new SqlParameter("@MaGV", maGV) }))
            {
                foreach (DataRow dong in dt.Rows) danhSach.Add(MapThongTinBuoiHoc(dong));
            }
            return danhSach;
        }

        // 1. Lấy danh sách lớp kèm sĩ số do giảng viên phụ trách
        public static DataTable LayDanhSachLopTheoGiangVien(string maGV)
        {
            string sql = @"
                SELECT lhp.MaLopHP, lhp.TenLopHP, lhp.MaMon, mh.TenMon, lhp.PhongHoc, lhp.NamHoc,
                       COUNT(ct.MaSV) AS SiSo
                FROM LopHocPhan lhp
                INNER JOIN MonHoc mh ON lhp.MaMon = mh.MaMon
                LEFT JOIN ChiTietLopHP ct ON lhp.MaLopHP = ct.MaLopHP
                WHERE lhp.MaGV = @maGV
                GROUP BY lhp.MaLopHP, lhp.TenLopHP, lhp.MaMon, mh.TenMon, lhp.PhongHoc, lhp.NamHoc
                ORDER BY lhp.MaLopHP";

            return DatabaseConnection.ExecuteQuery(sql, new[] { new SqlParameter("@maGV", maGV) });
        }

        // 2. Lấy sĩ số sinh viên của một lớp học phần
        public static int LaySiSoLop(string maLopHP)
        {
            string sql = "SELECT COUNT(1) FROM ChiTietLopHP WHERE MaLopHP = @ma";
            return Convert.ToInt32(DatabaseConnection.ExecuteScalar(sql, new[] { new SqlParameter("@ma", maLopHP) }) ?? 0);
        }

        // 3. Tính số thứ tự buổi học tiếp theo (Buổi 1, Buổi 2,...)
        public static int LaySoBuoiTiepTheo(string maLopHP)
        {
            string sql = "SELECT COUNT(1) + 1 FROM BuoiHoc WHERE MaLopHP = @ma";
            return Convert.ToInt32(DatabaseConnection.ExecuteScalar(sql, new[] { new SqlParameter("@ma", maLopHP) }) ?? 1);
        }

        // 4. Kiểm tra lớp có phiên đang mở chưa đóng (trong thời gian điểm danh)
        public static bool KiemTraLopDangCoPhienMo(string maLopHP)
        {
            // Tự động đóng các phiên đã quá giờ điểm danh trước khi kiểm tra

            string sql = @"
                SELECT COUNT(1) FROM BuoiHoc 
                WHERE MaLopHP = @ma AND TrangThai = N'Mo' 
                  AND DATEADD(MINUTE, ThoiGianDemNguoc, ThoiGianBatDau) > GETDATE()";

            return Convert.ToInt32(DatabaseConnection.ExecuteScalar(sql, new[]
            {
                new SqlParameter("@ma", maLopHP)
            }) ?? 0) > 0;
        }

        // 5. Kiểm tra mã OTP có đang trùng với phiên mở khác không
        public static bool KiemTraTrungOTP(string maOTP)
        {

            string sql = @"
                SELECT COUNT(1) FROM BuoiHoc 
                WHERE MaOTP = @otp AND TrangThai = N'Mo' 
                  AND DATEADD(MINUTE, ThoiGianDemNguoc, ThoiGianBatDau) > GETDATE()";

            return Convert.ToInt32(DatabaseConnection.ExecuteScalar(sql, new[]
            {
                new SqlParameter("@otp", maOTP)
            }) ?? 0) > 0;
        }

        // 6. Lấy giờ server SQL để đồng bộ đồng hồ đếm ngược
        public static DateTime LayGioServer()
        {
            return Convert.ToDateTime(DatabaseConnection.ExecuteScalar("SELECT GETDATE()") ?? DateTime.Now);
        }

        // 7. Mở phiên điểm danh mới, trả về MaBuoiHoc
        public static int TaoPhienDiemDanh(string maLopHP, string tenBuoiHoc, string maOTP, string ipGV, string subnetMaskGV, int thoiGianDemNguoc)
        {
            string sql = @"
                INSERT INTO BuoiHoc (MaLopHP, TenBuoiHoc, NgayHoc, MaOTP, IP_GV, SubnetMask_GV, ThoiGianBatDau, ThoiGianDemNguoc, TrangThai)
                OUTPUT INSERTED.MaBuoiHoc
                VALUES (@maLHP, @tenBuoi, CAST(GETDATE() AS DATE), @otp, @ip, @subnet, GETDATE(), @phut, N'Mo')";

            return Convert.ToInt32(DatabaseConnection.ExecuteScalar(sql, new[]
            {
                new SqlParameter("@maLHP", maLopHP),
                new SqlParameter("@tenBuoi", tenBuoiHoc),
                new SqlParameter("@otp", maOTP),
                new SqlParameter("@ip", ipGV),
                new SqlParameter("@subnet", (object?)subnetMaskGV ?? DBNull.Value),
                new SqlParameter("@phut", thoiGianDemNguoc)
            }) ?? 0);
        }

        // 8. Lấy chi tiết buổi học theo mã
        public static ThongTinBuoiHoc? LayThongTinBuoiHoc(int maBuoiHoc)
        {
            string sql = @"
                SELECT bh.*, lhp.TenLopHP,
                       (SELECT COUNT(1) FROM ChiTietLopHP ct WHERE ct.MaLopHP = bh.MaLopHP) AS TongSiSo
                FROM BuoiHoc bh
                INNER JOIN LopHocPhan lhp ON bh.MaLopHP = lhp.MaLopHP
                WHERE bh.MaBuoiHoc = @ma";

            DataTable dt = DatabaseConnection.ExecuteQuery(sql, new[] { new SqlParameter("@ma", maBuoiHoc) });
            return dt.Rows.Count > 0 ? MapThongTinBuoiHoc(dt.Rows[0]) : null;
        }

        // 9. Danh sách sinh viên điểm danh realtime (hiển thị trên Grid)
        public static DataTable LayDanhSachDiemDanhRealtime(int maBuoiHoc)
        {
            string sql = @"
                SELECT ROW_NUMBER() OVER (ORDER BY dd.ThoiGianQuet ASC) AS STT,
                       sv.MaSV, sv.HoTen, 
                       CONVERT(VARCHAR(8), dd.ThoiGianQuet, 108) AS GioCheckIn,
                       dd.TrangThai
                FROM DiemDanh dd
                INNER JOIN SinhVien sv ON dd.MaSV = sv.MaSV
                WHERE dd.MaBuoiHoc = @ma
                ORDER BY dd.ThoiGianQuet ASC";

            return DatabaseConnection.ExecuteQuery(sql, new[] { new SqlParameter("@ma", maBuoiHoc) });
        }

        // 10. Thống kê realtime: (Có mặt, Chưa điểm danh, Sĩ số)
        public static (int daCoMat, int chuaDiemDanh, int tongSiSo) LayThongKeRealtime(int maBuoiHoc, string maLopHP)
        {
            int siSo = LaySiSoLop(maLopHP);
            string sql = "SELECT COUNT(1) FROM DiemDanh WHERE MaBuoiHoc = @ma AND TrangThai IN (N'Có mặt', N'Đi muộn')";
            int daCoMat = Convert.ToInt32(DatabaseConnection.ExecuteScalar(sql, new[] { new SqlParameter("@ma", maBuoiHoc) }) ?? 0);
            return (daCoMat, Math.Max(0, siSo - daCoMat), siSo);
        }

        // 11. Kiểm tra xem buổi học đã có sinh viên nào điểm danh chưa và số lượng
        public static int LaySoLuongSVDaDiemDanh(int maBuoiHoc)
        {
            string sqlCheck = "SELECT COUNT(1) FROM DiemDanh WHERE MaBuoiHoc = @ma";
            return Convert.ToInt32(DatabaseConnection.ExecuteScalar(sqlCheck, new[] { new SqlParameter("@ma", maBuoiHoc) }) ?? 0);
        }

        public static bool DaCoSinhVienDiemDanh(int maBuoiHoc)
        {
            return LaySoLuongSVDaDiemDanh(maBuoiHoc) > 0;
        }

        // 12. Hủy phiên điểm danh (xóa toàn bộ dữ liệu điểm danh liên quan nếu có SV đã quét)
        public static bool HuyPhienDiemDanh(int maBuoiHoc)
        {
            using SqlConnection conn = DatabaseConnection.GetConnection();
            conn.Open();
            using SqlTransaction tran = conn.BeginTransaction();
            try
            {
                // Khóa buổi trước, cùng thứ tự với điểm danh và tự đóng phiên.
                using (SqlCommand kiemTra = new SqlCommand(
                    "SELECT TrangThai FROM BuoiHoc WITH (UPDLOCK,HOLDLOCK) WHERE MaBuoiHoc=@ma", conn, tran))
                {
                    kiemTra.Parameters.Add(new SqlParameter("@ma", maBuoiHoc));
                    if (Convert.ToString(kiemTra.ExecuteScalar()) != "Mo")
                    {
                        tran.Rollback();
                        return false;
                    }
                }
                // 1. Xóa các bản ghi điểm danh của buổi học này trước (để không vi phạm khóa ngoại)
                string sqlXoaDiemDanh = "DELETE FROM DiemDanh WHERE MaBuoiHoc = @ma";
                using (SqlCommand cmdDD = new SqlCommand(sqlXoaDiemDanh, conn, tran))
                {
                    cmdDD.Parameters.Add(new SqlParameter("@ma", maBuoiHoc));
                    cmdDD.ExecuteNonQuery();
                }

                // 2. Xóa bản ghi buổi học đang mở
                string sqlXoaBuoiHoc = "DELETE FROM BuoiHoc WHERE MaBuoiHoc = @ma AND TrangThai = N'Mo'";
                int affected = 0;
                using (SqlCommand cmdBH = new SqlCommand(sqlXoaBuoiHoc, conn, tran))
                {
                    cmdBH.Parameters.Add(new SqlParameter("@ma", maBuoiHoc));
                    affected = cmdBH.ExecuteNonQuery();
                }

                if (affected > 0)
                {
                    tran.Commit();
                    return true;
                }

                tran.Rollback();
                return false;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        // 12. Đóng buổi học qua Stored Procedure sp_DongBuoiHoc
        public static KetQuaDongBuoiHoc DongBuoiHoc(int maBuoiHoc, bool chiKhiHetHan = false)
        {
            using SqlConnection conn = DatabaseConnection.GetConnection();
            using SqlCommand cmd = new SqlCommand("sp_DongBuoiHoc", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add(new SqlParameter("@MaBuoiHoc", maBuoiHoc));
            cmd.Parameters.Add(new SqlParameter("@ChiKhiHetHan", chiKhiHetHan));

            conn.Open();
            using SqlDataReader r = cmd.ExecuteReader();
            if (r.Read())
            {
                return new KetQuaDongBuoiHoc
                {
                    MaBuoiHoc = maBuoiHoc,
                    MaLopHP = r["MaLopHP"]?.ToString() ?? "",
                    TongSiSo = Convert.ToInt32(r["TongSiSo"] ?? 0),
                    SoCoMat = Convert.ToInt32(r["SoCoMat"] ?? 0),
                    SoDiMuon = Convert.ToInt32(r["SoDiMuon"] ?? 0),
                    SoVang = Convert.ToInt32(r["SoVang"] ?? 0),
                    SoNghiPhep = Convert.ToInt32(r["SoNghiPhep"] ?? 0)
                };
            }
            return new KetQuaDongBuoiHoc { MaBuoiHoc = maBuoiHoc };
        }

        // 13. Khôi phục phiên chưa đóng khi giảng viên mở lại app
        public static ThongTinBuoiHoc? LayPhienDangMoCuaGiangVien(string maGV)
        {
            // Tự động đóng các phiên đã quá giờ điểm danh

            string sql = @"
                SELECT TOP 1 bh.*, lhp.TenLopHP,
                       (SELECT COUNT(1) FROM ChiTietLopHP ct WHERE ct.MaLopHP = bh.MaLopHP) AS TongSiSo
                FROM BuoiHoc bh
                INNER JOIN LopHocPhan lhp ON bh.MaLopHP = lhp.MaLopHP
                WHERE lhp.MaGV = @maGV AND bh.TrangThai = N'Mo'
                  AND DATEADD(MINUTE, bh.ThoiGianDemNguoc, bh.ThoiGianBatDau) > GETDATE()
                ORDER BY bh.ThoiGianBatDau DESC";

            DataTable dt = DatabaseConnection.ExecuteQuery(sql, new[]
            {
                new SqlParameter("@maGV", maGV)
            });

            return dt.Rows.Count > 0 ? MapThongTinBuoiHoc(dt.Rows[0]) : null;
        }

        // Helper: Ánh xạ DataRow sang ThongTinBuoiHoc
        private static ThongTinBuoiHoc MapThongTinBuoiHoc(DataRow row) => new()
        {
            MaBuoiHoc = Convert.ToInt32(row["MaBuoiHoc"]),
            MaLopHP = row["MaLopHP"]?.ToString() ?? "",
            TenLopHP = row["TenLopHP"]?.ToString() ?? "",
            TenBuoiHoc = row["TenBuoiHoc"]?.ToString() ?? "",
            NgayHoc = Convert.ToDateTime(row["NgayHoc"]),
            MaOTP = row["MaOTP"]?.ToString() ?? "",
            IP_GV = row["IP_GV"]?.ToString() ?? "",
            SubnetMask_GV = row["SubnetMask_GV"] == DBNull.Value ? null : row["SubnetMask_GV"]?.ToString(),
            ThoiGianBatDau = Convert.ToDateTime(row["ThoiGianBatDau"]),
            ThoiGianDemNguoc = Convert.ToInt32(row["ThoiGianDemNguoc"]),
            TrangThai = row["TrangThai"]?.ToString() ?? "",
            TongSiSo = Convert.ToInt32(row["TongSiSo"] ?? 0)
        };
    }
}
