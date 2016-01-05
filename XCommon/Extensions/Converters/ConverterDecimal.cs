
namespace XCommon.Extensions.Converters
{
    public static class ConverterDecimal
    {
        public static decimal ToDecimal(this string valor)
        {
            valor = valor.Replace(".", ",");
            decimal resultado = 0;
            decimal.TryParse(valor, out resultado);
            return resultado;
        }

        public static decimal ToDecimal(this object valor)
        {
            if (valor == null)
                return 0;

            decimal resultado = 0;
            decimal.TryParse(valor.ToString(), out resultado);
            return resultado;
        }
    }
}
