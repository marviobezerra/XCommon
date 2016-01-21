using System;
using XCommon.Patterns.Specification.Entity.Extensions;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Test.Patterns.Specification.Helper;
using Xunit;

namespace XCommon.Test.Patterns.Specification.Entity
{
    public class AndCheckValueTest
    {
        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(5, 6, false)]
        [InlineData(-1, 0, false)]
        [InlineData(-1, -1, false)]
        [InlineData(-10, -1, false)]
        [InlineData(1, 0, true)]
        [InlineData(10, 0, true)]
        [InlineData(-1, -2, true)]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan Int")]
        public void BiggerThan_Int_Without_Execute(int value, int compare, bool valid)
        {
            BiggerThanEntity<int> entity = new BiggerThanEntity<int>(value, compare);

            var spec = new SpecificationEntity<BiggerThanEntity<int>>()
                .AndIsBiggerThan(c => c.Value, c => c.Compare);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(0, 0.1, false)]
        [InlineData(0.001, 0.002, false)]
        [InlineData(-0.002, -0.002, false)]
        [InlineData(-1, 0, false)]
        [InlineData(-1, -1, false)]
        [InlineData(1, 0, true)]
        [InlineData(10, 9.999, true)]
        [InlineData(0.1, 0, true)]
        [InlineData(0.002, 0.001, true)]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan Decimal")]
        public void BiggerThan_Decimal_Without_Execute(decimal value, decimal compare, bool valid)
        {
            BiggerThanEntity<decimal> entity = new BiggerThanEntity<decimal>(value, compare);

            var spec = new SpecificationEntity<BiggerThanEntity<decimal>>()
                .AndIsBiggerThan(c => c.Value, c => c.Compare);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }

        [Theory]
        [InlineData("2015-01-01", "2015-01-01", false, false)]
        [InlineData("2015-01-01 00:00:00", "2015-01-01 00:00:01", false, false)]
        [InlineData("2015-01-01 00:00:01", "2015-01-01 00:00:00", false, true)]
        [InlineData("2015-01-02 00:00:00", "2015-01-01 00:00:00", true, false)]
        [Trait("Patterns Specification Entity AndCheckValue", "BiggerThan DateTime")]
        public void BiggerThan_DateTime_Without_Execute(DateTime value, DateTime compare, bool valid, bool removeTime)
        {
            BiggerThanEntity<DateTime> entity = new BiggerThanEntity<DateTime>(value, compare);

            var spec = new SpecificationEntity<BiggerThanEntity<DateTime>>()
                .AndIsBiggerThan(c => c.Value, c => c.Compare, true);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(6, 5, false)]
        [InlineData(0, -1, false)]
        [InlineData(-1, -1, false)]
        [InlineData(-1, -10, false)]
        [InlineData(0, 1, true)]
        [InlineData(0, 10, true)]
        [InlineData(-2, -1, true)]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan Int")]
        public void LessThan_Int_Without_Execute(int value, int compare, bool valid)
        {
            LessThanEntity<int> entity = new LessThanEntity<int>(value, compare);

            var spec = new SpecificationEntity<LessThanEntity<int>>()
                .AndIsLessThan(c => c.Value, c => c.Compare);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(0.1, 0, false)]
        [InlineData(0.002, 0.001, false)]
        [InlineData(-0.001, -0.001, false)]
        [InlineData(-0, -1, false)]
        [InlineData(-1, -1, false)]
        [InlineData(0, 1, true)]
        [InlineData(9.999, 100, true)]
        [InlineData(0, 0.1, true)]
        [InlineData(0.001, 0.002, true)]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan Decimal")]
        public void LessThan_Decimal_Without_Execute(decimal value, decimal compare, bool valid)
        {
            LessThanEntity<decimal> entity = new LessThanEntity<decimal>(value, compare);

            var spec = new SpecificationEntity<LessThanEntity<decimal>>()
                .AndIsLessThan(c => c.Value, c => c.Compare);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }

        [Theory]
        [InlineData("2015-01-01", "2015-01-01", false, false)]
        [InlineData("2015-01-01 00:00:01", "2015-01-01 00:00:00", false, false)]
        [InlineData("2015-01-01 00:00:00", "2015-01-01 00:00:01", false, true)]
        [InlineData("2015-01-01 00:00:00", "2015-01-01 00:00:01", true, false)]
        [Trait("Patterns Specification Entity AndCheckValue", "LessThan DateTime")]
        public void LessThan_DateTime_Without_Execute(DateTime value, DateTime compare, bool valid, bool removeTime)
        {
            LessThanEntity<DateTime> entity = new LessThanEntity<DateTime>(value, compare);

            var spec = new SpecificationEntity<LessThanEntity<DateTime>>()
                .AndIsLessThan(c => c.Value, c => c.Compare, removeTime);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }

        [Theory]
        [InlineData(1, 0, 0, false)]
        [InlineData(0, 1, 1, false)]
        [InlineData(2, -1, -10, false)]
        [InlineData(-2, -1, 0, false)]
        [InlineData(0, 0, 1, true)]
        [InlineData(-1, -1, 0, true)]
        [InlineData(-1, -1, -1, true)]
        [InlineData(1, 1, 1, true)]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange Int")]
        public void InRange_Int_Without_Execute(int value, int start, int end, bool valid)
        {
            InRangeEntity<int> entity = new InRangeEntity<int>(value, start, end);

            var spec = new SpecificationEntity<InRangeEntity<int>>()
                .AndIsInRange(c => c.Value, c => c.Start, c => c.End);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }

        [Theory]
        [InlineData(0.1, 0, 0, false)]
        [InlineData(0.001, 0, 0, false)]
        [InlineData(2, -1, -10, false)]
        [InlineData(-0.001, 0, 1, false)]
        [InlineData(0, 0, 1, true)]
        [InlineData(-0.001, -0.001, 0, true)]
        [InlineData(-1, -1, -1, true)]
        [InlineData(1, 1, 1, true)]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange Decimal")]
        public void InRange_Decimal_Without_Execute(decimal value, decimal start, decimal end, bool valid)
        {
            InRangeEntity<decimal> entity = new InRangeEntity<decimal>(value, start, end);

            var spec = new SpecificationEntity<InRangeEntity<decimal>>()
                .AndIsInRange(c => c.Value, c => c.Start, c => c.End);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }

        [Theory]        
        [InlineData("2015-01-01 00:00:01", "2015-01-01 00:00:00", "2015-01-01 00:00:00", false, false)]        
        [InlineData("2015-01-01 00:00:00", "2015-01-01 00:00:01", "2015-01-01 00:00:01", false, false)]
        [InlineData("2015-01-01 00:00:00", "2015-01-01 23:59:50", "2015-01-01 00:00:00", true, true)]
        [InlineData("2015-01-01", "2015-01-01", "2015-01-01", true, false)]
        [InlineData("2015-06-01", "2015-01-01", "2015-12-31", true, false)]
        [Trait("Patterns Specification Entity AndCheckValue", "InRange DateTime")]
        public void InRange_DateTime_Without_Execute(DateTime value, DateTime start, DateTime end, bool valid, bool removeTime)
        {
            InRangeEntity<DateTime> entity = new InRangeEntity<DateTime>(value, start, end);

            var spec = new SpecificationEntity<InRangeEntity<DateTime>>()
                .AndIsInRange(c => c.Value, c => c.Start, c => c.End, removeTime);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }
        
        [Theory]
        [InlineData(-10, -1, false)]
        [InlineData(1, 0, true)]
        [Trait("Patterns Specification Entity AndCheckValue", "Out of Box")]
        public void Int_BiggerThan_InValid_OutOfBox(int value, int compare, bool valid)
        {
            BiggerThanEntity<int> entity = new BiggerThanEntity<int>(0, 0);

            SpecificationEntity<BiggerThanEntity<int>> spec = new SpecificationEntity<BiggerThanEntity<int>>()
                .AndIsBiggerThan(c => value, c => compare);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }        
    }
}
