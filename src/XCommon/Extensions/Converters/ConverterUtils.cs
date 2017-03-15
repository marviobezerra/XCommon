using System;
using System.IO;
using System.Linq;
using XCommon.Extensions.String;
using XCommon.Util;

namespace XCommon.Extensions.Converters
{
    public static class ConverterUtils
    {
        public static Stream ToStream(this byte[] input)
        {
            return new MemoryStream(input);
        }

        public static byte[] ToByte(this Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

        public static TEnum ToEnum<TEnum>(this string value)
            where TEnum : struct, IConvertible
        {
            if (string.IsNullOrEmpty(value))
            {
                return default(TEnum);
            }

			var intResult = -1;
			var values = Enum.GetValues(typeof(TEnum)).Cast<int>();

            if (!Enum.TryParse(value, true, out TEnum enumresult))
                return default(TEnum);

            intResult = enumresult.ToInt32();

            if (!values.Contains(intResult))
                return default(TEnum);
            
            return  enumresult;
        }

        public static Guid ToGuid(this string value)
        {
            if (value.IsEmpty())
                return Guid.Empty;

            var result = Guid.Empty;

            if (value.Length <= 12)
            {
                Guid.TryParse(string.Format("00000000-0000-0000-0000-{0}", value.Trim().PadLeft(12, '0')), out result);
                return result;
            }

            Guid.TryParse(value, out result);
            return result;
        }

        public static int ToInt32(this object value, int defaultValue = 0)
        {
            if (value == null)
                return defaultValue;


			if (int.TryParse(value.ToString(), out int result))
				return result;

			return defaultValue;
        }

        public static bool ToBool(this BooleanOption value, bool defaultValue = true)
        {
            switch (value)
            {
                case BooleanOption.True:
                    return true;
                case BooleanOption.False:
                    return false;
                case BooleanOption.All:
                default:
                    return defaultValue;
            }
        }
    }
}
