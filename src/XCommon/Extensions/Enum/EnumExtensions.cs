using System;
using System.ComponentModel;
using XCommon.Extensions.String;

namespace XCommon.Extensions.Enum
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

		public static bool Has<TEnum>(this System.Enum type, TEnum value) where TEnum : struct
		{
			try
			{
				return (((int)(object)type & (int)(object)value) == (int)(object)value);
			}
			catch
			{
				return false;
			}
		}

		public static TEnum Add<TEnum>(this System.Enum type, TEnum value) where TEnum : struct
		{
			try
			{
				return (TEnum)(object)(((int)(object)type | (int)(object)value));
			}
			catch (Exception ex)
			{
				throw new ArgumentException(string.Format("Could not append value from enumerated type '{0}'.", typeof(TEnum).Name), ex);
			}
		}

		public static TEnum Remove<TEnum>(this System.Enum type, TEnum value) where TEnum : struct
		{
			try
			{
				return (TEnum)(object)(((int)(object)type & ~(int)(object)value));
			}
			catch (Exception ex)
			{
				throw new ArgumentException(string.Format("Could not remove value from enumerated type '{0}'.", typeof(TEnum).Name), ex);
			}
		}
	}
}
