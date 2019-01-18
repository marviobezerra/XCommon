using System;

namespace XCommon.EF.Application.Context.Register
{
	public class UsersTokens
    {
		public Guid IdUserToken { get; set; }

		public Guid IdUser { get; set; }

		public string Token { get; set; }

		public string TokenType { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime ValidUntil { get; set; }

		public virtual Users Users { get; set; }
	}
}
