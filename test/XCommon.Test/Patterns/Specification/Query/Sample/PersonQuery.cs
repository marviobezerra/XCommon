using System.Linq;
using XCommon.Extensions.String;
using XCommon.Patterns.Specification.Query;
using XCommon.Patterns.Specification.Query.Extensions;
using XCommon.Test.Entity;

namespace XCommon.Test.Patterns.Specification.Query.Sample
{
	public class PersonQuery : SpecificationQuery<PersonEntity, PersonFilter>
	{
		public override IQueryable<PersonEntity> Build(IQueryable<PersonEntity> source, PersonFilter filter)
		{
			var specifications = NewSpecificationList()
				.And(e => e.Id == filter.Id, f => f.Id.HasValue)
				.And(e => filter.Ids.Contains(e.Id), f => f.Ids.Count > 0)
				.And(e => e.Name.Contains(filter.Name), f => f.Name.IsNotEmpty())
				.And(e => e.Email == filter.Email, f => f.Email.IsNotEmpty())
				.And(e => e.Age == filter.Age, f => f.Age.HasValue)
				.OrderBy(e => e.Name)
				.Take(filter);

			return CheckSpecifications(specifications, source, filter);
		}
	}
}
