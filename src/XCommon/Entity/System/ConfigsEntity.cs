using System;
using System.Runtime.Serialization;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.Entity.System
{
	public class ConfigsEntity : EntityBase
	{
		public Guid IdConfig { get; set; }

		public string Module { get; set; }

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
