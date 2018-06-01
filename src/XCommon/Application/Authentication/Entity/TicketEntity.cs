using System;
using System.Collections.Generic;

namespace XCommon.Application.Authentication.Entity
{
	public class TicketEntity
    {
		public TicketEntity()
		{
			Roles = new List<string>();
		}

        public Guid Key { get; set; }

        public List<string> Roles { get; set; }

        public string Culture { get; set; }
    }
}
