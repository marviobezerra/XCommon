using System;

namespace XCommon.Application.Mail.Implementations
{
    public class MailInMemoryEntity
    {
        public MailInMemoryEntity(string from, string fromName, string to, string toName, string subject, string body)
        {
            To = to;
            ToName = ToName;
			From = from;
			FromName = fromName;
            Subject = subject;
            Body = body;
            SendDate = DateTime.Now;
        }

        public string To { get; private set; }

		public string ToName { get; private set; }

		public string From { get; private set; }

		public string FromName { get; private set; }

        public string Subject { get; private set; }

        public string Body { get; private set; }

        public DateTime SendDate { get; private set; }
    }
}
