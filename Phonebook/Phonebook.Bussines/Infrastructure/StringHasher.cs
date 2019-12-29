using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Infrastructure
{
    public class StringHasher
    {
        public static string GenerateHash(string password)
        {
            using (MD5 _md5 = MD5.Create())
            {
                string hash = "";
                byte[] passwordbytes = Encoding.UTF8.GetBytes(password);
                byte[] hashbytes = _md5.ComputeHash(passwordbytes);

                for (int i = 0; i < hashbytes.Length; i++)
                {
                    hash += hashbytes[i].ToString("x2");
                }

                return hash;
            }
        }

        public static bool VerifyHash(string password, string hashedpassword)
        {
            string hashedInputPassword = GenerateHash(password);

            return string.Equals(hashedInputPassword, hashedpassword, StringComparison.Ordinal);
        }
    }
}
