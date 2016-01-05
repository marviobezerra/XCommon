using XCommon.Util;

namespace XCommon.Extensions.Converters
{
    public static class ConverterBooleanOption
    {
        public static BooleanOption ToBooleanOption(this object valor)
        {
            return valor.ToBooleanOption(BooleanOption.All);
        }

        public static BooleanOption ToBooleanOption(this object valor, BooleanOption padrao)
        {
            BooleanOption retorno = padrao;

            if (valor == null)
                return retorno;

            if (valor is bool)
            {
                retorno = (bool)valor ? BooleanOption.True : BooleanOption.False;
            }

            return retorno;
        }

        public static bool? ToBoolByBooleanOption(this BooleanOption valor)
        {
            switch (valor)
            {
                case BooleanOption.True:
                    return true;
                case BooleanOption.False:
                    return false;
                case BooleanOption.All:
                default:
                    return null;
            }
        }

        public static bool ToBoolean(this object value)
        {
            try
            {
                bool resul = false;
                bool.TryParse(value.ToString(), out resul);
                return resul;
            }
            catch
            {
                return false;
            }
        }
    }
}
