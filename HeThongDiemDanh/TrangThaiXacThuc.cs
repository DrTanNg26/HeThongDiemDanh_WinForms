using System;

namespace HeThongDiemDanh
{
    // Mỗi buổi học có bộ đếm riêng. Đóng/mở lại popup không làm mất số lần sai.
    public class TrangThaiXacThuc
    {
        public string LoaiCauHoi { get; private set; }
        public int SoLanSai { get; private set; }

        public TrangThaiXacThuc()
        {
            if (Random.Shared.Next(2) == 0)
            {
                LoaiCauHoi = "NgaySinh";
            }
            else
            {
                LoaiCauHoi = "CCCD";
            }
        }

        public void GhiNhanTraLoiSai()
        {
            SoLanSai = SoLanSai + 1;
        }

        public bool DaBiKhoa()
        {
            if (SoLanSai >= 3)
            {
                return true;
            }
            return false;
        }
    }
}
