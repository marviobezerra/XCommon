using XCommon.Web.Authentication.Base;

namespace XCommon.Web.Authentication.Map
{
    public class AuthenticationMapConfig : AuthenticationConfigBase
    {
        public AuthenticationMapConfig() 
            : base(AuthenticationType.Map)
        {

        }

        public string UriLogin { get; set; }

        public string UriRedirect { get; set; }

        public string UriParam { get; set; }
    }
}
