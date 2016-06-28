using System;
using System.Threading.Tasks;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Application.Login
{
	public interface ILoginBusiness
    {
        Task<Execute<SingUpEntity>> ValidaUserAsync(Guid key);
        Task<Execute<SingUpEntity>> ValidaUserAsync(SignInEntity login);
        Task<Execute<SingUpEntity>> ValidaUserNewAsync(SingUpEntity login);
        Task<Execute> ConfirmeEmailAsync(Guid userKey);
        Task<Execute> ConfirmePhoneAsync(Guid userKey);
        Task<Execute> ChangePasswordRequestTokenAsync(string email);
        Task<Execute> ChangePasswordValidateTokenAsync(string token);
        Task<Execute> ChangePasswordAsync(string token, string newPassword);
        Task<Execute> ChangePasswordAsync(LoginChangePasswordEntity info);
        Task<bool> EmailExistsAsync(string email);
        Task LogoutAsync(Guid userKey);
        Task LogoutAsync(string sessionId);
        Task LoginAsync(Guid userKey, string sessionId);
        string NewToken();
    }
}
