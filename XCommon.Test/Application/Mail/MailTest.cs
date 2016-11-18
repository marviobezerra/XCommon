using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCommon.Application.Mail;
using XCommon.Application.Mail.Implementations;
using Xunit;
using FluentAssertions;

namespace XCommon.Test.Application.Mail
{
    public class MailTest
    {
        [Fact(DisplayName = "Send")]
        public void Send()
        {
            IMail mail = new MailInMemory();
            string to = "marvio.bezerra@gmail.com";
            string subject = "Simple mail";
            string body = "Just testing code";

            mail.Send(to, subject, body);

            var sentMails = (mail as MailInMemory).GetMailsTo(to);
            var sentMail = sentMails.FirstOrDefault();

            sentMails.Count.Should().Be(1, "Was sent one mail");

            sentMail.Should().NotBeNull("There is a mail sent");
            sentMail.To.Should().Be(to);
            sentMail.ReplyTo.Should().Be(to);
            sentMail.Subject.Should().Be(subject);
            sentMail.Body.Should().Be(body);
        }

        [Fact(DisplayName = "Send (ReplyTo)")]
        public void SendReplyTo()
        {
            IMail mail = new MailInMemory();
            string to = "marvio.bezerra@gmail.com";
            string replyTo = "maria@gmail.com";
            string subject = "Simple mail";
            string body = "Just testing code";

            mail.Send(to, replyTo, subject, body);

            var sentMails = (mail as MailInMemory).GetMailsTo(to);
            var sentMail = sentMails.FirstOrDefault();

            sentMails.Count.Should().Be(1, "Was sent one mail");

            sentMail.Should().NotBeNull("There is a mail sent");
            sentMail.To.Should().Be(to);
            sentMail.ReplyTo.Should().Be(replyTo);
            sentMail.Subject.Should().Be(subject);
            sentMail.Body.Should().Be(body);
        }

        [Fact(DisplayName = "Send (Many)")]
        public void SendMany()
        {
            IMail mail = new MailInMemory();
            string toMarvio = "marvio.bezerra@gmail.com";
            string toMaria = "maria@gmail.com";

            for (int i = 0; i < 3; i++)
            {
                mail.Send(toMarvio, $"Mail {i}", $"Interact {i}");
            }

            for (int i = 0; i < 5; i++)
            {
                mail.Send(toMaria, $"Mail {i}", $"Interact {i}");
            }


            var sentMailsMarvio = (mail as MailInMemory).GetMailsTo(toMarvio);
            var sentMailsMaria = (mail as MailInMemory).GetMailsTo(toMaria);

            sentMailsMarvio.Count.Should().Be(3, "It were sent 3 mails to Marvio");
            sentMailsMaria.Count.Should().Be(5, "It were sent 5 mails to Maria");

        }
    }
}