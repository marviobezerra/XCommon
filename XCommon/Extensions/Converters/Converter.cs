using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace XCommon.Extensions.Converters
{
    public static class Converter
    {
        public static TEntity Convert<TEntity>(this object source, params string[] ignore)
            where TEntity : class, new()
        {
            return source.Convert<TEntity>(true, ignore);
        }

        public static TEntity Convert<TEntity>(this object source, bool nullIfEmpty, params string[] ignore)
            where TEntity : class, new()
        {
            var retorno = new TEntity();

            if (source == null)
                return nullIfEmpty ? null : retorno;

            var propriedadesOrigem = source.GetType().GetProperties();
            var propriedadesDestino = retorno.GetType().GetProperties();

            foreach (var dadoDestino in propriedadesDestino)
            {
                if (ignore.Length > 0 && ignore.Contains(dadoDestino.Name))
                    continue;

                var busca = propriedadesOrigem.Where(c => c.Name == dadoDestino.Name && c.PropertyType == dadoDestino.PropertyType);

                if (busca.Count() <= 0)
                    continue;

                var dadoOrigem = busca.First();

                if (dadoDestino.CanWrite && dadoOrigem.CanRead)
                    dadoDestino.SetValue(retorno, dadoOrigem.GetValue(source, null), null);
            }

            return retorno;
        }

        public static List<TEntity> Convert<TEntity, TSource>(this ICollection<TSource> source, params string[] ignore)
            where TEntity : class, new()
            where TSource : class
        {
            var retorno = new List<TEntity>();

            foreach (TSource item in source)
            {
                retorno.Add(item.Convert<TEntity>(ignore));
            }

            return retorno;
        }

        public static Stream ToStream(this byte[] input)
        {
            return new MemoryStream(input);
        }

        public static byte[] ToByte(this Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

        public static double ToDouble(this DateTime value)
        {
            var stringValue = value.ToString("yyyyMMddhhmm");
            double result = 0;
            double.TryParse(stringValue, out result);
            return result;
        }

        public static DateTime ToDate(this double value)
        {
            DateTime result = DateTime.MinValue;
            DateTime.TryParse(value.ToString(), out result);
            return result;
        }

        public static TEnum ToEnum<TEnum>(this string value, TEnum defaultValue)
            where TEnum : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            TEnum result;

            return Enum.TryParse<TEnum>(value, true, out result) ? result : defaultValue;
        }
    }
}
