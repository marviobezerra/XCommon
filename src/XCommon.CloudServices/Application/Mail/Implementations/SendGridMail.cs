using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using XCommon.Application;
using XCommon.Application.Executes;
using XCommon.Application.Mail;
using XCommon.Extensions.String;
using XCommon.Patterns.Ioc;

namespace XCommon.CloudServices.Application.Mail.Implementations
{
	public class SendGridMail : IMail
	{
		public SendGridMail()
		{
			Kernel.Resolve(this);
		}

		[Inject]
		private IApplicationSettings ApplicationSettings { get; set; }

		public async Task<Execute> SendAsync(string from, string to, string subject, string body)
			=> await SendAsync(from, from, to, to, subject, body);

		public async Task<Execute> SendAsync(string from, string fromName, string to, string toName, string subject, string body)
		{
			var result = new Execute();

			if (ApplicationSettings.CloudKeys == null || ApplicationSettings.CloudKeys.SendGridKey.IsEmpty())
			{
				result.AddMessage(ExecuteMessageType.Error, "SendGrid API Key is not defined on ApplicationSettings.");
				return result;
			}

			try
			{
				var client = new SendGridClient(ApplicationSettings.CloudKeys.SendGridKey);
				var msg = MailHelper.CreateSingleEmail(new EmailAddress(from, fromName), new EmailAddress(to, toName), subject, body, body);
				var response = await client.SendEmailAsync(msg);
			}
			catch (Exception ex)
			{
				result.AddMessage(ex, "Error sending email by SendGrid");
			}

			return result;
		}
	}
}
