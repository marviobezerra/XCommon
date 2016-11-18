using System;

namespace XCommon.Application.Mail.Implementations
{
    public class MailInMemoryEntity
    {
        public MailInMemoryEntity(string to, string replyTo, string subject, string body)
        {
            To = to;
            ReplyTo = replyTo;
            Subject = subject;
            Body = body;
            SendDate = DateTime.Now;
        }

        public string To { get; private set; }

        public string ReplyTo { get; private set; }

        public string Subject { get; private set; }

        public string Body { get; private set; }

        public DateTime SendDate { get; private set; }
    }
}
