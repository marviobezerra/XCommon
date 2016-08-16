using System;
using System.Threading.Tasks;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Application.Login
{
    public interface ITicketManager
    {
        bool IsAuthenticated { get; }
        Guid UserKey { get; }
        ExecuteUser User { get; }
        Task SignInAsync(TicketEntity signUpTicket);
        Task SignOutAsync();
    }
}
