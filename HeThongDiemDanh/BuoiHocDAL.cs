using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh
{
    /// <summary>
    /// Model lưu thông tin tóm tắt kết quả khi đóng buổi học
    /// </summary>
    public class KetQuaDongBuoiHoc
    {
        public int MaBuoiHoc { get; set; }
        public string MaLopHP { get; set; } = string.Empty;
        public int TongSiSo { get; set; }
        public int SoCoMat { get; set; }
        public int SoDiMuon { get; set; }
        public int SoVang { get; set; }
        public int SoNghiPhep { get; set; }
        public int TongDaDiemDanh { get; set; }
    }

    /// <summary>
    /// Model lưu thông tin phiên buổi học
    /// </summary>
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
    /// Data Access Layer phụ trách toàn bộ nghiệp vụ Buổi học & Điểm danh cho Phân hệ Giảng viên (Thành viên 2)
    /// </summary>
    public static class BuoiHocDAL
    {
        // -------------------------------------------------------------------------------------------------
        // 1. Lấy danh sách các lớp học phần do Giảng viên phụ trách
        // -------------------------------------------------------------------------------------------------
        public static DataTable LayDanhSachLopTheoGiangVien(string maGV)
        {
            string sql = @"
                SELECT 
                    lhp.MaLopHP, 
                    lhp.TenLopHP, 
                    lhp.MaMon, 
                    mh.TenMon, 
                    lhp.PhongHoc, 
                    lhp.NamHoc, 
                    lhp.TongSoBuoi,
                    COUNT(ct.MaSV) AS SiSo
                FROM LopHocPhan lhp
                INNER JOIN MonHoc mh ON lhp.MaMon = mh.MaMon
                LEFT JOIN ChiTietLopHP ct ON lhp.MaLopHP = ct.MaLopHP
                WHERE lhp.MaGV = @maGV
                GROUP BY 
                    lhp.MaLopHP, lhp.TenLopHP, lhp.MaMon, mh.TenMon, 
                    lhp.PhongHoc, lhp.NamHoc, lhp.TongSoBuoi
                ORDER BY lhp.MaLopHP";

            return DatabaseConnection.ExecuteQuery(sql, new[]
            {
                new SqlParameter("@maGV", maGV)
            });
        }

        // -------------------------------------------------------------------------------------------------
        // 2. Lấy sĩ số của lớp học phần
        // -------------------------------------------------------------------------------------------------
        public static int LaySiSoLop(string maLopHP)
        {
            string sql = "SELECT COUNT(1) FROM ChiTietLopHP WHERE MaLopHP = @maLHP";
            object res = DatabaseConnection.ExecuteScalar(sql, new[]
            {
                new SqlParameter("@maLHP", maLopHP)
            });
            return Convert.ToInt32(res ?? 0);
        }

        // -------------------------------------------------------------------------------------------------
        // 3. Tự động tính số thứ tự buổi tiếp theo cho lớp học phần
        // -------------------------------------------------------------------------------------------------
        public static int LaySoBuoiTiepTheo(string maLopHP)
        {
            string sql = "SELECT COUNT(1) FROM BuoiHoc WHERE MaLopHP = @maLHP";
            object res = DatabaseConnection.ExecuteScalar(sql, new[]
            {
                new SqlParameter("@maLHP", maLopHP)
            });
            return Convert.ToInt32(res ?? 0) + 1;
        }

        // -------------------------------------------------------------------------------------------------
        // 4. Kiểm tra xem lớp học phần có phiên nào đang mở và chưa quá 4 tiếng không
        // -------------------------------------------------------------------------------------------------
        public static bool KiemTraLopDangCoPhienMo(string maLopHP)
        {
            string sql = @"
                SELECT COUNT(1) 
                FROM BuoiHoc 
                WHERE MaLopHP = @maLHP 
                  AND TrangThai = N'Mo'
                  AND DATEADD(MINUTE, @gioiHanPhut, ThoiGianBatDau) > GETDATE()";

            object res = DatabaseConnection.ExecuteScalar(sql, new[]
            {
                new SqlParameter("@maLHP", maLopHP),
                new SqlParameter("@gioiHanPhut", AppConfig.GioiHanTuDongDongPhien)
            });
            return Convert.ToInt32(res ?? 0) > 0;
        }

        // -------------------------------------------------------------------------------------------------
        // 5. Kiểm tra mã OTP có đang trùng với phiên nào còn hiệu lực đang mở không
        // -------------------------------------------------------------------------------------------------
        public static bool KiemTraTrungOTP(string maOTP)
        {
            string sql = @"
                SELECT COUNT(1) 
                FROM BuoiHoc 
                WHERE MaOTP = @otp 
                  AND TrangThai = N'Mo'
                  AND DATEADD(MINUTE, @gioiHanPhut, ThoiGianBatDau) > GETDATE()";

            object res = DatabaseConnection.ExecuteScalar(sql, new[]
            {
                new SqlParameter("@otp", maOTP),
                new SqlParameter("@gioiHanPhut", AppConfig.GioiHanTuDongDongPhien)
            });
            return Convert.ToInt32(res ?? 0) > 0;
        }

        // -------------------------------------------------------------------------------------------------
        // 6. Lấy ngày giờ hiện tại của SQL Server (để đồng bộ đếm ngược chính xác)
        // -------------------------------------------------------------------------------------------------
        public static DateTime LayGioServer()
        {
            string sql = "SELECT GETDATE()";
            object res = DatabaseConnection.ExecuteScalar(sql);
            return Convert.ToDateTime(res ?? DateTime.Now);
        }

        // -------------------------------------------------------------------------------------------------
        // 7. Tạo mới một phiên điểm danh (BuoiHoc)
        // -------------------------------------------------------------------------------------------------
        public static int TaoPhienDiemDanh(string maLopHP, string tenBuoiHoc, string maOTP, string ipGV, string subnetMaskGV, int thoiGianDemNguoc)
        {
            string sql = @"
                INSERT INTO BuoiHoc (MaLopHP, TenBuoiHoc, NgayHoc, MaOTP, IP_GV, SubnetMask_GV, ThoiGianBatDau, ThoiGianDemNguoc, TrangThai)
                OUTPUT INSERTED.MaBuoiHoc
                VALUES (@maLHP, @tenBuoiHoc, CAST(GETDATE() AS DATE), @maOTP, @ipGV, @subnetGV, GETDATE(), @thoiGianDemNguoc, N'Mo')";

            object res = DatabaseConnection.ExecuteScalar(sql, new[]
            {
                new SqlParameter("@maLHP", maLopHP),
                new SqlParameter("@tenBuoiHoc", tenBuoiHoc),
                new SqlParameter("@maOTP", maOTP),
                new SqlParameter("@ipGV", ipGV),
                new SqlParameter("@subnetGV", string.IsNullOrEmpty(subnetMaskGV) ? (object)DBNull.Value : subnetMaskGV),
                new SqlParameter("@thoiGianDemNguoc", thoiGianDemNguoc)
            });

            return Convert.ToInt32(res ?? 0);
        }

        // -------------------------------------------------------------------------------------------------
        // 8. Lấy thông tin chi tiết một buổi học theo MaBuoiHoc
        // -------------------------------------------------------------------------------------------------
        public static ThongTinBuoiHoc? LayThongTinBuoiHoc(int maBuoiHoc)
        {
            string sql = @"
                SELECT 
                    bh.MaBuoiHoc, 
                    bh.MaLopHP, 
                    lhp.TenLopHP,
                    bh.TenBuoiHoc, 
                    bh.NgayHoc, 
                    bh.MaOTP, 
                    bh.IP_GV, 
                    bh.SubnetMask_GV, 
                    bh.ThoiGianBatDau, 
                    bh.ThoiGianDemNguoc, 
                    bh.TrangThai,
                    (SELECT COUNT(1) FROM ChiTietLopHP ct WHERE ct.MaLopHP = bh.MaLopHP) AS TongSiSo
                FROM BuoiHoc bh
                INNER JOIN LopHocPhan lhp ON bh.MaLopHP = lhp.MaLopHP
                WHERE bh.MaBuoiHoc = @maBuoiHoc";

            DataTable dt = DatabaseConnection.ExecuteQuery(sql, new[]
            {
                new SqlParameter("@maBuoiHoc", maBuoiHoc)
            });

            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];
            return new ThongTinBuoiHoc
            {
                MaBuoiHoc = Convert.ToInt32(row["MaBuoiHoc"]),
                MaLopHP = row["MaLopHP"].ToString() ?? string.Empty,
                TenLopHP = row["TenLopHP"].ToString() ?? string.Empty,
                TenBuoiHoc = row["TenBuoiHoc"].ToString() ?? string.Empty,
                NgayHoc = Convert.ToDateTime(row["NgayHoc"]),
                MaOTP = row["MaOTP"].ToString() ?? string.Empty,
                IP_GV = row["IP_GV"].ToString() ?? string.Empty,
                SubnetMask_GV = row["SubnetMask_GV"] == DBNull.Value ? null : row["SubnetMask_GV"].ToString(),
                ThoiGianBatDau = Convert.ToDateTime(row["ThoiGianBatDau"]),
                ThoiGianDemNguoc = Convert.ToInt32(row["ThoiGianDemNguoc"]),
                TrangThai = row["TrangThai"].ToString() ?? string.Empty,
                TongSiSo = Convert.ToInt32(row["TongSiSo"])
            };
        }

        // -------------------------------------------------------------------------------------------------
        // 9. Lấy danh sách điểm danh realtime của buổi học (dùng cho FrmTrinhChieu và DataGridView)
        // -------------------------------------------------------------------------------------------------
        public static DataTable LayDanhSachDiemDanhRealtime(int maBuoiHoc)
        {
            string sql = @"
                SELECT 
                    ROW_NUMBER() OVER (ORDER BY dd.ThoiGianQuet ASC) AS STT,
                    sv.MaSV,
                    sv.HoTen,
                    CONVERT(VARCHAR(8), dd.ThoiGianQuet, 108) AS GioCheckIn,
                    dd.TrangThai,
                    dd.ThoiGianQuet
                FROM DiemDanh dd
                INNER JOIN SinhVien sv ON dd.MaSV = sv.MaSV
                WHERE dd.MaBuoiHoc = @maBuoiHoc
                ORDER BY dd.ThoiGianQuet ASC";

            return DatabaseConnection.ExecuteQuery(sql, new[]
            {
                new SqlParameter("@maBuoiHoc", maBuoiHoc)
            });
        }

        // -------------------------------------------------------------------------------------------------
        // 10. Lấy thống kê số lượng điểm danh realtime nhanh (Có mặt, Đi muộn, Chưa điểm danh)
        // -------------------------------------------------------------------------------------------------
        public static (int daCoMat, int chuaDiemDanh, int tongSiSo) LayThongKeRealtime(int maBuoiHoc, string maLopHP)
        {
            int tongSiSo = LaySiSoLop(maLopHP);

            string sql = @"
                SELECT COUNT(1) 
                FROM DiemDanh 
                WHERE MaBuoiHoc = @maBuoiHoc 
                  AND TrangThai IN (N'Có mặt', N'Đi muộn')";

            object res = DatabaseConnection.ExecuteScalar(sql, new[]
            {
                new SqlParameter("@maBuoiHoc", maBuoiHoc)
            });

            int daCoMat = Convert.ToInt32(res ?? 0);
            int chuaDiemDanh = Math.Max(0, tongSiSo - daCoMat);

            return (daCoMat, chuaDiemDanh, tongSiSo);
        }

        // -------------------------------------------------------------------------------------------------
        // 11. Kiểm tra xem buổi học đã có sinh viên nào điểm danh chưa
        // -------------------------------------------------------------------------------------------------
        public static bool DaCoSinhVienDiemDanh(int maBuoiHoc)
        {
            string sql = "SELECT COUNT(1) FROM DiemDanh WHERE MaBuoiHoc = @maBuoiHoc";
            object res = DatabaseConnection.ExecuteScalar(sql, new[]
            {
                new SqlParameter("@maBuoiHoc", maBuoiHoc)
            });
            return Convert.ToInt32(res ?? 0) > 0;
        }

        // -------------------------------------------------------------------------------------------------
        // 12. Hủy phiên điểm danh (chỉ cho phép nếu chưa có sinh viên nào điểm danh)
        // -------------------------------------------------------------------------------------------------
        public static bool HuyPhienDiemDanh(int maBuoiHoc)
        {
            if (DaCoSinhVienDiemDanh(maBuoiHoc))
            {
                return false;
            }

            string sql = "DELETE FROM BuoiHoc WHERE MaBuoiHoc = @maBuoiHoc AND TrangThai = N'Mo'";
            int affected = DatabaseConnection.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@maBuoiHoc", maBuoiHoc)
            });

            return affected > 0;
        }

        // -------------------------------------------------------------------------------------------------
        // 13. Kết thúc buổi học — gọi Stored Procedure sp_DongBuoiHoc
        // -------------------------------------------------------------------------------------------------
        public static KetQuaDongBuoiHoc DongBuoiHoc(int maBuoiHoc)
        {
            using SqlConnection conn = DatabaseConnection.GetConnection();
            using SqlCommand cmd = new SqlCommand("sp_DongBuoiHoc", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@MaBuoiHoc", maBuoiHoc));

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new KetQuaDongBuoiHoc
                {
                    MaBuoiHoc = reader["MaBuoiHoc"] != DBNull.Value ? Convert.ToInt32(reader["MaBuoiHoc"]) : maBuoiHoc,
                    MaLopHP = reader["MaLopHP"]?.ToString() ?? string.Empty,
                    TongSiSo = reader["TongSiSo"] != DBNull.Value ? Convert.ToInt32(reader["TongSiSo"]) : 0,
                    SoCoMat = reader["SoCoMat"] != DBNull.Value ? Convert.ToInt32(reader["SoCoMat"]) : 0,
                    SoDiMuon = reader["SoDiMuon"] != DBNull.Value ? Convert.ToInt32(reader["SoDiMuon"]) : 0,
                    SoVang = reader["SoVang"] != DBNull.Value ? Convert.ToInt32(reader["SoVang"]) : 0,
                    SoNghiPhep = reader["SoNghiPhep"] != DBNull.Value ? Convert.ToInt32(reader["SoNghiPhep"]) : 0,
                    TongDaDiemDanh = reader["TongDaDiemDanh"] != DBNull.Value ? Convert.ToInt32(reader["TongDaDiemDanh"]) : 0
                };
            }

            return new KetQuaDongBuoiHoc { MaBuoiHoc = maBuoiHoc };
        }

        // -------------------------------------------------------------------------------------------------
        // 14. Kiểm tra và khôi phục phiên đang mở của Giảng viên (khi mở lại ứng dụng)
        // -------------------------------------------------------------------------------------------------
        public static ThongTinBuoiHoc? LayPhienDangMoCuaGiangVien(string maGV)
        {
            string sql = @"
                SELECT TOP 1
                    bh.MaBuoiHoc, 
                    bh.MaLopHP, 
                    lhp.TenLopHP,
                    bh.TenBuoiHoc, 
                    bh.NgayHoc, 
                    bh.MaOTP, 
                    bh.IP_GV, 
                    bh.SubnetMask_GV, 
                    bh.ThoiGianBatDau, 
                    bh.ThoiGianDemNguoc, 
                    bh.TrangThai,
                    (SELECT COUNT(1) FROM ChiTietLopHP ct WHERE ct.MaLopHP = bh.MaLopHP) AS TongSiSo
                FROM BuoiHoc bh
                INNER JOIN LopHocPhan lhp ON bh.MaLopHP = lhp.MaLopHP
                WHERE lhp.MaGV = @maGV 
                  AND bh.TrangThai = N'Mo'
                  AND DATEADD(MINUTE, @gioiHanPhut, bh.ThoiGianBatDau) > GETDATE()
                ORDER BY bh.ThoiGianBatDau DESC";

            DataTable dt = DatabaseConnection.ExecuteQuery(sql, new[]
            {
                new SqlParameter("@maGV", maGV),
                new SqlParameter("@gioiHanPhut", AppConfig.GioiHanTuDongDongPhien)
            });

            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];
            return new ThongTinBuoiHoc
            {
                MaBuoiHoc = Convert.ToInt32(row["MaBuoiHoc"]),
                MaLopHP = row["MaLopHP"].ToString() ?? string.Empty,
                TenLopHP = row["TenLopHP"].ToString() ?? string.Empty,
                TenBuoiHoc = row["TenBuoiHoc"].ToString() ?? string.Empty,
                NgayHoc = Convert.ToDateTime(row["NgayHoc"]),
                MaOTP = row["MaOTP"].ToString() ?? string.Empty,
                IP_GV = row["IP_GV"].ToString() ?? string.Empty,
                SubnetMask_GV = row["SubnetMask_GV"] == DBNull.Value ? null : row["SubnetMask_GV"].ToString(),
                ThoiGianBatDau = Convert.ToDateTime(row["ThoiGianBatDau"]),
                ThoiGianDemNguoc = Convert.ToInt32(row["ThoiGianDemNguoc"]),
                TrangThai = row["TrangThai"].ToString() ?? string.Empty,
                TongSiSo = Convert.ToInt32(row["TongSiSo"])
            };
        }
    }
}
