﻿using FluentAssertions;
using XCommon.Extensions.String;
using XCommon.Test.Extensions.String.DataSource;
using XCommon.Util;
using Xunit;

namespace XCommon.Test.Extensions.String
{
    public class StringExtensionsTest
    {
        [Theory(DisplayName = "IsEmpty")]
        [MemberData(nameof(StringExtensionsDataSource.IsEmptyDataSource), MemberType = typeof(StringExtensionsDataSource))]
        public void IsEmpty(string data, bool valid, string message)
        {
            bool result = data.IsEmpty();
            result.Should().Be(valid, message);
        }

        [Theory(DisplayName = "IsNotEmpty")]
        [MemberData(nameof(StringExtensionsDataSource.IsNotEmptyDataSource), MemberType = typeof(StringExtensionsDataSource))]
        public void IsNotEmpty(string data, bool valid, string message)
        {
            bool result = data.IsNotEmpty();
            result.Should().Be(valid, message);
        }

        [Theory(DisplayName = "RemoveAccent")]
        [MemberData(nameof(StringExtensionsDataSource.RemoveAccentDataSource), MemberType = typeof(StringExtensionsDataSource))]
        public void RemoveAccent(Pair<string, string> data, bool valid, string message)
        {
            var result = data.Item1.RemoveAcent();
            result.Should().Be(data.Item2, message);
        }
    }
}