using XCommon.Extensions.String;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace XCommon.Util
{
    public static class StringUtils
    {
        public static string Randon(int size)
        {
            Byte[] seedBuffer = new Byte[4];

            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetBytes(seedBuffer);
                Random random = new Random(BitConverter.ToInt32(seedBuffer, 0));
                return new String(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", size).Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }

        public static string FirstPartEmail(string email)
        {
            return email.Split('@').FirstOrDefault();
        }

        public static string GetMD5(string value)
        {
            if (value.IsEmpty())
                return string.Empty;

            byte[] encodeValue = new UTF8Encoding().GetBytes(value);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodeValue);

            return BitConverter.ToString(hash)
               .Replace("-", string.Empty);
        }
    }
}
