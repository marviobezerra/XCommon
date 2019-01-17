using System;
using XCommon.Entity.Register.Enumerators;

namespace XCommon.EF.Application.Context.Register
{
	public class People
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

		public virtual Users Users { get; set; }
	}
}
