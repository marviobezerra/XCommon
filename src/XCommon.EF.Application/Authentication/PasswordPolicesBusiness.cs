using System;
using System.Linq;
using System.Threading.Tasks;
using XCommon.Application.Authentication;
using XCommon.Application.Executes;
using XCommon.Entity.System;
using XCommon.Extensions.String;

namespace XCommon.EF.Application.Authentication
{
	public class PasswordPolicesBusiness : IPasswordPolicesBusiness
	{

		public async Task<Execute> ValidatePasswordAsync(string email, string password)
		{
			var policy = new PasswordPolicesEntity();
			var result = new Execute();

			if (password.Length < policy.MinLength)
			{
				result.AddError(Resources.Messages.Password_MinLengthInvalid);
			}

			if (password.Length > policy.MaxLength)
			{
				result.AddError(Resources.Messages.Password_MaxLengthExceeded);
			}

			if (policy.RequireNumericChar && !password.ContainsNumericCharacters())
			{
				result.AddError(Resources.Messages.Password_NumericCharRequired);
			}

			if (policy.RequireLowercaseChar && !password.ContainsLowerCaseLetters())
			{
				result.AddError(Resources.Messages.Password_LowercaseCharRequired);
			}

			if (policy.RequireUppercaseChar && !password.ContainsUpperCaseLetters())
			{
				result.AddError(Resources.Messages.Password_UppercaseCharRequired);
			}

			if (policy.RequirePunctuationChar && !password.ContiansPunctuationCharacters())
			{
				result.AddError(Resources.Messages.Password_PunctuationCharRequired);
			}

			if (!policy.AllowSpaces && password.ContainsSpaces())
			{
				result.AddError(Resources.Messages.Password_SpaceNotAllowed);
			}

			if (!policy.AllowNonAscii && password.ContainsNonAsciiCharacters())
			{
				result.AddError(Resources.Messages.Password_NonAsciiNotAllowed);
			}

			if (!policy.ShouldNotMatchUserId)
			{
				var userMail = email.Split('@').First();

				if (password.Contains(userMail, StringComparison.InvariantCultureIgnoreCase))
				{
					result.AddError(Resources.Messages.Password_ShouldNotMatchUserId);
				}
			}

			return await Task.FromResult(result);
		}
	}
}
