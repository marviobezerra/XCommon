using System.Collections.Generic;
using System.Linq;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Application.Mail.Implementations
{
    public class MailInMemory : IMail
    {
        public MailInMemory()
        {
            Mails = new List<MailInMemoryEntity>();
        }

        public List<MailInMemoryEntity> Mails { get; set; }

        public Execute Send(string to, string subject, string body)
            => Send(to, to, subject, body);

        public Execute Send(string to, string replyTo, string subject, string body)
        {
            Execute result = new Execute();

            Mails.Add(new MailInMemoryEntity(to, replyTo, subject, body));

            return result;
        }

        public List<MailInMemoryEntity> GetMailsTo(string to)
        {
            return Mails
                .Where(c => c.To == to)
                .OrderBy(c => c.SendDate)
                .ToList();
        }
    }
}
