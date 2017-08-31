using System;

namespace XCommon.Application.Authentication.Entity
{
	public class TokenEntity
    {
		public string Key { get; set; }

		public string Token { get; set; }

		public string Name { get; set; }

		public string Culture { get; set; }

		public DateTime	Expire { get; set; }
	}
}
