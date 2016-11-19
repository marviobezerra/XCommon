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
            Specifications
                .And(e => e.Id == filter.Id, filter.Id.HasValue)
                .And(e => filter.Ids.Contains(e.Id), filter.Ids.Count > 0)
                .And(e => e.Name.Contains(filter.Name), filter.Name.IsNotEmpty())
                .And(e => e.Email == filter.Email, filter.Email.IsNotEmpty())
                .And(e => e.Age == filter.Age, filter.Age.HasValue)
                .OrderBy(e => e.Name)
                .Take(filter.PageNumber, filter.PageSize);

            return CheckSpecifications(source, filter);
        }
    }
}
