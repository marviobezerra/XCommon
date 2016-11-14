using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using XCommon.Application.Login;
using XCommon.Application.Login.Entity;

namespace XCommon.Web.Authentication.Ticket
{
    public interface ITicketManagerWeb : ITicketManager
    {
        Task<AuthenticationTicket> GetTicketAsync(TicketEntity signUpTicket);

        bool SetCookieCulture(string culture);
    }
}
