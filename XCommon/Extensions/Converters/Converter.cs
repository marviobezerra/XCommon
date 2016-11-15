using System;
using System.Collections.Generic;
using System.Linq;

namespace XCommon.Extensions.Converters
{
    public static class Converter
    {

        public static TEntity Convert<TEntity>(this object source, params string[] ignore)
            where TEntity : class, new()
        {
            if (source == null)
                return null;

            var result = new TEntity();
            
            var propertySource = source.GetType().GetProperties();
            var propertyResult = result.GetType().GetProperties();

            foreach (var item in propertyResult)
            {
                if (ignore.Length > 0 && ignore.Contains(item.Name))
                    continue;

                var find = propertySource.Where(c => c.Name == item.Name && c.PropertyType == item.PropertyType);

                if (!find.Any())
                    continue;

                var sourceItem = find.First();

                if (item.CanWrite && sourceItem.CanRead)
                    item.SetValue(result, sourceItem.GetValue(source, null), null);
            }

            return result;
        }

        public static List<TEntity> Convert<TEntity, TSource>(this IEnumerable<TSource> source, params string[] ignore)
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
    }
}
