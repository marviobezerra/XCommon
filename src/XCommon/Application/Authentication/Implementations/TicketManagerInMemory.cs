using System;
using System.Threading.Tasks;
using XCommon.Application.Authentication.Entity;
using XCommon.Application.Executes;

namespace XCommon.Application.Authentication.Implementations
{
	public class TicketManagerInMemory : ITicketManager
	{
        public TicketManagerInMemory(string cultureDefault)
        {
            Culture = cultureDefault;
            CultureDefault = cultureDefault;
        }

        private string CultureDefault { get; set; }

        public string Culture { get; private set; }

		public bool IsAuthenticated { get; private set; }

		public ExecuteUser User { get; set; }

		public Guid UserKey { get; set; }

		public async Task<string> SignInAsync(TicketEntity signUpTicket)
		{
            if (signUpTicket != null && signUpTicket.Status == TicketStatus.Sucess)
            {
                IsAuthenticated = true;
                Culture = signUpTicket.Culture;
                UserKey = signUpTicket.Key;
                User = new ExecuteUser
                {
                    Key = signUpTicket.Key,
                    Name = signUpTicket.Name,
                    Culture = "Fake"
                };

                return string.Empty;
            }

            await SignOutAsync();
			return string.Empty;

		}

		public async Task SignOutAsync()
		{
            IsAuthenticated = false;
            Culture = CultureDefault;
            UserKey = Guid.Empty;
            User = null;

            await Task.Yield();
        }

		public Task<TokenEntity> CheckTokenAsync(string key)
		{
			throw new NotImplementedException();
		}
	}
}
