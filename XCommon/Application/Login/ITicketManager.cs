using System;
using System.Threading.Tasks;
using XCommon.Application.Login.Entity;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Application.Login
{
    public interface ITicketManager
    {
        bool IsAuthenticated { get; }
        string Culture { get; }
        Guid UserKey { get; }
        ExecuteUser User { get; }
        Task SignInAsync(TicketEntity signUpTicket);
        Task SignOutAsync();
    }
}
