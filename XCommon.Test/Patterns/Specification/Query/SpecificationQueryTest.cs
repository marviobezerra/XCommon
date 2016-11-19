using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using XCommon.Patterns.Specification.Query;
using XCommon.Test.Entity;
using XCommon.Test.Patterns.Specification.Query.DataSource;
using XCommon.Test.Patterns.Specification.Query.Sample;
using Xunit;

namespace XCommon.Test.Patterns.Specification.Query
{
    public class SpecificationQueryTest
    {
        [Theory(DisplayName = "Query (Simple)")]
        [MemberData(nameof(SpecificationQueryDataSource.DefaultDataSource), MemberType = typeof(SpecificationQueryDataSource))]
        public void QuerySimple(List<PersonEntity> source, PersonFilter filter, int recordCount, string message)
        {
            IQueryBuilder<PersonEntity, PersonFilter> query = new PersonQuery();

            var result = query.Build(source, filter);
            result.Count().Should().Be(recordCount);
        }
    }
}
