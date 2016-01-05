using XCommon.Patterns.Repository.Executes;
using System;

namespace XCommon.Application.Login
{
    public interface ILoginBusiness
    {
        Execute<LoginPersonEntity> ValidaUser(object key);
        Execute<LoginPersonEntity> ValidaUser(LoginEntity login);
        Execute<LoginPersonEntity> ValidaUserNew(LoginPersonEntity login);
        Execute ConfirmeEmail(object userKey);
        Execute ConfirmePhone(object userKey);
        Execute ChangePasswordRequestToken(string email);
        Execute ChangePasswordValidateToken(string token);
        Execute ChangePassword(string token, string newPassword);
        Execute ChangePassword(LoginChangePasswordEntity info);
        bool EmailExists(string email);
        void Logout(object userKey);
        void Logout(string sessionId);
        void Login(object userKey, string sessionId);
        string NewToken();
    }
}
