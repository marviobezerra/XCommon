namespace XCommon.Application.Authentication.Entity
{
    public class PasswordRecoveryEntity
    {
        public string Token { get; set; }

        public string NewPassword { get; set; }
    }
}
