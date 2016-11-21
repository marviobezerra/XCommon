using System;
using System.Threading.Tasks;
using XCommon.Application.Executes;
using XCommon.Application.Login.Entity;

namespace XCommon.Application.Login
{
    public interface ILoginBusiness
    {
        Task<Execute<TicketStatus>> SignInAsync(SignInEntity login);

        Task<Execute<TicketEntity>> SignUpAsync(SignUpExternalEntity signUp);

        Task<Execute<TicketStatus>> SignUpAsync(SignUpInternalEntity signUp);

        Task<Execute<TicketStatus>> SignOutAsync(Guid userKey);
        
        Task<Execute> ConfirmEmailAsync(Guid userKey);

        Task<Execute> ConfirmPhoneAsync(Guid userKey);

        Task<Execute> RecoveryPasswordRequestTokenAsync(string email);

        Task<Execute> RecoveryPasswordValidateTokenAsync(string token);

        Task<Execute> RecoveryPasswordAsync(PasswordRecoveryEntity info);

        Task<Execute> ChangePasswordAsync(PasswordChangeEntity info);
    }
}
