using XCommon.Web.Authentication.Base;

namespace XCommon.Web.Authentication.Google
{
    public class AuthenticationGoogleConfig : AuthenticationConfigBase
    {
        public AuthenticationGoogleConfig() 
            : base(AuthenticationType.Google)
        {
        }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }
}
