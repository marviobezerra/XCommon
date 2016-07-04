using XCommon.Web.Authentication.Cookie;
using XCommon.Web.Authentication.Facebook;
using XCommon.Web.Authentication.Google;
using XCommon.Web.Authentication.Map;

namespace XCommon.Web.Authentication
{
    public class AuthenticationConfig
    {
        public AuthenticationCookieConfig Cookie { get; set; }

        public AuthenticationFacebookConfig Facebook { get; set; }

        public AuthenticationGoogleConfig Google { get; set; }

        public AuthenticationMapConfig Map { get; set; }
    }
}
