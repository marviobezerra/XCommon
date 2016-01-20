using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Patterns.Specification.Entity.Extensions;
using XCommon.Test.Patterns.Specification.Helper;
using Xunit;

namespace XCommon.Test.Patterns.Specification.Entity
{
    public class AndIsValidRegexTest
    {
        [Fact]
        [Trait("Patterns Specification Entity AndIsValid", "Regex")]
        public void Patterns_Specification_Entity_AndIsValidRegex_Email_Valid_With_Execute()
        {
            string regex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            string message = "Email valid";
            Execute execute = new Execute();
            SampleEntity entity = new SampleEntity
            {
                Email = "jonhy@gmail.com"
            };

            SpecificationEntity<SampleEntity> spec = new SpecificationEntity<SampleEntity>()
                .AndIsValidRegex(c => c.Email, regex, message);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
            Assert.Equal(false, execute.HasErro);
            Assert.Equal(0, execute.Messages.Count);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndIsValid", "Regex")]
        public void Patterns_Specification_Entity_AndIsValidRegex_Email_Valid_Without_Execute()
        {
            string regex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            SampleEntity entity = new SampleEntity
            {
                Email = "jonhy@gmail.com"
            };

            SpecificationEntity<SampleEntity> spec = new SpecificationEntity<SampleEntity>()
                .AndIsValidRegex(c => c.Email, regex);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndIsValid", "Regex")]
        public void Patterns_Specification_Entity_AndIsValidRegex_Email_InValid_With_Execute()
        {
            string regex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            string message = "Email invalid";
            Execute execute = new Execute();
            SampleEntity entity = new SampleEntity
            {
                Email = "jonhygmailcom"
            };

            SpecificationEntity<SampleEntity> spec = new SpecificationEntity<SampleEntity>()
                .AndIsValidRegex(c => c.Email, regex, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(false, result);
            Assert.Equal(true, execute.HasErro);
            Assert.Equal(1, execute.Messages.Count);
            Assert.Equal(message, execute.Messages[0].Message);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndIsValid", "Regex")]
        public void Patterns_Specification_Entity_AndIsValidRegex_Email_InValid_Without_Execute()
        {
            string regex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            string message = "Email invalid";
            SampleEntity entity = new SampleEntity
            {
                Email = "jonhygmailcom"
            };

            SpecificationEntity<SampleEntity> spec = new SpecificationEntity<SampleEntity>()
                .AndIsValidRegex(c => c.Email, regex, message);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);            
        }

        [Fact]
        [Trait("Patterns Specification Entity AndIsValid", "Regex")]
        public void Patterns_Specification_Entity_AndIsValidRegex_InvalidRegex_With_Execute()
        {
            string regex = @"[0-9]++";
            string message = "Invalid regex error";
            SampleEntity entity = new SampleEntity
            {
                Email = "Nothing new"
            };

            SpecificationEntity<SampleEntity> spec = new SpecificationEntity<SampleEntity>()
                .AndIsValidRegex(c => c.Email, regex, message);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndIsValid", "Regex")]
        public void Patterns_Specification_Entity_AndIsValidRegex_InvalidRegex_Without_Execute()
        {
            string regex = @"[0-9]++";
            string message = "Invalid regex error";
            Execute execute = new Execute();
            SampleEntity entity = new SampleEntity
            {
                Email = "Nothing new"
            };

            SpecificationEntity<SampleEntity> spec = new SpecificationEntity<SampleEntity>()
                .AndIsValidRegex(c => c.Email, regex, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(false, result);
            Assert.Equal(true, execute.HasErro);
            Assert.Equal(1, execute.Messages.Count);
            Assert.Equal(string.Format(Properties.Resources.InvalidRegex, regex), execute.Messages[0].Message);
        }        
    }
}
