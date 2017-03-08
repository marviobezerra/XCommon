using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace XCommon.Extensions.String
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !value.IsEmpty();
        }
        
        public static byte[] ToByte(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        public static string RemoveAcent(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
			{
				return value;
			}

			value = value.Normalize(NormalizationForm.FormD);
            var chars = value.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }
	}
}
