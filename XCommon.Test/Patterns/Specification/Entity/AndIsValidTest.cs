using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Patterns.Specification.Entity.Extensions;
using XCommon.Test.Patterns.Specification.Helper;
using Xunit;

namespace XCommon.Test.Patterns.Specification.Entity
{
    public class AndIsValidTest
    {
        [Theory]
        [InlineData("jonhy@gmail.com")]
        [InlineData("jonhy@gmail.us")]
        [InlineData("jonhy@gmail.io")]
        [InlineData("jonhy@gmail.com.cz")]
        [Trait("Patterns Specification Entity AndIsValid", "Email")]
        public void Patterns_Specification_Entity_AndIsValid_Email_Valid_With_Execute(string email)
        {
            string message = "Email valid";
            Execute execute = new Execute();
            SampleEntity entity = new SampleEntity
            {
                Email = email
            };

            SpecificationEntity<SampleEntity> spec = new SpecificationEntity<SampleEntity>()
                .AndIsEmail(c => c.Email, message);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
            Assert.Equal(false, execute.HasErro);
            Assert.Equal(0, execute.Messages.Count);
        }

        [Theory]
        [InlineData("jonhy@gmail.com")]
        [InlineData("jonhy@gmail.us")]
        [InlineData("jonhy@gmail.io")]
        [InlineData("jonhy@gmail.com.cz")]
        [Trait("Patterns Specification Entity AndIsValid", "Email")]
        public void Patterns_Specification_Entity_AndIsValid_Email_Valid_Without_Execute(string email)
        {
            string message = "Email valid";
            SampleEntity entity = new SampleEntity
            {
                Email = email
            };

            SpecificationEntity<SampleEntity> spec = new SpecificationEntity<SampleEntity>()
                .AndIsEmail(c => c.Email, message);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }
        
        [Theory]
        [InlineData("jonhygmail.com")]
        [InlineData("jonhy@gmail")]
        [InlineData("jonhy@.io")]
        [InlineData("jonhygmailcomcz")]
        [Trait("Patterns Specification Entity AndIsValid", "Email")]
        public void Patterns_Specification_Entity_AndIsValid_Email_InValid_With_Execute(string email)
        {
            string message = "Email invalid";
            Execute execute = new Execute();
            SampleEntity entity = new SampleEntity
            {
                Email = email
            };

            SpecificationEntity<SampleEntity> spec = new SpecificationEntity<SampleEntity>()
                .AndIsEmail(c => c.Email, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(false, result);
            Assert.Equal(true, execute.HasErro);
            Assert.Equal(1, execute.Messages.Count);
            Assert.Equal(message, execute.Messages[0].Message);
        }

        [Theory]
        [InlineData("jonhygmail.com")]
        [InlineData("jonhy@gmail")]
        [InlineData("jonhy@.io")]
        [InlineData("jonhygmailcomcz")]
        [Trait("Patterns Specification Entity AndIsValid", "Email")]
        public void Patterns_Specification_Entity_AndIsValid_Email_InValid_Without_Execute(string email)
        {
            string message = "Email invalid";
            SampleEntity entity = new SampleEntity
            {
                Email = email
            };

            SpecificationEntity<SampleEntity> spec = new SpecificationEntity<SampleEntity>()
                .AndIsEmail(c => c.Email, message);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }
        
        [Theory]
        [InlineData("http://www.google.com")]
        [InlineData("http://www.google.com.br")]
        [InlineData("www.google.com.br")]
        [InlineData("www.google.com")]
        [InlineData("www.google.us")]
        [Trait("Patterns Specification Entity AndIsValid", "URL")]
        public void Patterns_Specification_Entity_AndIsValid_URL_Valid_With_Execute(string url)
        {
            string message = "Url valid";
            Execute execute = new Execute();
            SampleEntity entity = new SampleEntity
            {
                Url = url
            };

            SpecificationEntity<SampleEntity> spec = new SpecificationEntity<SampleEntity>()
                .AndIsURL(c => c.Url, message);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
            Assert.Equal(false, execute.HasErro);
            Assert.Equal(0, execute.Messages.Count);
        }

        [Theory]
        [InlineData("http://www.google.com")]
        [InlineData("http://www.google.com.br")]
        [InlineData("www.google.com.br")]
        [InlineData("www.google.com")]
        [InlineData("www.google.us")]
        [InlineData("google.com")]
        [InlineData("google.com.br")]
        [Trait("Patterns Specification Entity AndIsValid", "URL")]
        public void Patterns_Specification_Entity_AndIsValid_URL_Valid_Without_Execute(string url)
        {
            string message = "Url valid";
            SampleEntity entity = new SampleEntity
            {
                Url = url
            };

            SpecificationEntity<SampleEntity> spec = new SpecificationEntity<SampleEntity>()
                .AndIsURL(c => c.Url, message);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }

        [Theory]
        [InlineData("wwwgooglecom")]
        [InlineData("12345")]
        [InlineData("^^^://nada.com")]
        [InlineData("&&&://nada.com")]
        [InlineData("http://nadacom")]
        [Trait("Patterns Specification Entity AndIsValid", "URL")]
        public void Patterns_Specification_Entity_AndIsValid_URL_InValid_With_Execute(string url)
        {
            string message = "Url invalid";
            Execute execute = new Execute();
            SampleEntity entity = new SampleEntity
            {
                Url = url
            };

            SpecificationEntity<SampleEntity> spec = new SpecificationEntity<SampleEntity>()
                .AndIsURL(c => c.Url, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(false, result);
            Assert.Equal(true, execute.HasErro);
            Assert.Equal(1, execute.Messages.Count);
            Assert.Equal(message, execute.Messages[0].Message);
        }

        [Theory]
        [Trait("Patterns Specification Entity AndIsValid", "URL")]
        [InlineData("wwwgooglecom")]
        [InlineData("12345")]
        [InlineData("^^^://nada.com")]
        [InlineData("&&&://nada.com")]
        [InlineData("http://nadacom")]
        public void Patterns_Specification_Entity_AndIsValid_URL_InValid_Without_Execute(string url)
        {
            string message = "Email invalid";
            SampleEntity entity = new SampleEntity
            {
                Url = url
            };

            SpecificationEntity<SampleEntity> spec = new SpecificationEntity<SampleEntity>()
                .AndIsURL(c => c.Url, message);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }
    }
}
