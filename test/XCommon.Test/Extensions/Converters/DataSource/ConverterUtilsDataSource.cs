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
                PairList<BooleanOption, bool, string> result = new PairList<BooleanOption, bool, string>();

                result.Add(BooleanOption.True, true, "BooleanOption.True = True");
                result.Add(BooleanOption.False, false, "BooleanOption.False = False");
                result.Add(BooleanOption.All, true, "BooleanOption.All = True");
                result.Add(default(BooleanOption), true, "default(BooleanOption) = True");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> BooleanOptionWithDefaultDataSource
        {
            get
            {
                PairList<BooleanOption, bool, bool, string> result = new PairList<BooleanOption, bool, bool, string>();

                result.Add(BooleanOption.True, false, true, "BooleanOption.True = True");
                result.Add(BooleanOption.False, true, false,  "BooleanOption.False = False");
                result.Add(BooleanOption.All, false, false, "BooleanOption.All = False");
                result.Add(default(BooleanOption), false, false, "default(BooleanOption) = True");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> IntDataSource
        {
            get
            {
                PairList<string, int, string> result = new PairList<string, int, string>();

                result.Add(null, 0, "Empty Int");
                result.Add(string.Empty, 0, "Empty Int");
                result.Add("", 0, "Empty Int");
                result.Add("    ", 0, "Empty Int");
                result.Add("AbCd", 0, "Invalid Int");
                result.Add("100.1", 0, "Invalid Int");

                result.Add("1", 1, "Valid Int");
                result.Add("-1", -1, "Valid Int");
                result.Add("10", 10, "Valid Int");
                result.Add("100", 100, "Valid Int");
                result.Add("1000", 1000, "Valid Int");
                result.Add("10000", 10000, "Valid Int");
                result.Add("100000", 100000, "Valid Int");
                result.Add("1000000", 1000000, "Valid Int");
                result.Add("10000000", 10000000, "Valid Int");
                result.Add("100000000", 100000000, "Valid Int");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> IntWithDefaultDataSource
        {
            get
            {
                PairList<string, int, int, string> result = new PairList<string, int, int, string>();

                result.Add(null, 9, 9, "Empty Int");
                result.Add(string.Empty, 90, 90, "Empty Int");
                result.Add("", -1, -1, "Empty Int");
                result.Add("    ", -7, -7, "Empty Int");
                result.Add("AbCd", 999, 999, "Invalid Int");
                result.Add("100.1", 80, 80, "Invalid Int");

                result.Add("1", 100, 1, "Valid Int");
                result.Add("-1", 100, -1, "Valid Int");
                result.Add("10", 100, 10, "Valid Int");
                result.Add("100", 100, 100, "Valid Int");
                result.Add("1000", 100, 1000, "Valid Int");
                result.Add("10000", 100, 10000, "Valid Int");
                result.Add("100000", 100, 100000, "Valid Int");
                result.Add("1000000", 100, 1000000, "Valid Int");
                result.Add("10000000", 100, 10000000, "Valid Int");
                result.Add("100000000", 100, 100000000, "Valid Int");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> GuidDataSource
        {
            get
            {
                PairList<string, Guid, string> result = new PairList<string, Guid, string>();

                result.Add(null, Guid.Empty, "Empty Guid");
                result.Add(string.Empty, Guid.Empty, "Empty Guid");
                result.Add("", Guid.Empty, "Empty Guid");
                result.Add("  ", Guid.Empty, "Empty Guid");
                result.Add("1", Guid.Parse("00000000-0000-0000-0000-000000000001"), "Guid value 1");
                result.Add("       1", Guid.Parse("00000000-0000-0000-0000-000000000001"), "Guid value 1");
                result.Add("1       ", Guid.Parse("00000000-0000-0000-0000-000000000001"), "Guid value 1");
                result.Add("1       ", Guid.Parse("00000000-0000-0000-0000-000000000001"), "Guid value 1");
                result.Add("A12", Guid.Parse("00000000-0000-0000-0000-000000000A12"), "Guid value A12");
                result.Add("A1Z", Guid.Parse("00000000-0000-0000-0000-000000000000"), "Guid value A1Z");
                result.Add("123456789012", Guid.Parse("00000000-0000-0000-0000-123456789012"), "Guid value 123456789012");
                result.Add("1234567890123", Guid.Parse("00000000-0000-0000-0000-000000000000"), "Guid value 123456789012");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> EnumDataSource
        {
            get
            {
                PairList<string, BooleanOption, string> result = new PairList<string, BooleanOption, string>();

                result.Add(null, default(BooleanOption), "Null String");
                result.Add("All", BooleanOption.All, "All");
                result.Add("False", BooleanOption.False, "False");
                result.Add("True", BooleanOption.True, "True");

                result.Add("   All   ", BooleanOption.All, "All");
                result.Add("   False   ", BooleanOption.False, "False");
                result.Add("   True   ", BooleanOption.True, "True");

                result.Add("0", BooleanOption.All, "Value 0 == All");
                result.Add("1", BooleanOption.True, "Value 1 == True");
                result.Add("2", BooleanOption.False, "Value 2 == False");
                result.Add("3", default(BooleanOption), "Value 3 == Default enum");
                result.Add("-1", default(BooleanOption), "Value -1 == Default enum");
                result.Add("XYZ", default(BooleanOption), "Value XYZ == Default enum");

                result.Add("   0   ", BooleanOption.All, "Value 0 == All");
                result.Add("   1   ", BooleanOption.True, "Value 1 == True");
                result.Add("   2   ", BooleanOption.False, "Value 2 == False");
                result.Add("   3   ", default(BooleanOption), "Value 3 == Default enum");
                result.Add("   -1   ", default(BooleanOption), "Value -1 == Default enum");
                result.Add("   XYZ   ", default(BooleanOption), "Value XYZ == Default enum");

                return result.Cast();
            }
        }
    }
}
