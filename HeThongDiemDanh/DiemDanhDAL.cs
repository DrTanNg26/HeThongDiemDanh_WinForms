using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh
{
    public static class DiemDanhDAL
    {
        public static async Task<bool> KiemTraXacThucAsync(string maSV, int maBuoiHoc,
            string loaiCauHoi, string traLoi, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(maSV) || maSV.Length > 10 || maBuoiHoc <= 0)
            {
                throw new InvalidOperationException("Thông tin sinh viên hoặc buổi học không hợp lệ.");
            }
            // 1. Kiểm tra định dạng trước khi hỏi cơ sở dữ liệu.
            traLoi = traLoi.Trim();
            DateTime ngaySinh;
            if (!KiemTraDinhDang(loaiCauHoi, traLoi, out ngaySinh))
            {
                return false;
            }

            // 2. Chỉ cho xác thực khi tài khoản còn hoạt động và thuộc buổi đang mở.
            string sql = "DECLARE @HienTai datetime = GETDATE(); ";
            sql += "SELECT sv.NgaySinh, RIGHT(sv.SoCCCD, 4) AS DuoiCCCD ";
            sql += "FROM dbo.SinhVien sv ";
            sql += "INNER JOIN dbo.TaiKhoan tk ON tk.TenDangNhap = sv.MaSV ";
            sql += "INNER JOIN dbo.ChiTietLopHP ct ON ct.MaSV = sv.MaSV ";
            sql += "INNER JOIN dbo.BuoiHoc bh ON bh.MaLopHP = ct.MaLopHP ";
            sql += "WHERE sv.MaSV = @MaSV AND bh.MaBuoiHoc = @MaBuoiHoc ";
            sql += "AND tk.VaiTro = N'SinhVien' AND tk.TrangThai = N'Hoạt động' ";
            sql += "AND bh.TrangThai = N'Mo' AND bh.ThoiGianBatDau <= @HienTai ";
            sql += "AND DATEADD(MINUTE, bh.ThoiGianDemNguoc, bh.ThoiGianBatDau) > @HienTai";

            try
            {
                using (SqlConnection con = DatabaseConnection.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@MaSV", SqlDbType.VarChar, 10).Value = maSV;
                    cmd.Parameters.Add("@MaBuoiHoc", SqlDbType.Int).Value = maBuoiHoc;
                    cmd.Parameters.Add("@GioiHanPhut", SqlDbType.Int).Value = LayGioiHanPhut();
                    await con.OpenAsync(token);

                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync(token))
                    {
                        if (!await dr.ReadAsync(token))
                        {
                            throw new InvalidOperationException("Buổi điểm danh đã đóng, hết hạn hoặc tài khoản không còn được tham gia. Hãy làm mới danh sách.");
                        }

                        if (loaiCauHoi == "NgaySinh")
                        {
                            return dr.GetDateTime(0).Date == ngaySinh.Date;
                        }
                        else
                        {
                            return string.Equals(dr.GetString(1), traLoi, StringComparison.Ordinal);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw TaoLoiDeHieu(ex);
            }
        }

        public static async Task<string> GhiNhanDiemDanhAsync(string maSV, int maBuoiHoc,
            string otp, string loaiCauHoi, string traLoi, CancellationToken token)
        {
            otp = otp.Trim();
            traLoi = traLoi.Trim();
            DateTime ngaySinh;
            if (string.IsNullOrWhiteSpace(maSV) || maSV.Length > 10 || maBuoiHoc <= 0)
            {
                throw new InvalidOperationException("Thông tin sinh viên hoặc buổi học không hợp lệ.");
            }
            if (otp.Length < 5 || otp.Length > 6)
            {
                throw new InvalidOperationException("Hãy nhập mã OTP gồm 5 hoặc 6 ký tự do giảng viên cung cấp.");
            }
            if (!KiemTraDinhDang(loaiCauHoi, traLoi, out ngaySinh))
            {
                throw new InvalidOperationException("Thông tin xác thực không hợp lệ. Hãy xác thực lại.");
            }

            int gioiHanPhut = LayGioiHanPhut();
            try
            {
                using (SqlConnection con = DatabaseConnection.GetConnection())
                {
                    await con.OpenAsync(token);

                    ThongTinMang mang = await LayMangCungGiangVienAsync(con, maBuoiHoc, token);

                    // 3. SQL Server dùng giờ máy chủ và một giao dịch để chống ghi trùng.
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_GhiNhanDiemDanh", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MaSV", SqlDbType.VarChar, 10).Value = maSV;
                        cmd.Parameters.Add("@MaBuoiHoc", SqlDbType.Int).Value = maBuoiHoc;
                        cmd.Parameters.Add("@MaOTP", SqlDbType.VarChar, 10).Value = otp;
                        cmd.Parameters.Add("@LoaiCauHoi", SqlDbType.VarChar, 20).Value = loaiCauHoi;
                        cmd.Parameters.Add("@TraLoi", SqlDbType.NVarChar, 100).Value = traLoi;
                        cmd.Parameters.Add("@IP_SV", SqlDbType.VarChar, 50).Value = mang.DiaChiIP;
                        cmd.Parameters.Add("@BoQuaKiemTraIP", SqlDbType.Bit).Value = false;
                        cmd.Parameters.Add("@GioiHanPhut", SqlDbType.Int).Value = gioiHanPhut;

                        object? ketQua = await cmd.ExecuteScalarAsync(token);
                        if (ketQua == null || ketQua == DBNull.Value)
                        {
                            throw new InvalidOperationException("Máy chủ chưa trả kết quả điểm danh. Hãy làm mới danh sách trước khi thử lại.");
                        }

                        return Convert.ToString(ketQua, CultureInfo.InvariantCulture) ?? string.Empty;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw TaoLoiDeHieu(ex);
            }
        }

        public static async Task<ThongTinMang> KiemTraMangAsync(int maBuoiHoc, CancellationToken token)
        {
            try
            {
                using (SqlConnection con = DatabaseConnection.GetConnection())
                {
                    await con.OpenAsync(token);
                    return await LayMangCungGiangVienAsync(con, maBuoiHoc, token);
                }
            }
            catch (SqlException ex)
            {
                throw TaoLoiDeHieu(ex);
            }
        }

        private static async Task<ThongTinMang> LayMangCungGiangVienAsync(SqlConnection con,
            int maBuoiHoc, CancellationToken token)
        {
            string ipGiangVien = "";
            string maskGiangVien = "";
            string sql = "SELECT IP_GV, SubnetMask_GV FROM dbo.BuoiHoc WHERE MaBuoiHoc = @MaBuoiHoc";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add("@MaBuoiHoc", SqlDbType.Int).Value = maBuoiHoc;
                using (SqlDataReader dr = await cmd.ExecuteReaderAsync(token))
                {
                    if (!await dr.ReadAsync(token))
                    {
                        throw new InvalidOperationException("Buổi học không còn tồn tại. Hãy làm mới danh sách.");
                    }
                    if (!dr.IsDBNull(0)) ipGiangVien = dr.GetString(0);
                    if (!dr.IsDBNull(1)) maskGiangVien = dr.GetString(1);
                }
            }
            if (!NetworkHelper.CungMang(ipGiangVien, ipGiangVien, maskGiangVien))
            {
                throw new InvalidOperationException("Buổi học chưa có IP hoặc subnet mask hợp lệ. Hãy báo giảng viên mở lại buổi trên mạng phòng học.");
            }

            List<ThongTinMang> cacMang = NetworkHelper.LayDanhSachMang();
            if (cacMang.Count == 0)
            {
                throw new InvalidOperationException("Chưa tìm thấy Wi-Fi/Ethernet đang kết nối. Hãy kết nối mạng phòng học rồi thử lại.");
            }
            foreach (ThongTinMang mang in cacMang)
            {
                // Hai địa chỉ phải cùng subnet xét theo cả mask của GV và SV.
                if (NetworkHelper.CungMang(mang.DiaChiIP, ipGiangVien, maskGiangVien)
                    && NetworkHelper.CungMang(mang.DiaChiIP, ipGiangVien, mang.SubnetMask))
                {
                    return mang;
                }
            }
            throw new InvalidOperationException("Bạn chưa cùng mạng với giảng viên. Hãy kết nối Wi-Fi/LAN phòng học rồi bấm Kiểm tra mạng.");
        }

        private static bool KiemTraDinhDang(string loaiCauHoi, string traLoi, out DateTime ngaySinh)
        {
            ngaySinh = DateTime.MinValue;
            if (loaiCauHoi == "NgaySinh")
            {
                return DateTime.TryParseExact(traLoi, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out ngaySinh);
            }
            else if (loaiCauHoi == "CCCD")
            {
                if (traLoi.Length != 4)
                {
                    return false;
                }
                foreach (char kyTu in traLoi)
                {
                    if (kyTu < '0' || kyTu > '9')
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private static int LayGioiHanPhut()
        {
            int gioiHan = AppConfig.GioiHanTuDongDongPhien;
            if (gioiHan <= 0 || gioiHan > 1440)
            {
                throw new InvalidOperationException("Cấu hình thời hạn buổi điểm danh phải từ 1 đến 1440 phút.");
            }
            return gioiHan;
        }

        private static InvalidOperationException TaoLoiDeHieu(SqlException ex)
        {
            if (ex.Number == 2627 || ex.Number == 2601)
            {
                return new InvalidOperationException("Bạn đã có kết quả điểm danh trong buổi này. Hãy làm mới danh sách.", ex);
            }
            if (ex.Number == 2812)
            {
                return new InvalidOperationException("Cơ sở dữ liệu chưa có chức năng ghi nhận điểm danh. Hãy chạy script 04_GhiNhanDiemDanh.sql trước.", ex);
            }
            if (ex.Number >= 50000 && ex.Number < 51000)
            {
                return new InvalidOperationException(ex.Message, ex);
            }
            return new InvalidOperationException("Không thể trao đổi dữ liệu điểm danh với SQL Server. Hãy kiểm tra kết nối rồi làm mới danh sách trước khi thử lại.", ex);
        }
    }
}
