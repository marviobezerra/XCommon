using System;
using System.Threading.Tasks;

namespace XCommon.Application.Login
{
    public interface ITicketManager
    {
        bool IsAuthenticated { get; }
        Guid UserKey { get; }
        Task SignInAsync(TicketEntity signUpTicket);
        Task SignOutAsync();
    }
}
