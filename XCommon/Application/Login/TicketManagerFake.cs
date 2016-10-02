using System;
using System.Threading.Tasks;
using XCommon.Extensions.Converters;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Application.Login
{
	public class TicketManagerFake : ITicketManager
	{
		public string Culture { get; private set; }

		public bool IsAuthenticated { get; private set; }

		public ExecuteUser User { get; set; }

		public Guid UserKey { get; set; }

		public async Task SignInAsync(TicketEntity signUpTicket)
		{
            if (signUpTicket.Status == TicketStatus.Sucess)
            {
                IsAuthenticated = true;
                Culture = signUpTicket.Culture;
                UserKey = signUpTicket.Key;
                User = new ExecuteUser
                {
                    Key = signUpTicket.Key,
                    Name = signUpTicket.Name,
                    Login = "Fake"
                };

                return;
            }

            await SignOutAsync();
		}

		public async Task SignOutAsync()
		{
            IsAuthenticated = false;
            Culture = string.Empty;
            UserKey = Guid.Empty;
            User = null;

            await Task.Yield();
        }
	}
}
