using System;
using System.Runtime.Serialization;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.Entity.System
{
	public class ConfigEntity : EntityBase
	{
		public Guid IdConfig { get; set; }

		public string Section { get; set; }

		public string ConfigKey { get; set; }

		public string Value { get; set; }

		[IgnoreDataMember]
		public override Guid Key
		{
			get
			{
				return IdConfig;
			}
			set
			{
				IdConfig = value;
			}
		}
	}
}
