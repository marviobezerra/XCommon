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
			PrimitiveTypes = new Dictionary<Type, string>
			{
				{ typeof(object), "object" },
				{ typeof(string), "string" },
				{ typeof(bool), "bool" },
				{ typeof(byte), "byte" },
				{ typeof(char), "char" },
				{ typeof(decimal), "decimal" },
				{ typeof(double), "double" },
				{ typeof(short), "short" },
				{ typeof(int), "int" },
				{ typeof(long), "long" },
				{ typeof(sbyte), "sbyte" },
				{ typeof(float), "float" },
				{ typeof(ushort), "ushort" },
				{ typeof(uint), "uint" },
				{ typeof(ulong), "ulong" }
			};
		}

		public static IEnumerable<PropertyInfo> GetProperties(this Type type, bool allProperties)
		{
			var properties = type.GetTypeInfo().DeclaredProperties.ToList();
			var baseType = type.GetTypeInfo().BaseType;

			if (baseType != typeof(object))
			{
				var baseProperties = GetProperties(baseType, allProperties);
				properties.AddRange(baseProperties);
			}

			return properties;
		}

		public static List<AttributeDetail<TAttribute>> GetAttributes<TAttribute>(this object objeto)
			where TAttribute : Attribute
		{
			var propriedades = objeto.GetType().GetProperties(true);

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
			var result = usePrimitiveTypes && PrimitiveTypes.ContainsKey(type)
				? PrimitiveTypes[type]
				: type.Name;

			if (!type.GetTypeInfo().IsGenericType)
			{
				return result;
			}

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
