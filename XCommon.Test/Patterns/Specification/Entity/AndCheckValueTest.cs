using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Test.Patterns.Specification.Helper;
using Xunit;

namespace XCommon.Test.Patterns.Specification.Entity
{
    public class AndCheckValueTest
    {
        #region BiggerThan
        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan")]
        public void Patterns_Specification_Entity_AndCheckValue_Int_BiggerThan_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.IntValue, c => c.IntStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan")]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_BiggerThan_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DecimalValue, c => c.DecimalStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_BiggerThan_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DateTimeValue, c => c.DateTimeStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_BiggerThan_Valid_RemoveTime_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DateTimeValue, c => c.DateTimeStart, true);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan")]
        public void Patterns_Specification_Entity_AndCheckValue_Int_BiggerThan_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.IntValue, c => c.IntStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan")]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_BiggerThan_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DecimalValue, c => c.DecimalStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_BiggerThan_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DateTimeValue, c => c.DateTimeStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_BiggerThan_InValid_RemoveTime_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DateTimeValue, c => c.DateTimeStart, true);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan")]
        public void Patterns_Specification_Entity_AndCheckValue_Int_BiggerThan_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);
            string message = "Int Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.IntValue, c => c.IntStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(true, result);
            Assert.Equal(false, execute.HasErro);
            Assert.Equal(0, execute.Messages.Count);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan")]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_BiggerThan_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);
            string message = "Decimal Valid";


            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DecimalValue, c => c.DecimalStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(true, result);
            Assert.Equal(false, execute.HasErro);
            Assert.Equal(0, execute.Messages.Count);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_BiggerThan_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);
            string message = "DateTime Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DateTimeValue, c => c.DateTimeStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(true, result);
            Assert.Equal(false, execute.HasErro);
            Assert.Equal(0, execute.Messages.Count);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_BiggerThan_Valid_RemoveTime_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);
            string message = "DateTime Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DateTimeValue, c => c.DateTimeStart, true, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(true, result);
            Assert.Equal(false, execute.HasErro);
            Assert.Equal(0, execute.Messages.Count);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan")]
        public void Patterns_Specification_Entity_AndCheckValue_Int_BiggerThan_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, false);
            string message = "Int InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.IntValue, c => c.IntStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(false, result);
            Assert.Equal(true, execute.HasErro);
            Assert.Equal(message, execute.Messages[0].Message);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan")]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_BiggerThan_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, false);
            string message = "Decimal InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DecimalValue, c => c.DecimalStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(false, result);
            Assert.Equal(true, execute.HasErro);
            Assert.Equal(message, execute.Messages[0].Message);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_BiggerThan_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, false);
            string message = "DateTime InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DateTimeValue, c => c.DateTimeStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(false, result);
            Assert.Equal(true, execute.HasErro);
            Assert.Equal(message, execute.Messages[0].Message);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_BiggerThan_InValid_RemoveTime_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, false);
            string message = "Date InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => c.DateTimeValue, c => c.DateTimeStart, true, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(false, result);
            Assert.Equal(true, execute.HasErro);
            Assert.Equal(message, execute.Messages[0].Message);
        }
        #endregion

        #region LessThan
        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan")]
        public void Patterns_Specification_Entity_AndCheckValue_Int_LessThan_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.IntValue, c => c.IntStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan")]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_LessThan_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DecimalValue, c => c.DecimalStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_LessThan_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DateTimeValue, c => c.DateTimeStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_RemoveTime_LessThan_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DateTimeValue, c => c.DateTimeStart, true);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan")]
        public void Patterns_Specification_Entity_AndCheckValue_Int_LessThan_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.IntValue, c => c.IntStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan")]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_LessThan_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DecimalValue, c => c.DecimalStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_LessThan_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DateTimeValue, c => c.DateTimeStart);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_RemoveTime_InLessThan_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DateTimeValue, c => c.DateTimeStart, true);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan")]
        public void Patterns_Specification_Entity_AndCheckValue_Int_LessThan_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, true);
            string message = "Int Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.IntValue, c => c.IntStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(true, result);
            Assert.Equal(false, execute.HasErro);
            Assert.Equal(0, execute.Messages.Count);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan")]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_LessThan_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, true);
            string message = "Decimal Valid";


            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DecimalValue, c => c.DecimalStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(true, result);
            Assert.Equal(false, execute.HasErro);
            Assert.Equal(0, execute.Messages.Count);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_LessThan_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, true);
            string message = "DateTime Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DateTimeValue, c => c.DateTimeStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(true, result);
            Assert.Equal(false, execute.HasErro);
            Assert.Equal(0, execute.Messages.Count);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_RemoveTime_LessThan_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, true);
            string message = "DateTime Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DateTimeValue, c => c.DateTimeStart, true, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(true, result);
            Assert.Equal(false, execute.HasErro);
            Assert.Equal(0, execute.Messages.Count);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan")]
        public void Patterns_Specification_Entity_AndCheckValue_Int_LessThan_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, false);
            string message = "Int InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.IntValue, c => c.IntStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(false, result);
            Assert.Equal(true, execute.HasErro);
            Assert.Equal(message, execute.Messages[0].Message);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan")]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_LessThan_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, false);
            string message = "Decimal InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DecimalValue, c => c.DecimalStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(false, result);
            Assert.Equal(true, execute.HasErro);
            Assert.Equal(message, execute.Messages[0].Message);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_LessThan_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, false);
            string message = "DateTime InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DateTimeValue, c => c.DateTimeStart, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(false, result);
            Assert.Equal(true, execute.HasErro);
            Assert.Equal(message, execute.Messages[0].Message);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_RemoveTime_InLessThan_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.LessThan, false);
            string message = "Date InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => c.DateTimeValue, c => c.DateTimeStart, true, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(false, result);
            Assert.Equal(true, execute.HasErro);
            Assert.Equal(message, execute.Messages[0].Message);
        }
        #endregion

        #region InRange
        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange")]
        public void Patterns_Specification_Entity_AndCheckValue_Int_InRange_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.IntValue, c => c.IntStart, c => c.IntEnd);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange")]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_InRange_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DecimalValue, c => c.DecimalStart, c => c.DecimalEnd);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_InRange_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DateTimeValue, c => c.DateTimeStart, c => c.DateTimeEnd);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_RemoveTime_InRange_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DateTimeValue, c => c.DateTimeStart, c => c.DateTimeEnd, true);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange")]
        public void Patterns_Specification_Entity_AndCheckValue_Int_InRange_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.IntValue, c => c.IntStart, c => c.IntEnd);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange")]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_InRange_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DecimalValue, c => c.DecimalStart, c => c.DecimalEnd);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_InRange_InValid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DateTimeValue, c => c.DateTimeStart, c => c.DateTimeEnd);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_RemoveTime_InInRange_Valid_Without_Execute()
        {
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, false);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DateTimeValue, c => c.DateTimeStart, c => c.DateTimeEnd, true);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange")]
        public void Patterns_Specification_Entity_AndCheckValue_Int_InRange_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, true);
            string message = "Int Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.IntValue, c => c.IntStart, c => c.IntEnd, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(true, result);
            Assert.Equal(false, execute.HasErro);
            Assert.Equal(0, execute.Messages.Count);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange")]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_InRange_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, true);
            string message = "Decimal Valid";


            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DecimalValue, c => c.DecimalStart, c => c.DecimalEnd, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(true, result);
            Assert.Equal(false, execute.HasErro);
            Assert.Equal(0, execute.Messages.Count);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_InRange_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, true);
            string message = "DateTime Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DateTimeValue, c => c.DateTimeStart, c => c.DateTimeEnd, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(true, result);
            Assert.Equal(false, execute.HasErro);
            Assert.Equal(0, execute.Messages.Count);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_RemoveTime_InRange_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, true);
            string message = "DateTime Valid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DateTimeValue, c => c.DateTimeStart, c => c.DateTimeEnd, true, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(true, result);
            Assert.Equal(false, execute.HasErro);
            Assert.Equal(0, execute.Messages.Count);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange")]
        public void Patterns_Specification_Entity_AndCheckValue_Int_InRange_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, false);
            string message = "Int InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.IntValue, c => c.IntStart, c => c.IntEnd, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(false, result);
            Assert.Equal(true, execute.HasErro);
            Assert.Equal(message, execute.Messages[0].Message);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange")]
        public void Patterns_Specification_Entity_AndCheckValue_Decimal_InRange_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, false);
            string message = "Decimal InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DecimalValue, c => c.DecimalStart, c => c.DecimalEnd, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(false, result);
            Assert.Equal(true, execute.HasErro);
            Assert.Equal(message, execute.Messages[0].Message);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_InRange_InValid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, false);
            string message = "DateTime InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DateTimeValue, c => c.DateTimeStart, c => c.DateTimeEnd, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(false, result);
            Assert.Equal(true, execute.HasErro);
            Assert.Equal(message, execute.Messages[0].Message);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange")]
        public void Patterns_Specification_Entity_AndCheckValue_DateTime_RemoveTime_InInRange_Valid_With_Execute()
        {
            Execute execute = new Execute();
            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.InRange, false);
            string message = "Date InValid";

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsInRange(c => c.DateTimeValue, c => c.DateTimeStart, c => c.DateTimeEnd, true, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(false, result);
            Assert.Equal(true, execute.HasErro);
            Assert.Equal(message, execute.Messages[0].Message);
        }
        #endregion

        #region Out of Box
        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "Out of Box")]
        public void Patterns_Specification_Entity_AndCheckValue_Int_BiggerThan_Valid_OutOfBox()
        {
            int value = 5;
            int max = 4;

            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => value, c => max);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "Out of Box")]
        public void Patterns_Specification_Entity_AndCheckValue_Int_BiggerThan_InValid_OutOfBox()
        {
            int value = 4;
            int max = 5;

            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsBiggerThan(c => value, c => max);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "Out of Box")]
        public void Patterns_Specification_Entity_AndCheckValue_Int_AndIsLessThan_Valid_OutOfBox()
        {
            int value = 9;
            int max = 10;

            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => value, c => max);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("Patterns Specification Entity AndCheckValue", "Out of Box")]
        public void Patterns_Specification_Entity_AndCheckValue_Int_AndIsLessThan_InValid_OutOfBox()
        {
            int value = 11;
            int max = 10;

            AndCheckValueEntity entity = new AndCheckValueEntity(AndCheckCompareType.BiggerThan, true);

            SpecificationEntity<AndCheckValueEntity> spec = new SpecificationEntity<AndCheckValueEntity>()
                .AndIsLessThan(c => value, c => max);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(false, result);
        }
        #endregion
    }
}
