using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XCommon.Application.Authentication;
using XCommon.Application.Authentication.Entity;
using XCommon.Application.Executes;
using XCommon.Patterns.Ioc;

namespace XCommon.Web.Controllers
{
	public abstract class AccountController : BaseController
	{
		[Inject]
		protected ILoginBusiness LoginBusiness { get; set; }

		[Inject]
		private ITicketManager TicketManager { get; set; }

		[HttpPost("signin")]
		public virtual async Task<Execute<string>> SignIn([FromBody] SignInEntity signInEntity)
		{
			var signIn = await LoginBusiness.SignInAsync(signInEntity);
			var result = new Execute<string>(signIn);

			if (!result.HasErro)
			{
				result.Entity = TicketManager.WriteToken(signIn.Entity);
			}

			return result;
		}

		[HttpPost("signup")]
		public virtual async Task<Execute<string>> SignUp([FromBody] SignUpEntity signUpEntity)
		{
			var signUp = await LoginBusiness.SignUpAsync(signUpEntity);
			var result = new Execute<string>(signUp);

			if (!result.HasErro)
			{
				result.Entity = TicketManager.WriteToken(signUp.Entity);
			}

			return result;
		}
		
		[HttpPost("confirm/email")]
		public virtual async Task<Execute> ConfirmEmail(Guid userKey, string token)
		{
			return await LoginBusiness.ConfirmEmailAsync(userKey, token);
		}

		[HttpPost("confirm/phone")]
		public virtual async Task<Execute> ConfirmPhone(Guid userKey, string token)
		{
			return await LoginBusiness.ConfirmPhoneAsync(userKey, token);
		}

		[HttpPost("recoverypassword")]
		public virtual async Task<Execute> RecoveryPassword([FromBody] PasswordRecoveryEntity info)
		{
			return await LoginBusiness.RecoveryPasswordAsync(info);
		}

		[HttpPost("recoverypassword/requesttoken")]
		public virtual async Task<Execute> RecoveryPasswordRequestToken(string email)
		{
			return await LoginBusiness.RecoveryPasswordRequestTokenAsync(email);
		}

		[HttpPost("recoverypassword/validatetoken")]
		public virtual async Task<Execute> RecoveryPasswordValidateToken(Guid userKey, string token)
		{
			return await LoginBusiness.RecoveryPasswordValidateTokenAsync(userKey, token);
		}

		[Authorize]
		[HttpPost("changepassword")]
		public virtual async Task<Execute> ChangePassword([FromBody] PasswordChangeEntity info)
		{
			info.Key = Ticket.UserKey;
			return await LoginBusiness.ChangePasswordAsync(info);
		}
	}
}
