using XCommon.Web.Authentication.Base;

namespace XCommon.Web.Authentication.Cookie
{
    public class AuthenticationCookieConfig : AuthenticationConfigBase
    {
        public AuthenticationCookieConfig() 
            : base(AuthenticationType.Cookie)
        {
        }

        public string CookieName { get; set; }
    }
}
