using System;
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

		string WriteToken(TicketEntity signUpTicket);

		string GetVaue(string key);

		string GetHost();
	}
}
