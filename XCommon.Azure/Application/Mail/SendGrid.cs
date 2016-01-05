using XCommon.Application.Mail;
using XCommon.Patterns.Repository.Executes;
using SendGrid;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace XCommon.Azure.Application.Mail
{
    public class SendGrid : IMail
    {
        private string UserName { get; set; }

        private string Password { get; set; }

        private string FromEmail { get; set; }

        private string FromName { get; set; }

        public SendGrid()
        {
            UserName = ConfigurationManager.AppSettings["XCommon:SendGrid:User"];
            Password = ConfigurationManager.AppSettings["XCommon:SendGrid:Password"];

            FromName = ConfigurationManager.AppSettings["XCommon:SendGrid:FromName"];
            FromEmail = ConfigurationManager.AppSettings["XCommon:SendGrid:FromEmail"];
        }

        public Execute Send(string to, string subject, string body)
        {
            return Send(to, FromEmail, subject, body);
        }

        public Execute Send(string to, string replyTo, string subject, string body)
        {
            Execute result = new Execute();

            try
            {
                SendGridMessage myMessage = new SendGridMessage
                {
                    From = new MailAddress(FromEmail, FromName, Encoding.UTF8),
                    To = new MailAddress[] { new MailAddress(to, "", Encoding.UTF8) },
                    ReplyTo = new MailAddress[] { new MailAddress(replyTo, FromName, Encoding.UTF8) },
                    Subject = subject,
                    Html = body
                };

                Web transportWeb = new Web(new NetworkCredential(UserName, Password));
                transportWeb.DeliverAsync(myMessage);
            }
            catch (Exception ex)
            {
                result.AddMessage(ex, "Erro ao enviar email");
            }

            return result;
        }
    }
}
