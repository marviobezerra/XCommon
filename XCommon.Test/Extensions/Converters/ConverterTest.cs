using FluentAssertions;
using XCommon.Extensions.Converters;
using XCommon.Test.Extensions.Converters.DataSource;
using XCommon.Test.Extensions.Converters.Sample;
using XCommon.Util;
using Xunit;

namespace XCommon.Test.Extensions.Converters
{
    public class ConverterTest
    {
        [Theory(DisplayName = "Type converter")]
        [MemberData(nameof(ConverterDataSource.PersonConverterDataSource), MemberType = typeof(ConverterDataSource))]
        public void TypeConverter(Pair<PersonAEntity, PersonBEntity> data, bool valid, string message)
        {
            var result = data.Item1.Convert<PersonBEntity>();

            data.Item1.IdPerson.Should().Be(result.IdPerson);
            data.Item1.IdPersonNullable.Should().Be(result.IdPersonNullable);

            data.Item1.Age.Should().Be(result.Age);
            data.Item1.AgeNullable.Should().Be(result.AgeNullable);

            data.Item1.Name.Should().Be(result.Name);
            data.Item1.Email.Should().Be(result.Email);
        }
    }
}
