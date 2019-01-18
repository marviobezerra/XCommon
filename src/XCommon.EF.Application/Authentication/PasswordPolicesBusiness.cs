using System.Linq;
using XCommon.Application.Executes;
using XCommon.Entity.Config;
using XCommon.Extensions.String;

namespace XCommon.EF.Application.Authentication
{
	public class PasswordPolicesBusiness
	{
		public Execute ValidatePassword(string email, string password)
		{
			var policy = new PasswordPolicesEntity();
			var result = new Execute();

			if (password.Length < policy.MinLength)
			{
				result.AddError("MinLengthInvalid");
			}

			if (password.Length > policy.MaxLength)
			{
				result.AddError("MaxLengthExceeded");
			}

			if (policy.RequireNumericChar && !password.ContainsNumericCharacters())
			{
				result.AddError("NumericCharRequired");
			}

			if (policy.RequireLowercaseChar && !password.ContainsLowerCaseLetters())
			{
				result.AddError("LowercaseCharRequired");
			}

			if (policy.RequireUppercaseChar && !password.ContainsUpperCaseLetters())
			{
				result.AddError("UppercaseCharRequired");
			}

			if (policy.RequirePunctuationChar && !password.ContiansPunctuationCharacters())
			{
				result.AddError("PunctuationCharRequired");
			}

			if (!policy.AllowSpaces && password.ContainsSpaces())
			{
				result.AddError("SpaceNotAllowed");
			}

			if (!policy.AllowNonAscii && password.ContainsNonAsciiCharacters())
			{
				result.AddError("NonAsciiNotAllowed");
			}

			if (!policy.ShouldNotMatchUserId)
			{
				var userMail = email.Split('@').First();

				if (password.Contains(userMail, System.StringComparison.OrdinalIgnoreCase | System.StringComparison.InvariantCulture))
				{
					result.AddError("ShouldNotMatchUserId");
				}
			}

			return result;
		}
	}
}
