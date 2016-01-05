using XCommon.Patterns.Repository.Executes;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace XCommon.Application.Mail.Implementations
{
    public class MailGmail : IMail
    {
        public Execute Send(string to, string subject, string body)
        {
            return Send(to, to, subject, body);
        }

        public Execute Send(string to, string replyTo, string subject, string body)
        {
            Execute result = new Execute();

            string mail = ConfigurationManager.AppSettings["Prospect:Mail"].ToLower();
            string mailUser = ConfigurationManager.AppSettings["Prospect:MailUser"].ToLower();
            string mailPassword = ConfigurationManager.AppSettings["Prospect:MailPassword"].ToLower();

            try
            {
                Task.Run(() =>
                {
                    using (SmtpClient client = new SmtpClient()
                    {
                        Port = 587,
                        Host = "smtp.gmail.com",
                        EnableSsl = true,
                        Timeout = 10000,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(mailUser, mailPassword)
                    })
                    {

                        using (MailMessage mailMessage = new MailMessage(mail, to, subject, body)
                        {
                            IsBodyHtml = true,
                            BodyEncoding = UTF8Encoding.UTF8,
                            DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
                        })
                        {
                            mailMessage.ReplyToList.Add(replyTo);
                            client.Send(mailMessage);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                result.AddMessage(ex, "Error on send e-mail");
            }

            return result;
        }
    }
}
