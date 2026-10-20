using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh
{
    public static class SinhVienDAL
    {
        public static async Task<DataTable> LayBuoiHocDangMoAsync(string maSV, CancellationToken cancellationToken)
        {
            // 1. Lấy các buổi học đang mở của lớp mà sinh viên đã được phân vào.
            string sql = "SELECT bh.MaBuoiHoc, mh.TenMon, lhp.TenLopHP, gv.HoTen AS GiangVien, ";
            sql += "lhp.PhongHoc, bh.NgayHoc, bh.ThoiGianBatDau, bh.ThoiGianDemNguoc, ";
            sql += "COALESCE(dd.TrangThai, N'Chưa điểm danh') AS KetQua ";
            sql += "FROM ChiTietLopHP ct ";
            sql += "INNER JOIN LopHocPhan lhp ON lhp.MaLopHP = ct.MaLopHP ";
            sql += "INNER JOIN MonHoc mh ON mh.MaMon = lhp.MaMon ";
            sql += "INNER JOIN GiangVien gv ON gv.MaGV = lhp.MaGV ";
            sql += "INNER JOIN BuoiHoc bh ON bh.MaLopHP = lhp.MaLopHP ";
            sql += "LEFT JOIN DiemDanh dd ON dd.MaBuoiHoc = bh.MaBuoiHoc AND dd.MaSV = ct.MaSV ";
            sql += "WHERE ct.MaSV = @MaSV AND bh.TrangThai = N'Mo' ";
            sql += "AND bh.ThoiGianBatDau <= GETDATE() ";
            sql += "AND DATEADD(MINUTE, bh.ThoiGianDemNguoc, bh.ThoiGianBatDau) > GETDATE() ";
            sql += "ORDER BY bh.ThoiGianBatDau DESC, bh.MaBuoiHoc DESC";

            // 2. Tạo kết nối và câu lệnh. using sẽ tự đóng, kể cả khi có lỗi.
            using (SqlConnection con = DatabaseConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                // Truyền giá trị riêng bằng tham số, không nối dữ liệu người dùng vào SQL.
                cmd.Parameters.Add("@MaSV", SqlDbType.VarChar, 10).Value = maSV;

                // 3. Mở kết nối và đọc dữ liệu mà không làm đứng giao diện.
                await con.OpenAsync(cancellationToken);
                using (SqlDataReader dr = await cmd.ExecuteReaderAsync(cancellationToken))
                {
                    DataTable dt = new DataTable();

                    // Tạo các cột của DataTable theo kết quả SQL trả về.
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        dt.Columns.Add(dr.GetName(i), dr.GetFieldType(i));
                    }

                    // Đọc từng dòng và thêm vào DataTable.
                    while (await dr.ReadAsync(cancellationToken))
                    {
                        object[] giaTri = new object[dr.FieldCount];
                        dr.GetValues(giaTri);
                        dt.Rows.Add(giaTri);
                    }

                    // 4. Trả bảng để Form gán vào DataGridView.DataSource.
                    return dt;
                }
            }
        }
    }
}
