using System;
using System.Threading.Tasks;
using XCommon.Application.Authentication.Entity;
using XCommon.Application.Executes;

namespace XCommon.Application.Authentication
{
	public interface ITicketManager
    {
		
        bool IsAuthenticated { get; }

        string Culture { get; }

        Guid UserKey { get; }

        ExecuteUser User { get; }

		Task<string> SignInAsync(TicketEntity signUpTicket);

        Task SignOutAsync();

		Task<TokenEntity> CheckTokenAsync(string key);
    }
}
