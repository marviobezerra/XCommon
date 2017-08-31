using System;
using System.Threading.Tasks;
using XCommon.Application.Authentication;
using XCommon.Application.Authentication.Entity;
using XCommon.Application.Executes;
using XCommon.Extensions.Converters;

namespace XCommon.Test.Web.Site.Code
{
	public class LoginBusiness : ILoginBusiness
	{
		public Task<Execute> ChangePasswordAsync(PasswordChangeEntity info)
		{
			throw new NotImplementedException();
		}

		public Task<Execute> ConfirmEmailAsync(Guid userKey)
		{
			throw new NotImplementedException();
		}

		public Task<Execute> ConfirmPhoneAsync(Guid userKey)
		{
			throw new NotImplementedException();
		}

		public Task<Execute> RecoveryPasswordAsync(PasswordRecoveryEntity info)
		{
			throw new NotImplementedException();
		}

		public Task<Execute> RecoveryPasswordRequestTokenAsync(string email)
		{
			throw new NotImplementedException();
		}

		public Task<Execute> RecoveryPasswordValidateTokenAsync(string token)
		{
			throw new NotImplementedException();
		}

		public Task<Execute<TicketStatus>> SignInAsync(SignInEntity login)
		{
			throw new NotImplementedException();
		}

		public Task<Execute<TicketStatus>> SignOutAsync(Guid userKey)
		{
			throw new NotImplementedException();
		}

		public Task<Execute<TicketEntity>> SignUpAsync(SignUpExternalEntity signUp)
		{
			var result = new Execute<TicketEntity>(new TicketEntity
			{
				Culture= "en-US",
				Key = "123".ToGuid(),
				Name = "Márvio André",
				Status = TicketStatus.Sucess
			});

			return Task.FromResult(result);
		}

		public Task<Execute<TicketStatus>> SignUpAsync(SignUpInternalEntity signUp)
		{
			throw new NotImplementedException();
		}
	}
}
