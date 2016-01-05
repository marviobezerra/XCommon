using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace XCommon.Extensions.String
{
    public static class Utity
    {
        public static string Remove(this string value, params string[] caracteres)
        {
            if (caracteres == null)
                return string.Empty;

            foreach (string item in caracteres)
                value = value.Replace(item, string.Empty);

            return value;
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static string IfNull(this string value, string valueIfNull)
        {
            return value.IsNotEmpty()
                ? value
                : valueIfNull;
        }

        public static string ToUtf8(this string value)
        {
            var utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(value);

            return utf8.GetString(utfBytes, 0, utfBytes.Length);
        }

        public static byte[] ToByte(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        public static string RemoveAcento(this string value)
        {
            if (value.IsEmpty())
                return value;

            StringBuilder sbReturn = new StringBuilder();

            byte[] tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(value);
            var arrayText = Encoding.UTF8.GetString(tempBytes, 0, tempBytes.Length).ToCharArray();

            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }

            return sbReturn.ToString();
        }

        public static string RemoveExtraSpace(this string value)
        {
            return Regex.Replace(value, @"\s+", " ");
        }

        public static string StringNormalize(this string value)
        {
            value = value.ToLower();
            value = value.RemoveAcento();
            value = value.RemoveExtraSpace();
            return value;
        }

        public static List<string> StringSplit(this string value, string format, char split = ' ')
        {
            List<string> result = new List<string>();

            foreach (string item in value.Split(split))
            {
                result.Add(string.Format(format, item));
            }

            return result;
        }

        public static List<string> MakePrefix(this string value, string format)
        {
            List<string> result = new List<string>();

            value = value.StringNormalize();

            foreach (var item in value.Split(' '))
            {
                for (int i = 0; i < item.Length; i++)
                {
                    result.Add(string.Format(format, item.Substring(0, i + 1)));
                }
            }

            return result.Distinct().ToList();
        }

        public static bool ValueContains(this string value, string strB)
        {
            return ValueContains(value, strB, CompareType.Invariant);
        }

        public static bool ValueContains(this string value, string strB, CompareType type)
        {
            if (value.IsEmpty() || strB.IsEmpty())
                return false;

            if (type == CompareType.Default)
                return value.Contains(strB);

            return value.RemoveAcento().ToLower().Contains(strB.RemoveAcento().ToLower());
        }

        public static bool ValueEquals(this string value, string strB)
        {
            return ValueEquals(value, strB, CompareType.Invariant);
        }

        public static bool ValueEquals(this string value, string strB, CompareType type)
        {
            if (type == CompareType.Default)
                return value.Equals(strB);

            if (value.IsEmpty() && !strB.IsEmpty())
                return false;

            if (!value.IsEmpty() && strB.IsEmpty())
                return false;

            return value.RemoveAcento().ToLower().Trim().Equals(strB.RemoveAcento().ToLower().Trim());
        }
    }
}
