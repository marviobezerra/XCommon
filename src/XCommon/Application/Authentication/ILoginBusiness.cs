using System;
using System.Threading.Tasks;
using XCommon.Application.Executes;
using XCommon.Application.Authentication.Entity;

namespace XCommon.Application.Authentication
{
    public interface ILoginBusiness
    {
        Task<Execute<TicketEntity>> SignInAsync(SignInEntity login);

        Task<Execute<TicketEntity>> SignUpAsync(SignUpEntity signUp);
        
        Task<Execute> ConfirmEmailAsync(Guid userKey);

        Task<Execute> ConfirmPhoneAsync(Guid userKey);

        Task<Execute> RecoveryPasswordRequestTokenAsync(string email);

        Task<Execute> RecoveryPasswordValidateTokenAsync(string token);

        Task<Execute> RecoveryPasswordAsync(PasswordRecoveryEntity info);

        Task<Execute> ChangePasswordAsync(PasswordChangeEntity info);
    }
}
