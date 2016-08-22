using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using XCommon.Application.Login;
using XCommon.Patterns.Ioc;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Web.Controllers
{
    public abstract class AccountController : BaseController
    {
        protected ILoginBusiness LoginBusiness => Kernel.Resolve<ILoginBusiness>();

        [HttpPost("signin")]
        public virtual async Task<Execute<TicketStatus>> SignIn([FromBody] SignInEntity login)
        {
            return await LoginBusiness.SignInAsync(login);
        }

        [HttpPost("signup/external")]
        public virtual async Task<Execute<TicketEntity>> SignUp([FromBody] SignUpExternalEntity signUp)
        {
            return await LoginBusiness.SignUpAsync(signUp);
        }

        [HttpPost("signup")]
        public virtual async Task<Execute<TicketStatus>> SignUp([FromBody] SignUpInternalEntity signUp)
        {
            return await LoginBusiness.SignUpAsync(signUp);
        }
        
        [HttpGet("signout")]
        public virtual async Task<Execute<TicketStatus>> SignOut()
        {
            return await LoginBusiness.SignOutAsync(Ticket.UserKey);
        }

        [HttpPost("confirm/email")]
        public virtual async Task<Execute> ConfirmEmail(Guid userKey)
        {
            return await LoginBusiness.ConfirmEmailAsync(userKey);
        }

        [HttpPost("confirm/phone")]
        public virtual async Task<Execute> ConfirmPhone(Guid userKey)
        {
            return await LoginBusiness.ConfirmPhoneAsync(userKey);
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
        public virtual async Task<Execute> RecoveryPasswordValidateToken(string token)
        {
            return await LoginBusiness.RecoveryPasswordValidateTokenAsync(token);
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
