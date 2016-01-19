using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Test.Patterns.Specification.Helper;

namespace XCommon.Test.Patterns.Specification.Entity
{
    [TestClass]
    public class AndCheckValueTest
    {
        #region BiggerThan
        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Int_BiggerThan_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.IntValue, c => c.IntStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_BiggerThan_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DecimalValue, c => c.DecimalStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_BiggerThan_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DateTimeValue, c => c.DateTimeStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_BiggerThan_Valid_RemoveTime_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DateTimeValue, c => c.DateTimeStart, true);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Int_BiggerThan_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.IntValue, c => c.IntStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_BiggerThan_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DecimalValue, c => c.DecimalStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_BiggerThan_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DateTimeValue, c => c.DateTimeStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_BiggerThan_InValid_RemoveTime_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DateTimeValue, c => c.DateTimeStart, true);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Int_BiggerThan_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);
            string message = "Int Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.IntValue, c => c.IntStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
            Assert.AreEqual(0, execute.Messages.Count);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_BiggerThan_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);
            string message = "Decimal Valid";


            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DecimalValue, c => c.DecimalStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
            Assert.AreEqual(0, execute.Messages.Count);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_BiggerThan_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);
            string message = "DateTime Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DateTimeValue, c => c.DateTimeStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
            Assert.AreEqual(0, execute.Messages.Count);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_BiggerThan_Valid_RemoveTime_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);
            string message = "DateTime Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DateTimeValue, c => c.DateTimeStart, true, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
            Assert.AreEqual(0, execute.Messages.Count);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Int_BiggerThan_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, false);
            string message = "Int InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.IntValue, c => c.IntStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(message, execute.Messages[0].Message);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_BiggerThan_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, false);
            string message = "Decimal InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DecimalValue, c => c.DecimalStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(message, execute.Messages[0].Message);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_BiggerThan_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, false);
            string message = "DateTime InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DateTimeValue, c => c.DateTimeStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(message, execute.Messages[0].Message);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_BiggerThan_InValid_RemoveTime_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, false);
            string message = "Date InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DateTimeValue, c => c.DateTimeStart, true, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(message, execute.Messages[0].Message);
        }
        #endregion

        #region LessThan
        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Int_LessThan_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.IntValue, c => c.IntStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_LessThan_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DecimalValue, c => c.DecimalStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_LessThan_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DateTimeValue, c => c.DateTimeStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_RemoveTime_LessThan_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DateTimeValue, c => c.DateTimeStart, true);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Int_LessThan_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.IntValue, c => c.IntStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_LessThan_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DecimalValue, c => c.DecimalStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_LessThan_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DateTimeValue, c => c.DateTimeStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_RemoveTime_InLessThan_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DateTimeValue, c => c.DateTimeStart, true);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Int_LessThan_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, true);
            string message = "Int Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.IntValue, c => c.IntStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
            Assert.AreEqual(0, execute.Messages.Count);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_LessThan_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, true);
            string message = "Decimal Valid";


            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DecimalValue, c => c.DecimalStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
            Assert.AreEqual(0, execute.Messages.Count);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_LessThan_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, true);
            string message = "DateTime Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DateTimeValue, c => c.DateTimeStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
            Assert.AreEqual(0, execute.Messages.Count);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_RemoveTime_LessThan_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, true);
            string message = "DateTime Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DateTimeValue, c => c.DateTimeStart, true, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
            Assert.AreEqual(0, execute.Messages.Count);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Int_LessThan_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, false);
            string message = "Int InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.IntValue, c => c.IntStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(message, execute.Messages[0].Message);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_LessThan_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, false);
            string message = "Decimal InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DecimalValue, c => c.DecimalStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(message, execute.Messages[0].Message);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_LessThan_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, false);
            string message = "DateTime InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DateTimeValue, c => c.DateTimeStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(message, execute.Messages[0].Message);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_RemoveTime_InLessThan_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, false);
            string message = "Date InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DateTimeValue, c => c.DateTimeStart, true, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(message, execute.Messages[0].Message);
        }
        #endregion

        #region InRange
        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Int_InRange_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.IntValue, c => c.IntStart, c => c.IntEnd);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_InRange_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DecimalValue, c => c.DecimalStart, c => c.DecimalEnd);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_InRange_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DateTimeValue, c => c.DateTimeStart, c => c.DateTimeEnd);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_RemoveTime_InRange_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DateTimeValue, c => c.DateTimeStart, c => c.DateTimeEnd, true);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Int_InRange_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.IntValue, c => c.IntStart, c => c.IntEnd);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_InRange_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DecimalValue, c => c.DecimalStart, c => c.DecimalEnd);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_InRange_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DateTimeValue, c => c.DateTimeStart, c => c.DateTimeEnd);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_RemoveTime_InInRange_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DateTimeValue, c => c.DateTimeStart, c => c.DateTimeEnd, true);

            var result = spec.IsSatisfiedBy(entity);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Int_InRange_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, true);
            string message = "Int Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.IntValue, c => c.IntStart, c => c.IntEnd, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
            Assert.AreEqual(0, execute.Messages.Count);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_InRange_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, true);
            string message = "Decimal Valid";


            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DecimalValue, c => c.DecimalStart, c => c.DecimalEnd, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
            Assert.AreEqual(0, execute.Messages.Count);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_InRange_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, true);
            string message = "DateTime Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DateTimeValue, c => c.DateTimeStart, c => c.DateTimeEnd, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
            Assert.AreEqual(0, execute.Messages.Count);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_RemoveTime_InRange_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, true);
            string message = "DateTime Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DateTimeValue, c => c.DateTimeStart, c => c.DateTimeEnd, true, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, execute.HasErro);
            Assert.AreEqual(0, execute.Messages.Count);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Int_InRange_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, false);
            string message = "Int InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.IntValue, c => c.IntStart, c => c.IntEnd, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(message, execute.Messages[0].Message);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_InRange_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, false);
            string message = "Decimal InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DecimalValue, c => c.DecimalStart, c => c.DecimalEnd, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(message, execute.Messages[0].Message);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_InRange_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, false);
            string message = "DateTime InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DateTimeValue, c => c.DateTimeStart, c => c.DateTimeEnd, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(message, execute.Messages[0].Message);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_RemoveTime_InInRange_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, false);
            string message = "Date InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DateTimeValue, c => c.DateTimeStart, c => c.DateTimeEnd, true, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.AreEqual(false, result);
            Assert.AreEqual(true, execute.HasErro);
            Assert.AreEqual(message, execute.Messages[0].Message);
        }
        #endregion
    }
}
