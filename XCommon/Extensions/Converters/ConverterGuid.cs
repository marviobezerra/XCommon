using System;

namespace XCommon.Extensions.Converters
{
    public static class ConverterGuid
    {
        public static Guid ToGuid(this string value)
        {
            if (value == null)
                return Guid.Empty;

            if (value.Length <= 12)
                return new Guid(string.Format("00000000-0000-0000-0000-{0}", value.PadLeft(12, '0')));
            
            Guid result = Guid.Empty;
            Guid.TryParse(value, out result);
            return result;
        }

        public static Guid ToGuid(this int value)
        {
            return value.ToString().ToGuid();
        }
    }
}
