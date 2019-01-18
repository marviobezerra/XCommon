using System.Threading.Tasks;
using XCommon.Application.Executes;

namespace XCommon.Application.Authentication
{
	public interface IPasswordPolicesBusiness
	{
		Task<Execute> ValidatePasswordAsync(string email, string password);
	}
}
