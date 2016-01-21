using System;
using System.Collections.Generic;
using XCommon.Patterns.Specification.Entity.Extensions;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Test.Patterns.Specification.Helper;
using Xunit;

namespace XCommon.Test.Patterns.Specification.Entity
{
    public class AndIsNotEmptyTest
    {
        [Theory]
        [InlineData(null, false)]
        [InlineData(1, true)]
        [Trait("Patterns Specification Entity AndIsNotEmpty", "Int")]
        public void NotEmpty_Int_NotNull_Without_Execute(int? value, bool valid)
        {
            GenerictValueEntity<int?> entity = new GenerictValueEntity<int?>(value);

            SpecificationEntity<GenerictValueEntity<int?>> spec = new SpecificationEntity<GenerictValueEntity<int?>>()
                .AndIsNotEmpty(c => c.Value);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }

        [Theory]
        [MemberData(nameof(GetNotEmpty_Date_NotNull_Without_Execute))]
        public void NotEmpty_Date_NotNull_Without_Execute(DateTime? value, bool valid)
        {
            GenerictValueEntity<DateTime?> entity = new GenerictValueEntity<DateTime?>(value);

            SpecificationEntity<GenerictValueEntity<DateTime?>> spec = new SpecificationEntity<GenerictValueEntity<DateTime?>>()
                .AndIsNotEmpty(c => c.Value);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }

        /// <summary>
        /// Provide data to NotEmpty_Date_NotNull_Without_Execute
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GetNotEmpty_Date_NotNull_Without_Execute()
        {
            yield return new object[] { (DateTime?)null, false };
            yield return new object[] { (DateTime?)new DateTime(2015, 1, 1), true };
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("Hello word!", true)]
        [Trait("Patterns Specification Entity AndIsNotEmpty", "String")]
        public void NotEmpty_String_NotNull_Without_Execute(string value, bool valid)
        {
            GenerictValueEntity<string> entity = new GenerictValueEntity<string>(value);

            SpecificationEntity<GenerictValueEntity<string>> spec = new SpecificationEntity<GenerictValueEntity<string>>()
                .AndIsNotEmpty(c => c.Value);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }

        [InlineData(false, false)]
        [InlineData(true, true)]
        [Trait("Patterns Specification Entity AndIsNotEmpty", "Object")]
        public void NotEmpty_Object_NotNull_Without_Execute(bool create, bool valid)
        {
            GenerictValueEntity<object> entity = create 
                ? new GenerictValueEntity<object>("X")
                : null;

            SpecificationEntity<GenerictValueEntity<object>> spec = new SpecificationEntity<GenerictValueEntity<object>>()
                .AndIsNotEmpty(c => c);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }
    }
}
