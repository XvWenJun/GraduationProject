using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuotationSystem.BLL.Encrypt
{
    public class Md5Encrypt
    {
        public static string initPwd = "000000";

        public static string Encrypt(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            MD5 md5 = MD5.Create();
            byte[] encrypt = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            return Convert.ToBase64String(encrypt);
        }

        public static string GetInitPwd()
        {
            return Encrypt(initPwd);
        }
    }
}