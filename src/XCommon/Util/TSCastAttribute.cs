using System;

namespace XCommon.Util
{
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public class TSCastAttribute : Attribute
	{
		public TSCastAttribute(string type)
		{
			Type = type;
		}

		public string Type { get; set; }
	}
}
