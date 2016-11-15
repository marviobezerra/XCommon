using System;
using System.Collections.Generic;
using XCommon.Extensions.Converters;
using XCommon.Test.Extensions.Converters.Sample;
using XCommon.UnitTest;
using XCommon.Util;

namespace XCommon.Test.Extensions.Converters.DataSource
{
    public static class ConverterDataSource
    {
        public static IEnumerable<object[]> SimpleConverterDataSource
        {
            get
            {
                List<DataItem<EntityA>> result = new List<DataItem<EntityA>>();

                result.Add(new DataItem<EntityA>(new EntityA { }, true, "Default value"));

                result.Add(new DataItem<EntityA>(new EntityA
                {
                    IntValue = 17,
                    IntValueNullable = 17,
                    BoolValue = true,
                    BoolValueNullAble = true,
                    DateTimeValue = DateTime.Now,
                    DateTimeValueNullable = DateTime.Now,
                    EnumValue = Util.BooleanOption.True,
                    EnumValueNullable = BooleanOption.True,
                    GuidValue = "5".ToGuid(),
                    GuidValueNullable = "5".ToGuid(),
                    StringValue = "Marvio"
                }, true, "Default value"));

                result.Add(new DataItem<EntityA>(new EntityA
                {
                    IntValue = 100,
                    IntValueNullable = null,
                    BoolValue = false,
                    BoolValueNullAble = null,
                    DateTimeValue = DateTime.Now.AddDays(-1),
                    DateTimeValueNullable = null,
                    EnumValue = Util.BooleanOption.All,
                    EnumValueNullable = null,
                    GuidValue = "105".ToGuid(),
                    GuidValueNullable = null,
                    StringValue = "Marvio"
                }, true, "Default value"));

                result.Add(new DataItem<EntityA>(new EntityA
                {
                    IntValueNullable = 100,
                    BoolValueNullAble = false,
                    DateTimeValueNullable = DateTime.Now.AddDays(1),
                    EnumValueNullable = BooleanOption.False,
                    GuidValueNullable = "105".ToGuid(),
                }, true, "Default value"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> ComplexConverterDataSource
        {
            get
            {
                List<DataItem<EntityD>> result = new List<DataItem<EntityD>>();

                result.Add(new DataItem<EntityD>(new EntityD
                {
                    Id = 1,
                    EntityA = new EntityA
                    {
                        IntValue = 17,
                        IntValueNullable = 17,
                        BoolValue = true,
                        BoolValueNullAble = true,
                        DateTimeValue = DateTime.Now,
                        DateTimeValueNullable = DateTime.Now,
                        EnumValue = Util.BooleanOption.True,
                        EnumValueNullable = BooleanOption.True,
                        GuidValue = "5".ToGuid(),
                        GuidValueNullable = "5".ToGuid(),
                        StringValue = "Marvio"
                    }
                }, true, "Default value"));

                result.Add(new DataItem<EntityD>(new EntityD
                {
                    Id = 2,
                    EntityA = new EntityA
                    {
                        IntValue = 100,
                        IntValueNullable = null,
                        BoolValue = false,
                        BoolValueNullAble = null,
                        DateTimeValue = DateTime.Now.AddDays(-1),
                        DateTimeValueNullable = null,
                        EnumValue = Util.BooleanOption.All,
                        EnumValueNullable = null,
                        GuidValue = "105".ToGuid(),
                        GuidValueNullable = null,
                        StringValue = "Marvio"
                    }
                }, true, "Default value"));

                result.Add(new DataItem<EntityD>(new EntityD
                {
                    Id = 3,
                    EntityA = new EntityA
                    {
                        IntValueNullable = 100,
                        BoolValueNullAble = false,
                        DateTimeValueNullable = DateTime.Now.AddDays(1),
                        EnumValueNullable = BooleanOption.False,
                        GuidValueNullable = "105".ToGuid(),
                    }
                }, true, "Default value"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> ListConverterDataSource
        {
            get
            {
                List<EntityA> dataItem = new List<EntityA>();

                dataItem.Add(new EntityA
                {
                    IntValue = 17,
                    IntValueNullable = 17,
                    BoolValue = true,
                    BoolValueNullAble = true,
                    DateTimeValue = DateTime.Now,
                    DateTimeValueNullable = DateTime.Now,
                    EnumValue = Util.BooleanOption.True,
                    EnumValueNullable = BooleanOption.True,
                    GuidValue = "5".ToGuid(),
                    GuidValueNullable = "5".ToGuid(),
                    StringValue = "Marvio"
                });

                dataItem.Add(new EntityA
                {
                    IntValue = 100,
                    IntValueNullable = null,
                    BoolValue = false,
                    BoolValueNullAble = null,
                    DateTimeValue = DateTime.Now.AddDays(-1),
                    DateTimeValueNullable = null,
                    EnumValue = Util.BooleanOption.All,
                    EnumValueNullable = null,
                    GuidValue = "105".ToGuid(),
                    GuidValueNullable = null,
                    StringValue = "Marvio"
                });

                dataItem.Add(new EntityA
                {
                    IntValueNullable = 100,
                    BoolValueNullAble = false,
                    DateTimeValueNullable = DateTime.Now.AddDays(1),
                    EnumValueNullable = BooleanOption.False,
                    GuidValueNullable = "105".ToGuid(),
                });

                List<DataItem<List<EntityA>>> result = new List<DataItem<List<EntityA>>>();
                result.Add(new DataItem<List<EntityA>>(dataItem, true, "Valid list"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }
    }
}
