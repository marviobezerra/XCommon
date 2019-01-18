using System;

namespace XCommon.EF.Application.Context.System
{
	public class Config
	{
		public Guid IdConfig { get; set; }

		public string Section { get; set; }

		public string ConfigKey { get; set; }

		public string Value { get; set; }
	}
}
