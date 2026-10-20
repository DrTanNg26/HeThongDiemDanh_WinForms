using System;
using System.Data;
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
        // 0. Tự động đóng các phiên bị bỏ quên/quá 4 tiếng (Task 5.3)
        public static int TuDongDongPhienQuaHan()
        {
            string sql = @"
                UPDATE BuoiHoc 
                SET TrangThai = N'DaDong' 
                WHERE TrangThai = N'Mo' 
                  AND DATEADD(MINUTE, @phut, ThoiGianBatDau) <= GETDATE()";

            return DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@phut", AppConfig.GioiHanTuDongDongPhien)
            });
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

        // 4. Kiểm tra lớp có phiên đang mở chưa đóng (< 4 tiếng)
        public static bool KiemTraLopDangCoPhienMo(string maLopHP)
        {
            string sql = @"
                SELECT COUNT(1) FROM BuoiHoc 
                WHERE MaLopHP = @ma AND TrangThai = N'Mo' 
                  AND DATEADD(MINUTE, @phut, ThoiGianBatDau) > GETDATE()";

            return Convert.ToInt32(DatabaseConnection.ExecuteScalar(sql, new[]
            {
                new SqlParameter("@ma", maLopHP),
                new SqlParameter("@phut", AppConfig.GioiHanTuDongDongPhien)
            }) ?? 0) > 0;
        }

        // 5. Kiểm tra mã OTP có đang trùng với phiên mở khác không
        public static bool KiemTraTrungOTP(string maOTP)
        {
            string sql = @"
                SELECT COUNT(1) FROM BuoiHoc 
                WHERE MaOTP = @otp AND TrangThai = N'Mo' 
                  AND DATEADD(MINUTE, @phut, ThoiGianBatDau) > GETDATE()";

            return Convert.ToInt32(DatabaseConnection.ExecuteScalar(sql, new[]
            {
                new SqlParameter("@otp", maOTP),
                new SqlParameter("@phut", AppConfig.GioiHanTuDongDongPhien)
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

        // 11. Kiểm tra xem buổi học đã có sinh viên nào điểm danh chưa
        public static bool DaCoSinhVienDiemDanh(int maBuoiHoc)
        {
            string sqlCheck = "SELECT COUNT(1) FROM DiemDanh WHERE MaBuoiHoc = @ma";
            return Convert.ToInt32(DatabaseConnection.ExecuteScalar(sqlCheck, new[] { new SqlParameter("@ma", maBuoiHoc) }) ?? 0) > 0;
        }

        // 12. Hủy phiên nếu chưa có sinh viên nào điểm danh
        public static bool HuyPhienDiemDanh(int maBuoiHoc)
        {
            if (DaCoSinhVienDiemDanh(maBuoiHoc)) return false;

            string sqlXoa = "DELETE FROM BuoiHoc WHERE MaBuoiHoc = @ma AND TrangThai = N'Mo'";
            return DatabaseConnection.ExecuteNonQuery(sqlXoa, new[] { new SqlParameter("@ma", maBuoiHoc) }) > 0;
        }

        // 12. Đóng buổi học qua Stored Procedure sp_DongBuoiHoc
        public static KetQuaDongBuoiHoc DongBuoiHoc(int maBuoiHoc)
        {
            using SqlConnection conn = DatabaseConnection.GetConnection();
            using SqlCommand cmd = new SqlCommand("sp_DongBuoiHoc", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add(new SqlParameter("@MaBuoiHoc", maBuoiHoc));

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
            string sql = @"
                SELECT TOP 1 bh.*, lhp.TenLopHP,
                       (SELECT COUNT(1) FROM ChiTietLopHP ct WHERE ct.MaLopHP = bh.MaLopHP) AS TongSiSo
                FROM BuoiHoc bh
                INNER JOIN LopHocPhan lhp ON bh.MaLopHP = lhp.MaLopHP
                WHERE lhp.MaGV = @maGV AND bh.TrangThai = N'Mo'
                  AND DATEADD(MINUTE, @phut, bh.ThoiGianBatDau) > GETDATE()
                ORDER BY bh.ThoiGianBatDau DESC";

            DataTable dt = DatabaseConnection.ExecuteQuery(sql, new[]
            {
                new SqlParameter("@maGV", maGV),
                new SqlParameter("@phut", AppConfig.GioiHanTuDongDongPhien)
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
