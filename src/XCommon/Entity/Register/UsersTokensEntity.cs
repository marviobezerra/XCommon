using System;
using System.Runtime.Serialization;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.Entity.Register
{
	public class UsersTokensEntity : EntityBase
	{
		public Guid IdUserToken { get; set; }

		public Guid IdUser { get; set; }

		public string Token { get; set; }

		public string TokenType { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime ValidUntil { get; set; }

		[IgnoreDataMember]
		public override Guid Key
		{
			get
			{
				return IdUserToken;
			}
			set
			{
				IdUserToken = value;
			}
		}
	}
}
