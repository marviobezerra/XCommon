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
        public SpecificationQueryTest()
        {
            Specification = new PersonQuery();
        }

        public ISpecificationQuery<PersonEntity, PersonFilter> Specification { get; set; }

        [Theory(DisplayName = "Query (Simple)")]
		[Trait("Patterns", "Specification - Query")]
		[MemberData(nameof(SpecificationQueryDataSource.DataSourceDefault), MemberType = typeof(SpecificationQueryDataSource))]
        public void QuerySimple(List<PersonEntity> source, PersonFilter filter, int recordCount, string message)
        {
            var result = Specification.Build(source, filter);
            result.Count().Should().Be(recordCount, message);
        }

        [Theory(DisplayName = "Query (Order)")]
		[Trait("Patterns", "Specification - Query")]
		[MemberData(nameof(SpecificationQueryDataSource.DataSourceOrder), MemberType = typeof(SpecificationQueryDataSource))]
        public void QueryOrder(List<PersonEntity> source, PersonFilter filter, PersonEntity expected, string message)
        {
            var result = Specification.Build(source, filter);
            result.Count().Should().Be(1);
            result.First().Should().BeEquivalentTo(expected, message);
        }
    }
}
