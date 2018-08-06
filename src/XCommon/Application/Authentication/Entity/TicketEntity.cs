using System;
using System.Collections.Generic;

namespace XCommon.Application.Authentication.Entity
{
	public class TicketEntity
	{
		public TicketEntity()
		{
			Roles = new List<string>();
			Values = new Dictionary<string, string>();
		}

		public string Name { get; set; }

		public Guid Key { get; set; }

		public List<string> Roles { get; set; }

		public Dictionary<string, string> Values { get; set; }

		public string Culture { get; set; }
	}
}
