using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.DependencyInjection;
using XCommon.Application.Authentication.Entity;
using XCommon.Extensions.String;

namespace XCommon.Web.Authentication.Providers
{
	public class GoogleProvider : BaseProvider
	{
		public GoogleProvider() : base(ProviderType.Google)
		{
		}

		public string ClientId { get; set; }

		public string ClientSecret { get; set; }

		public override SignUpExternalEntity ParseExternalEntity(OAuthCreatingTicketContext ctx)
		{
			var signUp = new SignUpExternalEntity
			{
				Provider = ProviderType.Google
			};

			var id = ctx.User.Value<string>("id");
			if (id.IsNotEmpty())
			{
				signUp.Token = id;
				signUp.Identifier = id;
			}

			var name = ctx.User.Value<string>("displayName");
			if (name.IsNotEmpty())
			{
				signUp.Name = name;
			}

			var email = ctx.User["emails"]?[0]?["value"]?.ToString();
			if (email.IsNotEmpty())
			{
				signUp.Email = email;
			}

			var gender = ctx.User.Value<string>("gender");
			if (gender.IsNotEmpty())
			{
				signUp.Male = gender.ToLower() == "male";
			}

			var urlProfile = ctx.User.Value<string>("url");
			if (urlProfile.IsNotEmpty())
			{
				signUp.UrlProfile = urlProfile;
			}

			var urlImage = ctx.User["image"]?["url"]?.ToString();
			if (urlImage.IsNotEmpty())
			{
				signUp.UrlImage = urlImage.Replace("?sz=50", string.Empty);
			}

			var urlCover = ctx.User["cover"]?["coverPhoto"]?["url"]?.ToString();
			if (urlCover.IsNotEmpty())
			{
				signUp.UrlCover = urlCover;
			}

			var language = ctx.User.Value<string>("language");
			if (language.IsNotEmpty())
			{
				signUp.Language = language;
			}

			return signUp;
		}

		public override void SetUp(AuthenticationBuilder authentication)
		{
			authentication.AddGoogle(c =>
			{
				c.ClientId = ClientId;
				c.ClientSecret = ClientSecret;
				c.SaveTokens = true;

				c.Events = new OAuthEvents
				{
					OnRemoteFailure = ctx =>
					{
						return ProcessFail(ctx);
					},
					OnCreatingTicket = async ctx =>
					{
						await ProcessTicket(ctx);
						ctx.Success();
					}
				};
			});
		}
	}
}
