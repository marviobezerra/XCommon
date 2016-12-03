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
                PairList<EntityA, bool, string> result = new PairList<EntityA, bool, string>();

                result.Add(new EntityA { }, true, "Default value");

                result.Add(new EntityA
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
                }, true, "Default value");

                result.Add(new EntityA
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
                }, true, "Default value");

                result.Add(new EntityA
                {
                    IntValueNullable = 100,
                    BoolValueNullAble = false,
                    DateTimeValueNullable = DateTime.Now.AddDays(1),
                    EnumValueNullable = BooleanOption.False,
                    GuidValueNullable = "105".ToGuid(),
                }, true, "Default value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> ComplexConverterDataSource
        {
            get
            {
                PairList<EntityD, bool, string> result = new PairList<EntityD, bool, string>();

                result.Add(new EntityD
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
                }, true, "Default value");

                result.Add(new EntityD
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
                }, true, "Default value");

                result.Add(new EntityD
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
                }, true, "Default value");

                return result.Cast();
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

                PairList<List<EntityA>, bool, string> result = new PairList<List<EntityA>, bool, string>();
                result.Add(dataItem, true, "Valid list");

                return result.Cast();
            }
        }
    }
}
