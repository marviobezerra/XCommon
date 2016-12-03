using FluentAssertions;
using System;
using XCommon.Extensions.Util;
using XCommon.Test.Extensions.UtilTest.DataSource;
using Xunit;

namespace XCommon.Test.Extensions.UtilTest
{
    public class ReflactionExtensionsTest
    {
        [Theory(DisplayName = "IsBasedIn")]
        [MemberData(nameof(ReflactionExtensionsDataSource.IsBasedInDataSource), MemberType = typeof(ReflactionExtensionsDataSource))]
        public void IsBasedInClassClass(Type contract, Type concrect, bool expected, string message)
        {
            concrect.IsBasedIn(contract).Should().Be(expected, message);
        }

        [Theory(DisplayName = "GetClassName")]
        [MemberData(nameof(ReflactionExtensionsDataSource.GetClassNameDataSource), MemberType = typeof(ReflactionExtensionsDataSource))]
        public void GetClassName(Type contract, string expected)
        {
            contract.GetClassName().Should().Be(expected);
        }

        [Theory(DisplayName = "GetClassName")]
        [MemberData(nameof(ReflactionExtensionsDataSource.GetClassNameDataSourceNoPrimitives), MemberType = typeof(ReflactionExtensionsDataSource))]
        public void GetClassNameNoPrimitive(Type contract, string expected)
        {
            contract.GetClassName(false).Should().Be(expected);
        }
    }
}
