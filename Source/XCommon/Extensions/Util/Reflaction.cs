using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XCommon.Util;

namespace XCommon.Extensions.Util
{
    public static class Reflaction
    {
        private static Dictionary<Type, string> PrimitiveTypes { get; set; }

        static Reflaction()
        {
            PrimitiveTypes = new Dictionary<Type, string>();
            
            PrimitiveTypes.Add(typeof(object), "object");
            PrimitiveTypes.Add(typeof(string), "string");
            PrimitiveTypes.Add(typeof(bool), "bool");
            PrimitiveTypes.Add(typeof(byte), "byte");
            PrimitiveTypes.Add(typeof(char), "char");
            PrimitiveTypes.Add(typeof(decimal), "decimal");
            PrimitiveTypes.Add(typeof(double), "double");
            PrimitiveTypes.Add(typeof(short), "short");
            PrimitiveTypes.Add(typeof(int), "int");
            PrimitiveTypes.Add(typeof(long), "long");
            PrimitiveTypes.Add(typeof(sbyte), "sbyte");
            PrimitiveTypes.Add(typeof(float), "float");
            PrimitiveTypes.Add(typeof(ushort), "ushort");
            PrimitiveTypes.Add(typeof(uint), "uint");
            PrimitiveTypes.Add(typeof(ulong), "ulong");
        }

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

        public static string GetClassName(this Type type, bool usePrimitiveTypes = true)
        {
            string result = usePrimitiveTypes && PrimitiveTypes.ContainsKey(type)
                ? PrimitiveTypes[type]
                : type.Name;

            if (!type.GetTypeInfo().IsGenericType)
                return result;

            result = result.Substring(0, result.IndexOf('`'));
            result += "<";
            result += string.Join(", ", type.GetTypeInfo().GetGenericArguments().Select(t => t.GetClassName(usePrimitiveTypes)));
            result += ">";

            return result;
        }

        public static bool IsBasedIn(this Type concret, Type contract)
        {
            return concret.GetTypeInfo().IsSubclassOf(contract)
                || concret.GetTypeInfo().GetInterfaces().Contains(contract)
                || concret == contract;
        }
    }
}
