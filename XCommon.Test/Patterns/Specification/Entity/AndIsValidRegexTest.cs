using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Test.Patterns.Specification.Helper;

namespace XCommon.Test.Patterns.Specification.Entity
{
    [TestClass]
    public class AndIsValidRegexTest
    {
        [TestMethod]
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

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
            Assert.AreEqual(0, execute.Messages.Count);
        }

        [TestMethod]
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

            Assert.AreEqual(true, result);
        }

        [TestMethod]
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

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(1, execute.Messages.Count);
            Assert.AreEqual(message, execute.Messages[0].Message);
        }

        [TestMethod]
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

            Assert.AreEqual(false, result);            
        }

        [TestMethod]
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

            Assert.AreEqual(false, result);
        }

        [TestMethod]
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

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(1, execute.Messages.Count);
            Assert.AreEqual(string.Format(Properties.Resources.InvalidRegex, regex), execute.Messages[0].Message);
        }        
    }
}
