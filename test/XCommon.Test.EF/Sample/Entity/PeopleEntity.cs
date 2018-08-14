using System;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.Test.EF.Sample.Entity
{
	public class PeopleEntity : EntityBase
	{
		public Guid IdPerson { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }
		public override Guid Key
		{
			get => IdPerson;
			set => IdPerson = value;
		}
	}
}
