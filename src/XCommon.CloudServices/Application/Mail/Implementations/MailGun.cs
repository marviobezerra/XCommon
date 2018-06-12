using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XCommon.Application.Executes;
using XCommon.Application.Mail;
using XCommon.Application.Settings;
using XCommon.Extensions.String;
using XCommon.Patterns.Ioc;

namespace XCommon.CloudServices.Application.Mail.Implementations
{
	public class MailGun : IMail
	{
		public MailGun()
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

			if (ApplicationSettings.Mail == null
				|| ApplicationSettings.Mail.MailGunKey.IsEmpty()
				|| ApplicationSettings.Mail.MailGunDomain.IsEmpty())
			{
				result.AddMessage(ExecuteMessageType.Error, "MailGun API Key/Domain is not defined on ApplicationSettings.");
				return result;
			}

			try
			{
				var client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes("api" + ":" + ApplicationSettings.Mail.MailGunKey)));

				var form = new Dictionary<string, string>
				{
					["from"] = $"{fromName} <{from}>",
					["to"] = $"{toName} <{to}>",
					["subject"] = subject,
					["text"] = body
				};

				var response = await client.PostAsync($"https://api.mailgun.net/v3/{ApplicationSettings.Mail.MailGunDomain}/messages", new FormUrlEncodedContent(form));

				if (response.StatusCode != HttpStatusCode.OK)
				{
					result.AddMessage(ExecuteMessageType.Error, $"Error sending email by MailGun. HTTP: {response.StatusCode}");
				}
			}
			catch (Exception ex)
			{
				result.AddMessage(ex, "Error sending email by MailGun");
			}

			return await Task.FromResult(result);
		}
	}
}
