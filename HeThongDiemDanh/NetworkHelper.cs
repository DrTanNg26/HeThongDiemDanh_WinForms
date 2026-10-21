using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace HeThongDiemDanh
{
    public class ThongTinMang
    {
        public string TenCardMang { get; set; } = string.Empty;
        public string DiaChiIP { get; set; } = string.Empty;
        public string SubnetMask { get; set; } = string.Empty;
    }

    public static class NetworkHelper
    {
        public static List<ThongTinMang> LayDanhSachMang()
        {
            List<ThongTinMang> danhSach = new List<ThongTinMang>();

            // Chỉ lấy Wi-Fi hoặc Ethernet đang kết nối, bỏ qua card mạng ảo và VPN.
            NetworkInterface[] cacCardMang = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface cardMang in cacCardMang)
            {
                if (cardMang.OperationalStatus != OperationalStatus.Up)
                {
                    continue;
                }

                if (cardMang.NetworkInterfaceType != NetworkInterfaceType.Wireless80211
                    && cardMang.NetworkInterfaceType != NetworkInterfaceType.Ethernet
                    && cardMang.NetworkInterfaceType != NetworkInterfaceType.GigabitEthernet
                    && cardMang.NetworkInterfaceType != NetworkInterfaceType.FastEthernetT)
                {
                    continue;
                }

                string tenCard = (cardMang.Name + " " + cardMang.Description).ToLowerInvariant();
                string[] tuKhoaCardAo = { "virtual", "vmware", "hyper-v", "vethernet", "vpn", "tap-", "tun-", "tunnel", "wireguard", "tailscale", "zerotier", "loopback", "docker", "bluetooth" };
                bool laCardAo = false;
                foreach (string tuKhoa in tuKhoaCardAo)
                {
                    if (tenCard.Contains(tuKhoa))
                    {
                        laCardAo = true;
                        break;
                    }
                }

                if (laCardAo)
                {
                    continue;
                }

                IPInterfaceProperties thongTin = cardMang.GetIPProperties();
                foreach (UnicastIPAddressInformation diaChi in thongTin.UnicastAddresses)
                {
                    if (diaChi.Address.AddressFamily != AddressFamily.InterNetwork)
                    {
                        continue;
                    }

                    byte[] cacByte = diaChi.Address.GetAddressBytes();
                    if (IPAddress.IsLoopback(diaChi.Address) || cacByte[0] == 0
                        || cacByte[0] >= 224 || (cacByte[0] == 169 && cacByte[1] == 254))
                    {
                        continue;
                    }

                    string ip = diaChi.Address.ToString();
                    string mask = diaChi.IPv4Mask.ToString();
                    if (!CungMang(ip, ip, mask))
                    {
                        continue;
                    }

                    ThongTinMang mang = new ThongTinMang();
                    mang.TenCardMang = cardMang.Name;
                    mang.DiaChiIP = ip;
                    mang.SubnetMask = mask;
                    danhSach.Add(mang);
                }
            }

            return danhSach;
        }

        public static bool CungMang(string ip1, string ip2, string mask)
        {
            uint soIP1;
            uint soIP2;
            uint soMask;
            if (!ChuyenIPv4ThanhSo(ip1, out soIP1) || !ChuyenIPv4ThanhSo(ip2, out soIP2)
                || !ChuyenIPv4ThanhSo(mask, out soMask))
            {
                return false;
            }

            // Không dùng loopback, link-local, multicast hoặc địa chỉ chưa được cấp.
            if (!LaIPv4MangNoiBo(soIP1) || !LaIPv4MangNoiBo(soIP2))
            {
                return false;
            }

            // Mask hợp lệ có các bit 1 liền nhau rồi tới các bit 0 (ví dụ /24).
            // Không chấp nhận 0.0.0.0 vì mask này không giới hạn mạng điểm danh.
            ulong phanDao = uint.MaxValue - (ulong)soMask;
            if (soMask == 0 || (phanDao & (phanDao + 1)) != 0)
            {
                return false;
            }

            return (soIP1 & soMask) == (soIP2 & soMask);
        }

        private static bool LaIPv4MangNoiBo(uint ip)
        {
            uint phanDau = ip >> 24;
            uint haiPhanDau = ip >> 16;
            return phanDau >= 1 && phanDau <= 223 && phanDau != 127
                && haiPhanDau != (169 * 256 + 254);
        }

        private static bool ChuyenIPv4ThanhSo(string diaChi, out uint ketQua)
        {
            ketQua = 0;
            if (string.IsNullOrWhiteSpace(diaChi))
            {
                return false;
            }

            string[] cacPhan = diaChi.Split('.');
            if (cacPhan.Length != 4)
            {
                return false;
            }

            foreach (string phan in cacPhan)
            {
                byte giaTri;
                if (phan.Length == 0 || phan.Length > 3
                    || !byte.TryParse(phan, NumberStyles.None, CultureInfo.InvariantCulture, out giaTri))
                {
                    return false;
                }

                ketQua = (ketQua << 8) + giaTri;
            }

            return true;
        }
    }
}
