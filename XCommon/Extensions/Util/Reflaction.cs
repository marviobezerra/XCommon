using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using XCommon.Util;

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

        public static List<AttributeDetail<TAttribute>> GetAttributes<TAttribute>(this object objeto)
            where TAttribute : Attribute
        {
            var propriedades = objeto.GetType().GetProperties();

            var query = from p in propriedades
                        let attr = p.GetCustomAttributes(typeof(TAttribute), true)
                        where attr.Count() == 1
                        select new AttributeDetail<TAttribute>
                        {
                            Property = p,
                            Attribute = (TAttribute)attr.FirstOrDefault()
                        };

            return query.ToList();
        }

        public static string GetClassName(this Type type)
        {
            string result = type.Name;

            if (!type.GetTypeInfo().IsGenericType)
                return result;

            result = result.Substring(0, result.IndexOf('`'));
            result += "<";
            result += string.Join(", ", type.GetTypeInfo().GetGenericArguments().Select(t => t.GetClassName()));
            result += ">";

            return result;
        }

        public static bool IsTypeBased(this Type concret, Type contract)
        {
            return concret.GetTypeInfo().IsSubclassOf(contract)
                || concret.GetTypeInfo().IsAssignableFrom(contract)
                || concret.GetTypeInfo().GetInterfaces().Contains(contract)
                || concret == contract;
        }
    }
}
