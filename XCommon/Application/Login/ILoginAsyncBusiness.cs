using XCommon.Patterns.Repository.Executes;
using System;
using System.Threading.Tasks;

namespace XCommon.Application.Login
{
    public interface ILoginAsyncBusiness
    {
        Task<Execute<LoginPersonEntity>> ValidaUserAsync(object key);
        Task<Execute<LoginPersonEntity>> ValidaUserAsync(LoginEntity login);
        Task<Execute<LoginPersonEntity>> ValidaUserNewAsync(LoginPersonEntity login);
        Task<Execute> ConfirmeEmailAsync(object userKey);
        Task<Execute> ConfirmePhoneAsync(object userKey);
        Task<Execute> ChangePasswordRequestTokenAsync(string email);
        Task<Execute> ChangePasswordValidateTokenAsync(string token);
        Task<Execute> ChangePasswordAsync(string token, string newPassword);
        Task<Execute> ChangePasswordAsync(LoginChangePasswordEntity info);
        Task<bool> EmailExistsAsync(string email);
        Task LogoutAsync(object userKey);
        Task LogoutAsync(string sessionId);
        Task LoginAsync(object userKey, string sessionId);
        string NewToken();
    }
}
