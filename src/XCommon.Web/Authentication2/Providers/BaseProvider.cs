using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using XCommon.Application.Authentication;
using XCommon.Application.Authentication.Entity;
using XCommon.Patterns.Ioc;

namespace XCommon.Web.Authentication2.Providers
{
	public abstract class BaseProvider
	{
		public BaseProvider(ProviderType provider)
		{
			Provider = provider;
		}

		[Inject]
		private ILoginBusiness LoginBusiness { get; set; }

		public ProviderType Provider { get; private set; }

		public abstract void SetUp(AuthenticationBuilder authentication);

		public abstract SignUpExternalEntity ParseExternalEntity(OAuthCreatingTicketContext ctx);

		public async Task ProcessTicket(OAuthCreatingTicketContext ctx)
		{
			var signUp = ParseExternalEntity(ctx);
			var ticket = await LoginBusiness.SignUpAsync(signUp);

			if (ticket.HasErro || ticket.Entity.Status != TicketStatus.Sucess)
			{
				
				//ctx.Ticket = null;
				return;
			}

			//ctx.Ticket = await TicketManager.GetTicketAsync(ticket.Entity);
		}
	}
}
