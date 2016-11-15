using FluentAssertions;
using System;
using XCommon.Extensions.Converters;
using XCommon.Test.Extensions.Converters.DataSource;
using XCommon.Util;
using Xunit;

namespace XCommon.Test.Extensions.Converters
{
    public class ConverterUtilsTest
    {
        [Theory(DisplayName = "Converter ToGuid")]
        [MemberData(nameof(ConverterUtilsDataSource.GuidDataSource), MemberType = typeof(ConverterUtilsDataSource))]
        public void ConverterToGuid(Pair<string, Guid> data, bool valid, string message)
        {
            Guid result = data.Item1.ToGuid();
            result.Should().Be(data.Item2, message);
        }

        [Theory(DisplayName = "Converter ToEnum")]
        [MemberData(nameof(ConverterUtilsDataSource.EnumDataSource), MemberType = typeof(ConverterUtilsDataSource))]
        public void ConverterToEnum(Pair<string, BooleanOption> data, bool valid, string message)
        {
            BooleanOption result = data.Item1.ToEnum<BooleanOption>();
            result.Should().Be(data.Item2, message);
        }

        [Theory(DisplayName = "Converter ToInt")]
        [MemberData(nameof(ConverterUtilsDataSource.IntDataSource), MemberType = typeof(ConverterUtilsDataSource))]
        public void ConverterToInt(Pair<string, int> data, bool valid, string message)
        {
            int result = data.Item1.ToInt32();
            result.Should().Be(data.Item2, message);
        }

        [Theory(DisplayName = "Converter ToInt (With Default)")]
        [MemberData(nameof(ConverterUtilsDataSource.IntWithDefaultDataSource), MemberType = typeof(ConverterUtilsDataSource))]
        public void ConverterToIntWithDefault(Pair<string, int, int> data, bool valid, string message)
        {
            int result = data.Item1.ToInt32(data.Item3);
            result.Should().Be(data.Item2, message);
        }

        [Theory(DisplayName = "Converter ToBool")]
        [MemberData(nameof(ConverterUtilsDataSource.BooleanOptionDataSource), MemberType = typeof(ConverterUtilsDataSource))]
        public void ConverterToBool(Pair<BooleanOption, bool> data, bool valid, string message)
        {
            bool result = data.Item1.ToBool();
            result.Should().Be(data.Item2, message);
        }

        [Theory(DisplayName = "Converter ToBool (With Default)")]
        [MemberData(nameof(ConverterUtilsDataSource.BooleanOptionWithDefaultDataSource), MemberType = typeof(ConverterUtilsDataSource))]
        public void ConverterToBoolWithDefault(Pair<BooleanOption, bool, bool> data, bool valid, string message)
        {
            bool result = data.Item1.ToBool(data.Item3);
            result.Should().Be(data.Item2, message);
        }
    }
}
