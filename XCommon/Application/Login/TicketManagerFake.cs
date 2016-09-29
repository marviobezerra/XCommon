using System;
using System.Threading.Tasks;
using XCommon.Extensions.Converters;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Application.Login
{
	public class TicketManagerFake : ITicketManager
	{
		public string Culture { get; set; }

		public bool IsAuthenticated { get; set; }

		public ExecuteUser User
		{
			get
			{
				var userKey = UserKey;

				if (userKey == Guid.Empty)
					return null;

				return new ExecuteUser
				{
					Key = userKey,
					Name = "Fake"
				};
			}
		}

		public Guid UserKey
		{
			get
			{
				Guid result = Guid.Empty;

				if (!IsAuthenticated)
					return result;

				return 1.ToGuid();
			}
		}

		public async Task SignInAsync(TicketEntity signUpTicket)
		{
			await Task.Yield();
		}

		public async Task SignOutAsync()
		{
			await Task.Yield();
		}
	}
}
