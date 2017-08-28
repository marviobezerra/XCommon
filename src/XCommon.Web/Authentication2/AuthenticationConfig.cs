using XCommon.Application.Authentication.Entity;
using XCommon.Web.Authentication2.Providers;

namespace XCommon.Web.Authentication2
{
	public class AuthenticationConfig
	{
		public string UriLogin { get; set; }

		public int Expiration { get; set; }

		public string SecurityKey { get; set; }

		public AuthenticationType AuthenticationType { get; set; }

		public LocalProvider Local { get; set; }

		public FacebookProvider Facebook { get; set; }

		public GoogleProvider Google { get; set; }
	}
}
