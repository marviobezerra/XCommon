using FluentAssertions;
using System.Collections.Generic;
using XCommon.Extensions.Converters;
using XCommon.Test.Extensions.Converters.DataSource;
using XCommon.Test.Extensions.Converters.Sample;
using Xunit;

namespace XCommon.Test.Extensions.Converters
{
    public class ConverterTest
    {
        [Fact(DisplayName = "Type converter (Null Value)")]
		[Trait("Extensions", "Converters")]
		public void TypeConverterNullValue()
        {
            EntityA personA = null;
            var personB = personA.Convert<EntityB>();

            personB.Should().BeNull();
        }

        [Theory(DisplayName = "Type converter (Simple entity)")]
		[Trait("Extensions", "Converters")]
		[MemberData(nameof(ConverterDataSource.SimpleConverterDataSource), MemberType = typeof(ConverterDataSource))]
        public void TypeConverterSimpleEntity(EntityA data, string message)
        {
            var result = data.Convert<EntityB>();

            result.GuidValue.Should().Be(data.GuidValue, message);
            result.GuidValueNullable.Should().Be(data.GuidValueNullable, message);

            result.IntValue.Should().Be(data.IntValue, message);
            result.IntValueNullable.Should().Be(data.IntValueNullable, message);

            result.StringValue.Should().Be(data.StringValue, message);

            result.BoolValue.Should().Be(data.BoolValue, message);
            result.BoolValueNullAble.Should().Be(data.BoolValueNullAble, message);

            result.EnumValue.Should().Be(data.EnumValue, message);
            result.EnumValueNullable.Should().Be(data.EnumValueNullable, message);

            result.DateTimeValue.Should().Be(data.DateTimeValue, message);
            result.DateTimeValueNullable.Should().Be(data.DateTimeValueNullable, message);
        }


        [Theory(DisplayName = "Type converter (Simple entity with ignore)")]
		[Trait("Extensions", "Converters")]
		[MemberData(nameof(ConverterDataSource.SimpleConverterDataSource), MemberType = typeof(ConverterDataSource))]
        public void TypeConverterSimpleEntityWithIgnore(EntityA data, string message)
        {
            var result = data.Convert<EntityB>(nameof(data.BoolValueNullAble), nameof(data.DateTimeValueNullable), nameof(data.IntValue));

            result.GuidValue.Should().Be(data.GuidValue, message);
            result.GuidValueNullable.Should().Be(data.GuidValueNullable, message);

            result.IntValue.Should().Be(default(int), message);
            result.IntValueNullable.Should().Be(data.IntValueNullable, message);

            result.StringValue.Should().Be(data.StringValue, message);

            result.BoolValue.Should().Be(data.BoolValue, message);
            result.BoolValueNullAble.Should().Be(null, message);

            result.EnumValue.Should().Be(data.EnumValue, message);
            result.EnumValueNullable.Should().Be(data.EnumValueNullable, message);

            result.DateTimeValue.Should().Be(data.DateTimeValue, message);
            result.DateTimeValueNullable.Should().Be(null, message);
        }
		
        [Theory(DisplayName = "Type converter (Simple heritage entity)")]
		[Trait("Extensions", "Converters")]
		[MemberData(nameof(ConverterDataSource.SimpleConverterDataSource), MemberType = typeof(ConverterDataSource))]
        public void TypeConverterHeritageEntity(EntityA data, string message)
        {
            var result = data.Convert<EntityC>();

            result.GuidValue.Should().Be(data.GuidValue, message);
            result.GuidValueNullable.Should().Be(data.GuidValueNullable, message);

            result.IntValue.Should().Be(data.IntValue, message);
            result.IntValueNullable.Should().Be(data.IntValueNullable, message);

            result.StringValue.Should().Be(data.StringValue, message);

            result.BoolValue.Should().Be(data.BoolValue, message);
            result.BoolValueNullAble.Should().Be(data.BoolValueNullAble, message);

            result.EnumValue.Should().Be(data.EnumValue, message);
            result.EnumValueNullable.Should().Be(data.EnumValueNullable, message);

            result.DateTimeValue.Should().Be(data.DateTimeValue, message);
            result.DateTimeValueNullable.Should().Be(data.DateTimeValueNullable, message);
        }

        [Theory(DisplayName = "Type converter (Complex entity)")]
		[Trait("Extensions", "Converters")]
		[MemberData(nameof(ConverterDataSource.ComplexConverterDataSource), MemberType = typeof(ConverterDataSource))]
        public void TypeConverterComplexEntity(EntityD data, string message)
        {
            var result = data.Convert<EntityE>();

            result.Id.Should().Be(data.Id, message);

            result.EntityA.GuidValue.Should().Be(data.EntityA.GuidValue, message);
            result.EntityA.GuidValueNullable.Should().Be(data.EntityA.GuidValueNullable, message);

            result.EntityA.IntValue.Should().Be(data.EntityA.IntValue, message);
            result.EntityA.IntValueNullable.Should().Be(data.EntityA.IntValueNullable, message);

            result.EntityA.StringValue.Should().Be(data.EntityA.StringValue, message);

            result.EntityA.BoolValue.Should().Be(data.EntityA.BoolValue, message);
            result.EntityA.BoolValueNullAble.Should().Be(data.EntityA.BoolValueNullAble, message);

            result.EntityA.EnumValue.Should().Be(data.EntityA.EnumValue, message);
            result.EntityA.EnumValueNullable.Should().Be(data.EntityA.EnumValueNullable, message);

            result.EntityA.DateTimeValue.Should().Be(data.EntityA.DateTimeValue, message);
            result.EntityA.DateTimeValueNullable.Should().Be(data.EntityA.DateTimeValueNullable, message);
        }

        [Theory(DisplayName = "Type converter (Complex entity With Ignore)")]
		[Trait("Extensions", "Converters")]
		[MemberData(nameof(ConverterDataSource.ComplexConverterDataSource), MemberType = typeof(ConverterDataSource))]
        public void TypeConverterComplexEntityWithIgnore(EntityD data, string message)
        {
            var result = data.Convert<EntityE>(nameof(data.EntityA));

            result.Id.Should().Be(data.Id, message);
            result.EntityA.Should().Be(null, message);
        }

        [Theory(DisplayName = "Type converter (List)")]
		[Trait("Extensions", "Converters")]
		[MemberData(nameof(ConverterDataSource.ListConverterDataSource), MemberType = typeof(ConverterDataSource))]
        public void TypeConverterList(List<EntityA> data, string message)
        {
            var result = data.Convert<EntityB, EntityA>();

            result.Count.Should().Be(data.Count);

            for (var i = 0; i < result.Count; i++)
            {
                result[i].GuidValue.Should().Be(data[i].GuidValue, message);
                result[i].GuidValueNullable.Should().Be(data[i].GuidValueNullable, message);

                result[i].IntValue.Should().Be(data[i].IntValue, message);
                result[i].IntValueNullable.Should().Be(data[i].IntValueNullable, message);

                result[i].StringValue.Should().Be(data[i].StringValue, message);

                result[i].BoolValue.Should().Be(data[i].BoolValue, message);
                result[i].BoolValueNullAble.Should().Be(data[i].BoolValueNullAble, message);

                result[i].EnumValue.Should().Be(data[i].EnumValue, message);
                result[i].EnumValueNullable.Should().Be(data[i].EnumValueNullable, message);

                result[i].DateTimeValue.Should().Be(data[i].DateTimeValue, message);
                result[i].DateTimeValueNullable.Should().Be(data[i].DateTimeValueNullable, message);
            }
        }
    }
}
