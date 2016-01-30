using System;

namespace XCommon.Util
{
	[System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public sealed class IgnoreAttribute : Attribute
	{
		public IgnoreAttribute()
		{
		}
	}
}
