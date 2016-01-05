using System;
using System.Collections.Generic;

namespace XCommon.Extensions.Converters
{
    public static class ConverterInt32
    {
        public static int ToInt32(this object valor)
        {
            if (valor == null)
                return 0;

            int resultado = 0;
            if (int.TryParse(valor.ToString(), out resultado))
                return resultado;
            else
                return 0;
        }

        public static long ToInt64(this object valor)
        {
            if (valor == null)
                return 0;

            long resultado = 0;
            if (long.TryParse(valor.ToString(), out resultado))
                return resultado;
            else
                return 0;
        }

        public static List<long> ToListInt64(this IEnumerable<object> list)
        {
            List<long> result = new List<long>();

            foreach (var item in list)
            {
                var value = item.ToInt64OrNull();

                if (value.HasValue)
                    result.Add(value.Value);
            }

            return result;
        }

        public static long? ToInt64OrNull(this object valor)
        {
            if (valor == null)
                return 0;

            long resultado = 0;
            if (long.TryParse(valor.ToString(), out resultado))
                return resultado;
            else
                return null;
        }

        public static int? ToInt32OrNull(this object valor)
        {
            if (valor == null)
                return null;

            int resultado = 0;
            if (int.TryParse(valor.ToString(), out resultado))
                return resultado;
            else
                return null;
        }
    }
}
