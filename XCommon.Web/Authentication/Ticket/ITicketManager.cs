using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using XCommon.Application.Login;

namespace XCommon.Web.Authentication.Ticket
{
    public interface ITicketManagerWeb : ITicketManager
    {
        Task<AuthenticationTicket> GetTicketAsync(TicketEntity signUpTicket);
    }
}
