namespace XCommon.Application.Authentication.Entity
{
    public class SignInEntity
    {
        public string User { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
