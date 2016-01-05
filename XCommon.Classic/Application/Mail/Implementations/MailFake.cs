using XCommon.Patterns.Repository.Executes;
using System;
using System.IO;

namespace XCommon.Application.Mail.Implementations
{
    public class MailFake : IMail
    {
        public MailFake(string basePath)
        {
            BasePath = basePath;

            if (!Directory.Exists(BasePath))
                Directory.CreateDirectory(BasePath);
        }

        private string BasePath { get; set; }

        public Execute Send(string to, string subject, string body)
        {
            return Send(to, to, subject, body);
        }


        public Execute Send(string to, string replyTo, string subject, string body)
        {
            Execute result = new Execute();

            try
            {
                to = to.Replace("@", "-");
                var fileName = Path.Combine(BasePath, String.Format("{0}-{1}.txt", to, subject.Replace(" ", "-")));

                if (File.Exists(fileName))
                    File.Delete(fileName);

                File.WriteAllText(fileName, string.Format("{0}{1}{2}", body, Environment.NewLine, DateTime.Now));
            }
            catch (Exception ex)
            {
                result.AddMessage(ex, "Error on send e-mail");
            }

            return result;
        }
    }
}
