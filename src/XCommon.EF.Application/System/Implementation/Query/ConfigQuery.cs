using System.Linq;
using XCommon.EF.Application.Context.System;
using XCommon.Entity.System.Filter;
using XCommon.Extensions.Checks;
using XCommon.Extensions.String;
using XCommon.Patterns.Specification.Query;
using XCommon.Patterns.Specification.Query.Extensions;

namespace XCommon.EF.Application.System.Implementation.Query
{
	public class ConfigQuery : SpecificationQuery<Config, ConfigFilter>
	{
		public override IQueryable<Config> Build(IQueryable<Config> source, ConfigFilter filter)
		{
			var spefications = NewSpecificationList()
				.And(e => e.IdConfig == filter.Key, f => f.Key.HasValue)
				.And(e => filter.Keys.Contains(e.IdConfig), f => f.Keys.IsValidList())
				.And(e => e.ConfigKey == filter.ConfigKey, f => f.ConfigKey.IsNotEmpty())
				.And(e => e.Section == filter.Section, f => f.Section.IsNotEmpty())
				.OrderBy(e => e.ConfigKey)
				.Take(filter.PageNumber, filter.PageSize);

			return CheckSpecifications(spefications, source, filter);
		}
	}
}
