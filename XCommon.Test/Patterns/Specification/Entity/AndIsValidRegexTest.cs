using XCommon.Patterns.Specification.Entity.Extensions;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Test.Patterns.Specification.Helper;
using Xunit;

namespace XCommon.Test.Patterns.Specification.Entity
{
    public class AndIsValidRegexTest
    {
        [Theory]
        [InlineData(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", "jonhy@gmail.com", true)]
        [InlineData(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", "jonhygmailcom", false)]
        [InlineData(@"[0-9]++", "Nothing new", false)]
        [InlineData(@"\d+", "Hello 55 World!", true)]
        [Trait("Patterns Specification Entity AndIsValid", "Regex")]
        public void Patterns_Specification_Entity_AndIsValidRegex_Email_Valid_With_Execute(string regex, string value, bool valid)
        {
            GenerictValueEntity<string> entity = new GenerictValueEntity<string>(value);

            SpecificationEntity<GenerictValueEntity<string>> spec = new SpecificationEntity<GenerictValueEntity<string>>()
                .AndIsValidRegex(c => c.Value, regex);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }
    }
}
