using System;
using System.Collections.Generic;

namespace XCommon.Extensions.Converters
{
    public static class ConverterGuid
    {
        public static List<Guid> ToGuidList(this object Valor, char separador = ',')
        {
            List<Guid> retorno = new List<Guid>();

            if (Valor == null)
                return retorno;

            Guid aux = Guid.Empty;

            foreach (var item in Valor.ToString().Split(separador))
            {
                if (Guid.TryParse(item, out aux))
                    retorno.Add(aux);
            }

            return retorno;
        }

        public static Guid NewIfEmpty(this Guid? value)
        {
            return !value.HasValue || value == Guid.Empty ? Guid.NewGuid() : value.Value;
        }

        public static Guid? ToGuidOrNull(this object value)
        {
            if (value == null)
                return null;

            Guid result;
            if (Guid.TryParse(value.ToString(), out result))
                return result;
            else
                return null;
        }

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
