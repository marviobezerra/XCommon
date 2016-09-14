using System;
using XCommon.Extensions.String;

namespace XCommon.CodeGeratorV2.Angular.Extensions
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
				if (result.IsEmpty())
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
	}
}
