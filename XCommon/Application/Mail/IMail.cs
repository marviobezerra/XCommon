using XCommon.Patterns.Repository.Executes;

namespace XCommon.Application.Mail
{
    public interface IMail
    {
        Execute Send(string to, string subject, string body);
        Execute Send(string to, string replyTo, string subject, string body);
    }
}
