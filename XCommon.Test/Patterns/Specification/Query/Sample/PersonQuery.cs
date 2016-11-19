using System.Collections.Generic;
using System.Linq;
using XCommon.Extensions.String;
using XCommon.Patterns.Specification.Query;
using XCommon.Test.Entity;

namespace XCommon.Test.Patterns.Specification.Query.Sample
{
    public class PersonQuery : IQueryBuilder<PersonEntity, PersonFilter>
    {
        public IQueryable<PersonEntity> Build(IQueryable<PersonEntity> query, PersonFilter filter)
        {
            QueryBuilder<PersonEntity, PersonFilter> result = new QueryBuilder<PersonEntity, PersonFilter>()
               .And(e => e.Id == filter.Id, filter.Id.HasValue)
               .And(e => filter.Ids.Contains(e.Id), filter.Ids.Count > 0)
               .And(e => e.Name.Contains(filter.Name), filter.Name.IsNotEmpty())
               .And(e => e.Email == filter.Email, filter.Email.IsNotEmpty())
               .And(e => e.Age == filter.Age, filter.Age.HasValue)
               .OrderBy(e => e.Name)
               .Take(filter.PageNumber, filter.PageSize);

            return result.Build(query, filter);
        }

        public IQueryable<PersonEntity> Build(IEnumerable<PersonEntity> query, PersonFilter filter)
        {
            return Build(query.AsQueryable(), filter);
        }
    }
}
