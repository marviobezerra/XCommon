using XCommon.Patterns.Specification.Entity.Extensions;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Test.Patterns.Specification.Helper;
using Xunit;

namespace XCommon.Test.Patterns.Specification.Entity
{
    public class AndIsValidTest
    {
        [Theory]
        [InlineData("jonhy@gmail.com", true)]
        [InlineData("jonhy@gmail.us", true)]
        [InlineData("jonhy@gmail.io", true)]
        [InlineData("jonhy@gmail.com.cz", true)]
        [InlineData("jonhygmail.com", false)]
        [InlineData("jonhy@gmail", false)]
        [InlineData("jonhy@.io", false)]
        [InlineData("jonhygmailcomcz", false)]
        [Trait("Patterns Specification Entity AndIsValid", "Email")]
        public void Patterns_Specification_Entity_AndIsValid_Email_Valid_With_Execute(string email, bool valid)
        {
            GenerictValueEntity<string> entity = new GenerictValueEntity<string>(email);

            SpecificationEntity<GenerictValueEntity<string>> spec = new SpecificationEntity<GenerictValueEntity<string>>()
                .AndIsEmail(c => c.Value);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }
        
        [Theory]
        [InlineData("http://www.google.com", true)]
        [InlineData("http://www.google.com.br", true)]
        [InlineData("www.google.com.br", true)]
        [InlineData("www.google.com", true)]
        [InlineData("www.google.us", true)]
        [InlineData("wwwgooglecom", false)]
        [InlineData("12345", false)]
        [InlineData("^^^://nada.com", false)]
        [InlineData("&&&://nada.com", false)]
        [InlineData("http://nadacom", false)]
        [Trait("Patterns Specification Entity AndIsValid", "URL")]
        public void Patterns_Specification_Entity_AndIsValid_URL_Valid_With_Execute(string url, bool valid)
        {
            GenerictValueEntity<string> entity = new GenerictValueEntity<string>(url);

            SpecificationEntity<GenerictValueEntity<string>> spec = new SpecificationEntity<GenerictValueEntity<string>>()
                .AndIsURL(c => c.Value);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }
    }
}
