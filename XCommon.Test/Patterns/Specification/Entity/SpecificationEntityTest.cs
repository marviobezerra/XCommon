using XCommon.Patterns.Specification.Entity.Extensions;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Test.Patterns.Specification.Helper;
using Xunit;

namespace XCommon.Test.Patterns.Specification.Entity
{
    public class SpecificationEntityTest
    {
        [Fact]
        public void Patterns_Specification_Entity_SpecificationEntity_Whit_Null_Entity()
        {
            SpecificationEntity<SampleValueEntity> spec = new SpecificationEntity<SampleValueEntity>()
                .AndIsNotEmpty(c => c.IntEnd);

            var result = spec.IsSatisfiedBy(null);            
            Assert.False(result);
        }
    }
}
