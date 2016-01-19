using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Test.Patterns.Specification.Helper;

namespace XCommon.Test.Patterns.Specification.Entity
{
    [TestClass]
    public class AndIsNotEmptyTest
    {
        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Int_NotNull_Without_Execute()
        {
            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
                .AndIsNotEmpty(c => c.Int);

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(false));

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Date_NotNull_Without_Execute()
        {
            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
                .AndIsNotEmpty(c => c.DateTime);

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(false));

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_String_NotNull_Without_Execute()
        {
            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
                .AndIsNotEmpty(c => c.String);

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(false));

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Object_NotNull_Without_Execute()
        {
            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
                .AndIsNotEmpty(c => c.Item);

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(false));

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Int_Null_Without_Execute()
        {

            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
                .AndIsNotEmpty(c => c.Int);

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(true));

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Date_Null_Without_Execute()
        {
            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
               .AndIsNotEmpty(c => c.DateTime);

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(true));

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_String_Null_Without_Execute()
        {
            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
               .AndIsNotEmpty(c => c.String);

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(true));

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Object_Null_Without_Execute()
        {
            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
               .AndIsNotEmpty(c => c.Item);

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(true));

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Int_NotNull_With_Execute()
        {
            Execute execute = new Execute();
            string message = "Int not null";

            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
                .AndIsNotEmpty(c => c.Int, message);

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(false), execute);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Date_NotNull_With_Execute()
        {
            Execute execute = new Execute();
            string message = "Date not null";

            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
                .AndIsNotEmpty(c => c.DateTime, message);

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(false), execute);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_String_NotNull_With_Execute()
        {
            Execute execute = new Execute();
            string message = "String not null";

            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
                .AndIsNotEmpty(c => c.String, message);

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(false), execute);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Object_NotNull_With_Execute()
        {
            Execute execute = new Execute();
            string message = "Object not null";

            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
                .AndIsNotEmpty(c => c.Item, message);

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(false), execute);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Int_Null_With_Execute()
        {
            Execute execute = new Execute();
            string message = "Int null";

            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
                .AndIsNotEmpty(c => c.Int, message);

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(true), execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(message, execute.Messages[0].Message);
            Assert.AreEqual(1, execute.Messages.Count);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Date_Null_With_Execute()
        {
            Execute execute = new Execute();
            string message = "DateTime null";

            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
               .AndIsNotEmpty(c => c.DateTime, message);

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(true), execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(message, execute.Messages[0].Message);
            Assert.AreEqual(1, execute.Messages.Count);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_String_Null_With_Execute()
        {
            Execute execute = new Execute();
            string message = "String null";

            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
               .AndIsNotEmpty(c => c.String, message);

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(true), execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(message, execute.Messages[0].Message);
            Assert.AreEqual(1, execute.Messages.Count);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Object_Null_With_Execute()
        {
            Execute execute = new Execute();
            string message = "Object null";

            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
               .AndIsNotEmpty(c => c.Item, message);

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(true), execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(message, execute.Messages[0].Message);
            Assert.AreEqual(1, execute.Messages.Count);
        }
        
        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Int_Null_With_Execute_Format()
        {
            Execute execute = new Execute();
            string message = "{0} null";
            string messageExpected = "Int null";

            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
                .AndIsNotEmpty(c => c.Int, message, "Int");

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(true), execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(messageExpected, execute.Messages[0].Message);
            Assert.AreEqual(1, execute.Messages.Count);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Date_Null_With_Execute_Format()
        {
            Execute execute = new Execute();
            string message = "{0} null";
            string messageExpected = "DateTime null";

            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
               .AndIsNotEmpty(c => c.DateTime, message, "DateTime");

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(true), execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(messageExpected, execute.Messages[0].Message);
            Assert.AreEqual(1, execute.Messages.Count);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_String_Null_With_Execute_Format()
        {
            Execute execute = new Execute();
            string message = "{0} null";
            string messageExpected = "String null";

            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
               .AndIsNotEmpty(c => c.String, message, "String");

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(true), execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(messageExpected, execute.Messages[0].Message);
            Assert.AreEqual(1, execute.Messages.Count);
        }

        [TestMethod]
        [TestCategory("Patterns Specification Entity AndIsNotEmpty")]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Object_Null_With_Execute_Format()
        {
            Execute execute = new Execute();
            string message = "{0} null";
            string messageExpected = "Object null";

            SpecificationEntity<AndIsNotEmptyEntity> spec = new SpecificationEntity<AndIsNotEmptyEntity>()
               .AndIsNotEmpty(c => c.Item, message, "Object");

            var result = spec.IsSatisfiedBy(new AndIsNotEmptyEntity(true), execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(messageExpected, execute.Messages[0].Message);
            Assert.AreEqual(1, execute.Messages.Count);
        }
    }
}
