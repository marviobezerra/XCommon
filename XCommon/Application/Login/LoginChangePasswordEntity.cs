namespace XCommon.Application.Login
{
    public class LoginChangePasswordEntity
    {
        public object Key { get; set; }
        
        public string PasswordCurrent { get; set; }
        
        public string PasswordNew { get; set; }
        
        public string PasswordConfirm { get; set; }
    }
}
