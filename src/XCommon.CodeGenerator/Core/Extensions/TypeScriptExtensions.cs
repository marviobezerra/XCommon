using System;
using System.Linq;

namespace XCommon.CodeGenerator.Core.Extensions
{
	public static class TypeScriptExtensions
    {
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

				if (char.IsUpper(item))
				{
					result += '-';
					result += item;
					continue;
				}

				result += item;
			}

			return result.ToLower();
		}
	}
}
