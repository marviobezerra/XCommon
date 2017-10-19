using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCommon.Application.Executes;

namespace XCommon.Application.Mail.Implementations
{
    public class MailInMemory : IMail
    {
        public MailInMemory()
        {
            Mails = new List<MailInMemoryEntity>();
        }

        public List<MailInMemoryEntity> Mails { get; set; }
		
        public List<MailInMemoryEntity> GetMailsTo(string to)
        {
            return Mails
                .Where(c => c.To == to)
                .OrderBy(c => c.SendDate)
                .ToList();
        }

		public async Task<Execute> SendAsync(string from, string to, string subject, string body)
			=> await SendAsync(from, from, to, to, subject, body);

		public async Task<Execute> SendAsync(string from, string fromName, string to, string toName, string subject, string body)
		{
			var result = new Execute();
			Mails.Add(new MailInMemoryEntity(to, toName, from, fromName, subject, body));

			return await Task.FromResult(result);
		}
	}
}
