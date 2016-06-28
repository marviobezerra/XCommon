namespace XCommon.Application.Login
{
    public class SignInEntity
    {
        public string User { get; set; }
        
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
