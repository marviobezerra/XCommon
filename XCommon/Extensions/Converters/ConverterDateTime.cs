using System;

namespace XCommon.Extensions.Converters
{
    public static class ConverterDateTime
    {
        public static DateTime? ToDateTimeOrNull(this object Valor)
        {
            if (Valor == null)
                return null;

            DateTime Retorno;
            if (DateTime.TryParse(Valor.ToString(), out Retorno))
                return Retorno;
            else
                return null;
        }

        public static DateTime ToDateTime(this object Valor)
        {
            if (Valor == null)
                return DateTime.Now;

            DateTime Retorno;
            if (DateTime.TryParse(Valor.ToString(), out Retorno))
                return Retorno;
            else
                return DateTime.Now;
        }

        public static DateTime IfMinDate(this DateTime value, DateTime valueIfNull)
        {
            return value != DateTime.MinValue
                ? value
                : valueIfNull;
        }
    }
}
