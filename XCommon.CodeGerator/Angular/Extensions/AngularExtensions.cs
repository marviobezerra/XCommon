using System;
using System.Linq;

namespace XCommon.CodeGerator.Angular.Extensions
{
	public static class AngularExtensions
    {
		internal static bool GetOutLet(this string component)
		{
			return component.Contains("?o");
		}

		internal static string GetSelector(this string component)
		{
			var result = string.Empty;

			foreach (var item in component)
			{
				if (string.IsNullOrEmpty(result))
				{
					result += item;
					continue;
				}

				if (Char.IsUpper(item))
				{
					result += '-';
					result += item;
					continue;
				}

				result += item;
			}

			return result.ToLower();
		}

		internal static string GetName(this string name)
		{
			var result = name.First().ToString().ToUpper() + name.Substring(1);
			result = result.Replace("Service", string.Empty);
			result = result.Replace("Component", string.Empty);
			return result;

		}
	}
}
