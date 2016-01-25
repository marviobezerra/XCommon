using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCommon.Test.Patterns.Specification.Helper
{
	public class PersonsEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public DateTime BirthDay { get; set; }

		public bool Male { get; set; }
	}

	public class PersonFilter
	{
		public int? Id { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public DateTime? BirthDay { get; set; }

		public bool? Male { get; set; }
	}
}
