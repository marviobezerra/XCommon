using System;
using System.Collections.Generic;
using XCommon.UnitTest;
using XCommon.Util;

namespace XCommon.Test.Extensions.Converters.DataSource
{
    public static class ConverterUtilsDataSource
    {
        public static IEnumerable<object[]> BooleanOptionDataSource
        {
            get
            {
                List<DataItem<Pair<BooleanOption, bool>>> result = new List<DataItem<Pair<BooleanOption, bool>>>();

                result.Add(new DataItem<Pair<BooleanOption, bool>>(new Pair<BooleanOption, bool>(BooleanOption.True, true), true, "BooleanOption.True = True"));
                result.Add(new DataItem<Pair<BooleanOption, bool>>(new Pair<BooleanOption, bool>(BooleanOption.False, false), true, "BooleanOption.False = False"));
                result.Add(new DataItem<Pair<BooleanOption, bool>>(new Pair<BooleanOption, bool>(BooleanOption.All, true), true, "BooleanOption.All = True"));
                result.Add(new DataItem<Pair<BooleanOption, bool>>(new Pair<BooleanOption, bool>(default(BooleanOption), true), true, "default(BooleanOption) = True"));
                
                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> BooleanOptionWithDefaultDataSource
        {
            get
            {
                List<DataItem<Pair<BooleanOption, bool, bool>>> result = new List<DataItem<Pair<BooleanOption, bool, bool>>>();

                result.Add(new DataItem<Pair<BooleanOption, bool, bool>>(new Pair<BooleanOption, bool, bool>(BooleanOption.True, true, false), true, "BooleanOption.True = True"));
                result.Add(new DataItem<Pair<BooleanOption, bool, bool>>(new Pair<BooleanOption, bool, bool>(BooleanOption.False, false, false), true, "BooleanOption.False = False"));
                result.Add(new DataItem<Pair<BooleanOption, bool, bool>>(new Pair<BooleanOption, bool, bool>(BooleanOption.All, false, false), true, "BooleanOption.All = False"));
                result.Add(new DataItem<Pair<BooleanOption, bool, bool>>(new Pair<BooleanOption, bool, bool>(default(BooleanOption), false, false), true, "default(BooleanOption) = True"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> IntDataSource
        {
            get
            {
                List<DataItem<Pair<string, int>>> result = new List<DataItem<Pair<string, int>>>();

                result.Add(new DataItem<Pair<string, int>>(new Pair<string, int>(null, 0), true, "Empty Int"));
                result.Add(new DataItem<Pair<string, int>>(new Pair<string, int>(string.Empty, 0), true, "Empty Int"));
                result.Add(new DataItem<Pair<string, int>>(new Pair<string, int>("", 0), true, "Empty Int"));
                result.Add(new DataItem<Pair<string, int>>(new Pair<string, int>("    ", 0), true, "Empty Int"));
                result.Add(new DataItem<Pair<string, int>>(new Pair<string, int>("AbCd", 0), true, "Invalid Int"));
                result.Add(new DataItem<Pair<string, int>>(new Pair<string, int>("100.1", 0), true, "Invalid Int"));

                result.Add(new DataItem<Pair<string, int>>(new Pair<string, int>("1", 1), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int>>(new Pair<string, int>("-1", -1), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int>>(new Pair<string, int>("10", 10), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int>>(new Pair<string, int>("100", 100), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int>>(new Pair<string, int>("1000", 1000), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int>>(new Pair<string, int>("10000", 10000), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int>>(new Pair<string, int>("100000", 100000), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int>>(new Pair<string, int>("1000000", 1000000), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int>>(new Pair<string, int>("10000000", 10000000), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int>>(new Pair<string, int>("100000000", 100000000), true, "Valid Int"));
                
                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> IntWithDefaultDataSource
        {
            get
            {
                List<DataItem<Pair<string, int, int>>> result = new List<DataItem<Pair<string, int, int>>>();

                result.Add(new DataItem<Pair<string, int, int>>(new Pair<string, int, int>(null, 9, 9), true, "Empty Int"));
                result.Add(new DataItem<Pair<string, int, int>>(new Pair<string, int, int>(string.Empty, 90, 90), true, "Empty Int"));
                result.Add(new DataItem<Pair<string, int, int>>(new Pair<string, int, int>("", -1, -1), true, "Empty Int"));
                result.Add(new DataItem<Pair<string, int, int>>(new Pair<string, int, int>("    ", -7, -7), true, "Empty Int"));
                result.Add(new DataItem<Pair<string, int, int>>(new Pair<string, int, int>("AbCd", 999, 999), true, "Invalid Int"));
                result.Add(new DataItem<Pair<string, int, int>>(new Pair<string, int, int>("100.1", 80, 80), true, "Invalid Int"));
                                                                                    
                result.Add(new DataItem<Pair<string, int, int>>(new Pair<string, int, int>("1", 1, 100), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int, int>>(new Pair<string, int, int>("-1", -1, 100), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int, int>>(new Pair<string, int, int>("10", 10, 100), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int, int>>(new Pair<string, int, int>("100", 100, 100), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int, int>>(new Pair<string, int, int>("1000", 1000, 100), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int, int>>(new Pair<string, int, int>("10000", 10000, 100), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int, int>>(new Pair<string, int, int>("100000", 100000, 100), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int, int>>(new Pair<string, int, int>("1000000", 1000000, 100), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int, int>>(new Pair<string, int, int>("10000000", 10000000, 100), true, "Valid Int"));
                result.Add(new DataItem<Pair<string, int, int>>(new Pair<string, int, int>("100000000", 100000000, 100), true, "Valid Int"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> GuidDataSource
        {
            get
            {
                List<DataItem<Pair<string, Guid>>> result = new List<DataItem<Pair<string, Guid>>>();

                result.Add(new DataItem<Pair<string, Guid>>(new Pair<string, Guid>(null, Guid.Empty), true, "Empty Guid"));
                result.Add(new DataItem<Pair<string, Guid>>(new Pair<string, Guid>(string.Empty, Guid.Empty), true, "Empty Guid"));
                result.Add(new DataItem<Pair<string, Guid>>(new Pair<string, Guid>("", Guid.Empty), true, "Empty Guid"));
                result.Add(new DataItem<Pair<string, Guid>>(new Pair<string, Guid>("  ", Guid.Empty), true, "Empty Guid"));
                result.Add(new DataItem<Pair<string, Guid>>(new Pair<string, Guid>("1", Guid.Parse("00000000-0000-0000-0000-000000000001")), true, "Guid value 1"));
                result.Add(new DataItem<Pair<string, Guid>>(new Pair<string, Guid>("       1", Guid.Parse("00000000-0000-0000-0000-000000000001")), true, "Guid value 1"));
                result.Add(new DataItem<Pair<string, Guid>>(new Pair<string, Guid>("1       ", Guid.Parse("00000000-0000-0000-0000-000000000001")), true, "Guid value 1"));
                result.Add(new DataItem<Pair<string, Guid>>(new Pair<string, Guid>("1       ", Guid.Parse("00000000-0000-0000-0000-000000000001")), true, "Guid value 1"));
                result.Add(new DataItem<Pair<string, Guid>>(new Pair<string, Guid>("A12", Guid.Parse("00000000-0000-0000-0000-000000000A12")), true, "Guid value A12"));
                result.Add(new DataItem<Pair<string, Guid>>(new Pair<string, Guid>("A1Z", Guid.Parse("00000000-0000-0000-0000-000000000000")), true, "Guid value A1Z"));
                result.Add(new DataItem<Pair<string, Guid>>(new Pair<string, Guid>("123456789012", Guid.Parse("00000000-0000-0000-0000-123456789012")), true, "Guid value 123456789012"));
                result.Add(new DataItem<Pair<string, Guid>>(new Pair<string, Guid>("1234567890123", Guid.Parse("00000000-0000-0000-0000-000000000000")), true, "Guid value 123456789012"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> EnumDataSource
        {
            get
            {
                List<DataItem<Pair<string, BooleanOption>>> result = new List<DataItem<Pair<string, BooleanOption>>>();

                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>(null, default(BooleanOption)), true, "Null String"));
                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("All", BooleanOption.All), true, "All"));
                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("False", BooleanOption.False), true, "False"));
                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("True", BooleanOption.True), true, "True"));

                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("   All   ", BooleanOption.All), true, "All"));
                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("   False   ", BooleanOption.False), true, "False"));
                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("   True   ", BooleanOption.True), true, "True"));

                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("0", BooleanOption.All), true, "Value 0 == All"));
                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("1", BooleanOption.True), true, "Value 1 == True"));
                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("2", BooleanOption.False), true, "Value 2 == False"));
                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("3", default(BooleanOption)), true, "Value 3 == Default enum"));
                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("-1", default(BooleanOption)), true, "Value -1 == Default enum"));
                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("XYZ", default(BooleanOption)), true, "Value XYZ == Default enum"));

                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("   0   ", BooleanOption.All), true, "Value 0 == All"));
                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("   1   ", BooleanOption.True), true, "Value 1 == True"));
                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("   2   ", BooleanOption.False), true, "Value 2 == False"));
                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("   3   ", default(BooleanOption)), true, "Value 3 == Default enum"));
                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("   -1   ", default(BooleanOption)), true, "Value -1 == Default enum"));
                result.Add(new DataItem<Pair<string, BooleanOption>>(new Pair<string, BooleanOption>("   XYZ   ", default(BooleanOption)), true, "Value XYZ == Default enum"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }
    }
}
