using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using XCommon.Application;
using XCommon.Application.Authentication;
using XCommon.Application.Authentication.Entity;
using XCommon.Patterns.Ioc;

namespace XCommon.Web.Authentication.Providers
{
	public abstract class BaseProvider
	{
		public BaseProvider(ProviderType provider)
		{
			Provider = provider;
		}

		private string RedirectKey => "XCommonRedirect";

		private ITicketManager TicketManager => Kernel.Resolve<ITicketManager>();

		private ILoginBusiness LoginBusiness => Kernel.Resolve<ILoginBusiness>();

		protected IApplicationSettings ApplicationSettings => Kernel.Resolve<IApplicationSettings>();

		protected AuthenticationConfig AuthenticationConfig => ApplicationSettings.Values.Get<AuthenticationConfig>(ApplicationBuilderExtension.Authentication);

		public ProviderType Provider { get; private set; }

		public abstract void SetUp(AuthenticationBuilder authentication);

		public abstract SignUpExternalEntity ParseExternalEntity(OAuthCreatingTicketContext ctx);

		public async Task ProcessFail(RemoteFailureContext ctx)
		{
			await Task.Factory.StartNew(() => {

				ctx.HandleResponse();
				var url = AuthenticationConfig.UriError + UrlEncoder.Default.Encode(ctx.Failure.Message);

				if (ctx.Failure.Message.StartsWith(RedirectKey) && AuthenticationConfig.AuthenticationType == AuthenticationType.Cookie)
				{
					url = AuthenticationConfig.UriCookieSucess;
				}

				if (ctx.Failure.Message.StartsWith(RedirectKey) && AuthenticationConfig.AuthenticationType == AuthenticationType.Token)
				{
					var split = ctx.Failure.Message.Split('|');
					url = AuthenticationConfig.UriTokenSucess + split.Last();
				}

				ctx.Response.Redirect(url);
			});
		}

		public async Task ProcessTicket(OAuthCreatingTicketContext ctx)
		{
			var signUpExternal = ParseExternalEntity(ctx);
			var ticket = await LoginBusiness.SignUpAsync(signUpExternal);

			if (ticket.HasErro || ticket.Entity.Status != TicketStatus.Sucess)
			{
				await ctx.HttpContext.SignOutAsync();
				ctx.Response.Redirect(AuthenticationConfig.UriError);
				return;
			}

			var tokenKey = await TicketManager.SignInAsync(ticket.Entity);
			throw new Exception($"{RedirectKey}|{tokenKey}");
		}
	}
}
