using System.Threading.Tasks;
using XCommon.Application;
using XCommon.Application.Mail;
using XCommon.CloudServices.Application.Mail.Implementations;
using XCommon.Patterns.Ioc;
using Xunit;

namespace XCommon.Test.CloudServices
{
	public class MailGunTest
	{
		[SkippableFact]
		public async Task SendMail()
		{
			var app = new ApplicationSettings
			{
				CloudKeys = new CloudServicesKeys
				{
					MailGunDomain = "sandbox412370cd86ec43da8b550f934d447d47.mailgun.org",
					MailGunKey = "key-4f555f7ea4764348c6b872580e5ef1c8"
				}
			};

			Kernel.Map<IApplicationSettings>().To(app);
			Kernel.Map<IMail>().To<MailGun>();

			var mail = Kernel.Resolve<IMail>();
			var result = await mail.SendAsync("marvio.bezerra@hotmail.com", "marvio.bezerra@gmail.com", "Test", "Hello world");
			Assert.False(result.HasErro);
		}
	}
}
