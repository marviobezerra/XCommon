using System.Linq;
using XCommon.EF.Application.Context.Register;
using XCommon.Entity.Register.Filter;
using XCommon.Extensions.Checks;
using XCommon.Extensions.String;
using XCommon.Patterns.Specification.Query;
using XCommon.Patterns.Specification.Query.Extensions;

namespace XCommon.EF.Application.Register.Implementation.Query
{
	public class UsersTokensQuery : SpecificationQuery<UsersTokens, UsersTokensFilter>
	{
		public override IQueryable<UsersTokens> Build(IQueryable<UsersTokens> source, UsersTokensFilter filter)
		{
			var spefications = NewSpecificationList()
				.And(e => e.IdUserToken == filter.Key, f => f.Key.HasValue)
				.And(e => e.IdUser == filter.IdUser, f => f.Key.HasValue)
				.And(e => filter.Keys.Contains(e.IdUserToken), f => f.Keys.IsValidList())
				.And(e => filter.IdUsers.Contains(e.IdUser), f => f.IdUsers.IsValidList())
				.And(e => e.TokenType == filter.TokenType, f => f.TokenType.IsNotEmpty())
				.OrderBy(e => e.IdUser)
				.Take(filter.PageNumber, filter.PageSize);

			return CheckSpecifications(spefications, source, filter);
		}
	}
}
