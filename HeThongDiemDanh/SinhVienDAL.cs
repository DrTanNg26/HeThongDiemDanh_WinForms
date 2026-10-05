using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongDiemDanh;

public static class SinhVienDAL
{
    // Chỉ đọc các phiên của lớp mà sinh viên đã được phân vào.
    public static async Task<DataTable> LayBuoiHocDangMoAsync(string maSV, CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT bh.MaBuoiHoc, mh.TenMon, lhp.TenLopHP, gv.HoTen AS GiangVien,
                   lhp.PhongHoc, bh.NgayHoc, bh.ThoiGianBatDau, bh.ThoiGianDemNguoc,
                   COALESCE(dd.TrangThai, N'Chưa điểm danh') AS KetQua
            FROM ChiTietLopHP ct
            INNER JOIN LopHocPhan lhp ON lhp.MaLopHP = ct.MaLopHP
            INNER JOIN MonHoc mh ON mh.MaMon = lhp.MaMon
            INNER JOIN GiangVien gv ON gv.MaGV = lhp.MaGV
            INNER JOIN BuoiHoc bh ON bh.MaLopHP = lhp.MaLopHP
            LEFT JOIN DiemDanh dd ON dd.MaBuoiHoc = bh.MaBuoiHoc AND dd.MaSV = ct.MaSV
            WHERE ct.MaSV = @MaSV AND bh.TrangThai = N'Mo'
              AND bh.ThoiGianBatDau <= GETDATE()
              AND DATEADD(MINUTE, @GioiHanPhut, bh.ThoiGianBatDau) > GETDATE()
            ORDER BY bh.ThoiGianBatDau DESC, bh.MaBuoiHoc DESC";

        using var connection = DatabaseConnection.GetConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@MaSV", SqlDbType.VarChar, 10).Value = maSV;
        command.Parameters.Add("@GioiHanPhut", SqlDbType.Int).Value = AppConfig.GioiHanTuDongDongPhien;
        await connection.OpenAsync(cancellationToken);
        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var table = new DataTable();
        for (int i = 0; i < reader.FieldCount; i++)
            table.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
        while (await reader.ReadAsync(cancellationToken))
        {
            var values = new object[reader.FieldCount];
            reader.GetValues(values);
            table.Rows.Add(values);
        }
        return table;
    }
}
