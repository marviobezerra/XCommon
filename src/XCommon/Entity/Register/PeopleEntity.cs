using System;
using System.Runtime.Serialization;
using XCommon.Entity.Register.Enumerators;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.Entity.Register
{
	public class PeopleEntity : EntityBase
	{
		public Guid IdPerson { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public GenderType Gender { get; set; }

		public DateTime? Birthday { get; set; }

		public string Culture { get; set; }

		public int TimeZone { get; set; }

		public string About { get; set; }

		public string ImageProfile { get; set; }

		public string ImageCover { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime ChangedAt { get; set; }


		[IgnoreDataMember]
		public override Guid Key
		{
			get
			{
				return IdPerson;
			}
			set
			{
				IdPerson = value;
			}
		}
	}
}
