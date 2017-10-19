using System.Threading.Tasks;
using XCommon.Application.Executes;

namespace XCommon.Application.Mail
{
    public interface IMail
    {
        Task<Execute> SendAsync(string from, string to, string subject, string body);
		Task<Execute> SendAsync(string from, string fromName, string to, string toName, string subject, string body);
    }
}
