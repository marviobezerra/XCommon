using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace XCommon.Extensions.Util
{
    public static class Reflaction
    {
        public static IEnumerable<PropertyInfo> GetProperties(this Type type)
        {
            var properties = type.GetTypeInfo().DeclaredProperties.ToList();
            var baseType = type.GetTypeInfo().BaseType;

            if (baseType != typeof(object))
            {
                var baseProperties = GetProperties(baseType);
                properties.AddRange(baseProperties);
            }

            return properties;
        }

        public static PropertyInfo GetProperty(this Type type, string name)
        {
            var propriedades = type.GetProperties();
            return propriedades.Where(c => c.Name == name).FirstOrDefault();
        }

        public static List<AtributoDetalhe<T>> GetAtributos<T>(this object objeto)
            where T : Attribute
        {
            var propriedades = objeto.GetType().GetProperties();

            var query = from p in propriedades
                        let attr = p.GetCustomAttributes(typeof(T), true)
                        where attr.Count() == 1
                        select new AtributoDetalhe<T>
                        {
                            Propriedade = p,
                            Atributo = (T)attr.FirstOrDefault()
                        };

            return query.ToList();
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            FieldInfo fi = value.GetType().GetRuntimeField(value.ToString());
            var attributes = (TAttribute[])fi.GetCustomAttributes(typeof(TAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0];
            }

            return null;
        }

        public static string GetAttributeDescription(this Enum value)
        {
            DescriptionAttribute description = GetAttribute<DescriptionAttribute>(value);

            return description == null
                ? string.Empty
                : description.Description;
        }
    }
}
