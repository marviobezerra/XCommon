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

        public static Guid ToGuid(this object Valor)
        {
            if (Valor == null)
                return Guid.Empty;

            Guid Retorno = Guid.Empty;
            Guid.TryParse(Valor.ToString(), out Retorno);
            return Retorno;
        }

        public static Guid NewIfEmpty(this Guid? Valor)
        {
            return !Valor.HasValue || Valor == Guid.Empty ? Guid.NewGuid() : Valor.Value;
        }

        public static Guid? ToGuidOrNull(this object Valor)
        {
            if (Valor == null)
                return null;

            Guid Retorno;
            if (Guid.TryParse(Valor.ToString(), out Retorno))
                return Retorno;
            else
                return null;
        }

        public static Guid ToGuid(this int valor)
        {
            return new Guid(string.Format("00000000-0000-0000-0000-{0}", valor.ToString().PadLeft(12, '0')));
        }
    }
}
