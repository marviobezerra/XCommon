using FluentAssertions;
using System;
using XCommon.Extensions.Checks;
using XCommon.Test.Extensions.Checks.DataSource;
using XCommon.Util;
using Xunit;

namespace XCommon.Test.Extensions.Checks
{
    public class ValueCheckTest
    {
        [Theory(DisplayName = "Bigger Than DateTime")]
        [MemberData(nameof(ValueCheckDataSource.BiggerThanDateTimeDataSource), MemberType = typeof(ValueCheckDataSource))]
        public void BiggerThanDateTime(Pair<DateTime, DateTime> data, bool expected, string message)
        {
            bool result = data.Item1.BiggerThan(data.Item2);

            expected.Should().Be(result, message);
        }

        [Theory(DisplayName = "Bigger Than DateTime (Without time)")]
        [MemberData(nameof(ValueCheckDataSource.BiggerThanDateTimeNoTimeDataSource), MemberType = typeof(ValueCheckDataSource))]
        public void BiggerThanDateTimeNoTime(Pair<DateTime, DateTime> data, bool expected, string message)
        {
            bool result = data.Item1.BiggerThan(data.Item2, true);

            expected.Should().Be(result, message);
        }

        [Theory(DisplayName = "Bigger Than Int")]
        [MemberData(nameof(ValueCheckDataSource.BiggerThanIntDataSource), MemberType = typeof(ValueCheckDataSource))]
        public void BiggerThanInt(Pair<int, int> data, bool expected, string message)
        {
            bool result = data.Item1.BiggerThan(data.Item2);

            expected.Should().Be(result, message);
        }

        [Theory(DisplayName = "Bigger Than Decimal")]
        [MemberData(nameof(ValueCheckDataSource.BiggerThanDecimalDataSource), MemberType = typeof(ValueCheckDataSource))]
        public void BiggerThanDecimal(Pair<decimal, decimal> data, bool expected, string message)
        {
            bool result = data.Item1.BiggerThan(data.Item2);

            expected.Should().Be(result, message);
        }
        
        [Theory(DisplayName = "Less Than DateTime")]
        [MemberData(nameof(ValueCheckDataSource.LessThanDateTimeDataSource), MemberType = typeof(ValueCheckDataSource))]
        public void LessThanDateTime(Pair<DateTime, DateTime> data, bool expected, string message)
        {
            bool result = data.Item1.LessThan(data.Item2);

            expected.Should().Be(result, message);
        }

        [Theory(DisplayName = "Less Than DateTime (Without time)")]
        [MemberData(nameof(ValueCheckDataSource.LessThanDateTimeNoTimeDataSource), MemberType = typeof(ValueCheckDataSource))]
        public void LessThanDateTimeNoTime(Pair<DateTime, DateTime> data, bool expected, string message)
        {
            bool result = data.Item1.LessThan(data.Item2, true);

            expected.Should().Be(result, message);
        }

        [Theory(DisplayName = "Less Than Int")]
        [MemberData(nameof(ValueCheckDataSource.LessThanIntDataSource), MemberType = typeof(ValueCheckDataSource))]
        public void LessThanInt(Pair<int, int> data, bool expected, string message)
        {
            bool result = data.Item1.LessThan(data.Item2);

            expected.Should().Be(result, message);
        }

        [Theory(DisplayName = "Less Than Decimal")]
        [MemberData(nameof(ValueCheckDataSource.LessThanDecimalDataSource), MemberType = typeof(ValueCheckDataSource))]
        public void LessThanDecimal(Pair<decimal, decimal> data, bool expected, string message)
        {
            bool result = data.Item1.LessThan(data.Item2);

            expected.Should().Be(result, message);
        }





        [Theory(DisplayName = "In Range DateTime")]
        [MemberData(nameof(ValueCheckDataSource.InRangeDateTimeDataSource), MemberType = typeof(ValueCheckDataSource))]
        public void InRangeDateTime(Pair<DateTime, DateTime, DateTime> data, bool expected, string message)
        {
            bool result = data.Item1.InRange(data.Item2, data.Item3);

            expected.Should().Be(result, message);
        }

        [Theory(DisplayName = "In Range DateTime (Without time)")]
        [MemberData(nameof(ValueCheckDataSource.InRangeDateTimeNoTimeDataSource), MemberType = typeof(ValueCheckDataSource))]
        public void InRangeDateTimeNoTime(Pair<DateTime, DateTime, DateTime> data, bool expected, string message)
        {
            bool result = data.Item1.InRange(data.Item2, data.Item3, true);

            expected.Should().Be(result, message);
        }

        [Theory(DisplayName = "In Range Int")]
        [MemberData(nameof(ValueCheckDataSource.InRangeIntDataSource), MemberType = typeof(ValueCheckDataSource))]
        public void InRangeInt(Pair<int, int, int> data, bool expected, string message)
        {
            bool result = data.Item1.InRange(data.Item2, data.Item3);

            expected.Should().Be(result, message);
        }

        [Theory(DisplayName = "In Range Decimal")]
        [MemberData(nameof(ValueCheckDataSource.InRangeDecimalDataSource), MemberType = typeof(ValueCheckDataSource))]
        public void InRangeDecimal(Pair<decimal, decimal, decimal> data, bool expected, string message)
        {
            bool result = data.Item1.InRange(data.Item2, data.Item3);

            expected.Should().Be(result, message);
        }
    }
}
