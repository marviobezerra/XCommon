using XCommon.Web.Authentication.Base;

namespace XCommon.Web.Authentication.Facebook
{
    public class AuthenticationFacebookConfig : AuthenticationConfigBase
    {
        public AuthenticationFacebookConfig() 
            : base(AuthenticationType.Facebook)
        {
        }

        public string AppId { get; internal set; }

        public string AppSecret { get; internal set; }
    }
}
