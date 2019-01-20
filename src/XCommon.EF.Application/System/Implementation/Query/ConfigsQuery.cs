using System.Linq;
using XCommon.EF.Application.Context.System;
using XCommon.Entity.System.Filter;
using XCommon.Extensions.Checks;
using XCommon.Extensions.String;
using XCommon.Patterns.Specification.Query;
using XCommon.Patterns.Specification.Query.Extensions;

namespace XCommon.EF.Application.System.Implementation.Query
{
	public class ConfigsQuery : SpecificationQuery<Configs, ConfigsFilter>
	{
		public override IQueryable<Configs> Build(IQueryable<Configs> source, ConfigsFilter filter)
		{
			var spefications = NewSpecificationList()
				.And(e => e.IdConfig == filter.Key, f => f.Key.HasValue)
				.And(e => filter.Keys.Contains(e.IdConfig), f => f.Keys.IsValidList())
				.And(e => e.ConfigKey == filter.ConfigKey, f => f.ConfigKey.IsNotEmpty())
				.And(e => e.Module == filter.Module, f => f.Module.IsNotEmpty())
				.OrderBy(e => e.ConfigKey)
				.Take(filter.PageNumber, filter.PageSize);

			return CheckSpecifications(spefications, source, filter);
		}
	}
}
