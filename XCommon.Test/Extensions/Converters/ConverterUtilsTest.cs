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
        public void ConverterToGuid(string source, Guid expected, string message)
        {
            Guid result = source.ToGuid();
            result.Should().Be(expected, message);
        }

        [Theory(DisplayName = "Converter ToEnum")]
        [MemberData(nameof(ConverterUtilsDataSource.EnumDataSource), MemberType = typeof(ConverterUtilsDataSource))]
        public void ConverterToEnum(string source, BooleanOption expected, string message)
        {
            BooleanOption result = source.ToEnum<BooleanOption>();
            result.Should().Be(expected, message);
        }

        [Theory(DisplayName = "Converter ToInt")]
        [MemberData(nameof(ConverterUtilsDataSource.IntDataSource), MemberType = typeof(ConverterUtilsDataSource))]
        public void ConverterToInt(string source, int expected, string message)
        {
            int result = source.ToInt32();
            result.Should().Be(expected, message);
        }

        [Theory(DisplayName = "Converter ToInt (With Default)")]
        [MemberData(nameof(ConverterUtilsDataSource.IntWithDefaultDataSource), MemberType = typeof(ConverterUtilsDataSource))]
        public void ConverterToIntWithDefault(string source, int defaultValue, int expected, string message)
        {
            int result = source.ToInt32(defaultValue);
            result.Should().Be(expected, message);
        }

        [Theory(DisplayName = "Converter ToBool")]
        [MemberData(nameof(ConverterUtilsDataSource.BooleanOptionDataSource), MemberType = typeof(ConverterUtilsDataSource))]
        public void ConverterToBool(BooleanOption source, bool expected, string message)
        {
            bool result = source.ToBool();
            result.Should().Be(expected, message);
        }

        [Theory(DisplayName = "Converter ToBool (With Default)")]
        [MemberData(nameof(ConverterUtilsDataSource.BooleanOptionWithDefaultDataSource), MemberType = typeof(ConverterUtilsDataSource))]
        public void ConverterToBoolWithDefault(BooleanOption source, bool defaultValue, bool expected, string message)
        {
            bool result = source.ToBool(defaultValue);
            result.Should().Be(expected, message);
        }
    }
}
