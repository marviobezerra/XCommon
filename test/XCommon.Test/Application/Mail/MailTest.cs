using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using XCommon.Application.Mail;
using XCommon.Application.Mail.Implementations;
using Xunit;

namespace XCommon.Test.Application.Mail
{
	public class MailTest
    {
        [Fact(DisplayName = "Send")]
		[Trait("Application", "Mail")]
		public async Task Send()
        {
            var mail = new MailInMemory();
            var to = "marvio.bezerra@gmail.com";
            var subject = "Simple mail";
            var body = "Just testing code";

            await mail.SendAsync(to, to, subject, body);

            var sentMails = (mail as MailInMemory).GetMailsTo(to);
            var sentMail = sentMails.FirstOrDefault();

            sentMails.Count.Should().Be(1, "Was sent one mail");

            sentMail.Should().NotBeNull("There is a mail sent");
            sentMail.To.Should().Be(to);
            sentMail.From.Should().Be(to);
            sentMail.Subject.Should().Be(subject);
            sentMail.Body.Should().Be(body);
        }

        [Fact(DisplayName = "Send (ReplyTo)")]
		[Trait("Application", "Mail")]
		public async Task SendReplyTo()
        {
            IMail mail = new MailInMemory();
            var to = "marvio.bezerra@gmail.com";
            var from = "maria@gmail.com";
            var subject = "Simple mail";
            var body = "Just testing code";

            await mail.SendAsync(to, from, subject, body);

            var sentMails = (mail as MailInMemory).GetMailsTo(to);
            var sentMail = sentMails.FirstOrDefault();

            sentMails.Count.Should().Be(1, "Was sent one mail");

            sentMail.Should().NotBeNull("There is a mail sent");
            sentMail.To.Should().Be(to);
            sentMail.From.Should().Be(from);
            sentMail.Subject.Should().Be(subject);
            sentMail.Body.Should().Be(body);
        }

        [Fact(DisplayName = "Send (Many)")]
		[Trait("Application", "Mail")]
		public async Task SendMany()
        {
            IMail mail = new MailInMemory();
            var toMarvio = "marvio.bezerra@gmail.com";
            var toMaria = "maria@gmail.com";

            for (var i = 0; i < 3; i++)
            {
                await mail.SendAsync(toMarvio, toMaria, $"Mail {i}", $"Interact {i}");
            }

            for (var i = 0; i < 5; i++)
            {
                await mail.SendAsync(toMaria, toMarvio, $"Mail {i}", $"Interact {i}");
            }

            var sentMailsMarvio = (mail as MailInMemory).GetMailsTo(toMarvio);
            var sentMailsMaria = (mail as MailInMemory).GetMailsTo(toMaria);

            sentMailsMarvio.Count.Should().Be(3, "It were sent 3 mails to Marvio");
            sentMailsMaria.Count.Should().Be(5, "It were sent 5 mails to Maria");

        }
    }
}