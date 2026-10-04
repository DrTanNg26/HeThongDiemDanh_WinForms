using System;
using System.Security.Cryptography;
using System.Text;

namespace HeThongDiemDanh
{
    public static class PasswordHelper
    {
        public static string BamSHA256(string plainText)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        public static bool KiemTra(string plainText, string hashDaLuu)
        {
            return BamSHA256(plainText) == hashDaLuu;
        }
    }
}