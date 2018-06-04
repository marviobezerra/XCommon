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

		public Task<Execute> ConfirmEmailAsync(Guid userKey, string token)
		{
			throw new NotImplementedException();
		}

		public Task<Execute> ConfirmPhoneAsync(Guid userKey, string token)
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

		public Task<Execute> RecoveryPasswordValidateTokenAsync(Guid userKey, string token)
		{
			throw new NotImplementedException();
		}

		public Task<Execute<TicketEntity>> SignInAsync(SignInEntity signIn)
		{
			throw new NotImplementedException();
		}

		public Task<Execute<TicketEntity>> SignUpAsync(SignUpEntity signUp)
		{
			throw new NotImplementedException();
		}
	}
}
