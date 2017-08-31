using XCommon.Application.Authentication.Entity;
using XCommon.Web.Authentication.Providers;

namespace XCommon.Web.Authentication
{
	public class AuthenticationConfig
	{
		public AuthenticationConfig()
		{
			UriApplicationBase = "";
			UriLogin = "/login";
			UriError = "/error?msg=";
			UriTokenSucess = "/token/";
		}

		public string UriLogin { get; set; }

		public string UriError { get; set; }

		public string UriApplicationBase { get; set; }

		public string UriTokenSucess { get; set; }

		public string UriCookieSucess { get; set; }

		public int Expiration { get; set; }

		public string SecurityKey { get; set; }

		public AuthenticationType AuthenticationType { get; set; }

		public LocalProvider Local { get; set; }

		public FacebookProvider Facebook { get; set; }

		public GoogleProvider Google { get; set; }
	}
}
