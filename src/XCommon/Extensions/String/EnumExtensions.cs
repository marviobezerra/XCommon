using System;
using System.ComponentModel;

namespace XCommon.Extensions.String
{
	public static class EnumExtensions
	{
		public static string GetEnumDescription<TEnum>(this TEnum enumerationValue) where TEnum : struct
		{
			var type = enumerationValue.GetType();

			if (!type.IsEnum)
			{
				throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
			}

			var memberInfo = type.GetMember(enumerationValue.ToString());
			if (memberInfo != null && memberInfo.Length > 0)
			{
				var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

				if (attrs != null && attrs.Length > 0)
				{
					return ((DescriptionAttribute)attrs[0]).Description;
				}
			}

			var result = enumerationValue.ToString();

			return result.IsEmpty()
				? enumerationValue.ToString()
				: result;
		}
	}
}
