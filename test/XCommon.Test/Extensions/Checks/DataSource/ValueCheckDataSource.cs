using System;
using System.Collections.Generic;
using XCommon.Extensions.Converters;
using XCommon.UnitTest;
using XCommon.Util;

namespace XCommon.Test.Extensions.Checks.DataSource
{
    public static class ValueCheckDataSource
    {
        public static IEnumerable<object[]> BiggerThanDateTimeDataSource
        {
            get
            {
                PairList<Pair<DateTime, DateTime>, bool, string> result = new PairList<Pair<DateTime, DateTime>, bool, string>();

                result.Add(new Pair<DateTime, DateTime>(DateTime.Now, DateTime.Now), false, "Equals date");

                return result.Cast();                
            }
        }

        public static IEnumerable<object[]> BiggerThanDateTimeNoTimeDataSource
        {
            get
            {
                PairList<Pair<DateTime, DateTime>, bool, string> result = new PairList<Pair<DateTime, DateTime>, bool, string>();

                result.Add(new Pair<DateTime, DateTime>(DateTime.Now, DateTime.Now), false, "Equals date");

                return result.Cast();                
            }
        }

        public static IEnumerable<object[]> BiggerThanIntDataSource
        {
            get
            {
                PairList<int, int, bool, string> result = new PairList<int, int, bool, string>();

                result.Add(0, 0, false, "Equals value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> BiggerThanDecimalDataSource
        {
            get
            {
                PairList<decimal, decimal, bool, string> result = new PairList<decimal, decimal, bool, string>();

                result.Add(0, 0, false, "Equals value");
                
                return result.Cast();                
            }
        }

        public static IEnumerable<object[]> LessThanDateTimeDataSource
        {
            get
            {
                PairList<Pair<DateTime, DateTime>, bool, string> result = new PairList<Pair<DateTime, DateTime>, bool, string>();

                result.Add(new Pair<DateTime, DateTime>(DateTime.Now, DateTime.Now), false, "Equals date");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> LessThanDateTimeNoTimeDataSource
        {
            get
            {
                PairList<Pair<DateTime, DateTime>, bool, string> result = new PairList<Pair<DateTime, DateTime>, bool, string>();

                result.Add(new Pair<DateTime, DateTime>(DateTime.Now, DateTime.Now), false, "Equals date");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> LessThanIntDataSource
        {
            get
            {
                PairList<int, int, bool, string> result = new PairList<int, int, bool, string>();

                result.Add(0, 0, false, "Equals value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> LessThanDecimalDataSource
        {
            get
            {
                PairList<decimal, decimal, bool, string> result = new PairList<decimal, decimal, bool, string>();

                result.Add(0, 0, false, "Equals value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> InRangeDateTimeDataSource
        {
            get
            {
                PairList<Pair<DateTime, DateTime, DateTime>, bool, string> result = new PairList<Pair<DateTime, DateTime, DateTime>, bool, string>();

                result.Add(new Pair<DateTime, DateTime, DateTime>(DateTime.Now, DateTime.Now, DateTime.Now), true, "Equals date");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> InRangeDateTimeNoTimeDataSource
        {
            get
            {
                PairList<Pair<DateTime, DateTime, DateTime>, bool, string> result = new PairList<Pair<DateTime, DateTime, DateTime>, bool, string>();

                result.Add(new Pair<DateTime, DateTime, DateTime>(DateTime.Now, DateTime.Now, DateTime.Now), true, "Equals date");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> InRangeIntDataSource
        {
            get
            {
                PairList<int, int, int, bool, string> result = new PairList<int, int, int, bool, string>();

                result.Add(0, 0, 0, true, "Equals value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> InRangeDecimalDataSource
        {
            get
            {
                PairList<decimal, decimal, decimal, bool, string> result = new PairList<decimal, decimal, decimal, bool, string>();

                result.Add(0, 0, 0, true, "Equals value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> GuidValidList
        {
            get
            {
                PairList<List<Guid>, bool, string> result = new PairList<List<Guid>, bool, string>();

                result.Add(null, false, "Null list");
                result.Add(new List<Guid>(), false, "Empty list");
                result.Add(new List<Guid> { "0".ToGuid() }, true, "Just GuidEmpty");
                result.Add(new List<Guid> { "1".ToGuid() }, true, "One valid Guid");
                result.Add(new List<Guid> { "1".ToGuid(), "2".ToGuid() }, true, "Two valid Guid");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> GuidValidListIgnoreEmpty
        {
            get
            {
                PairList<List<Guid>, bool, string> result = new PairList<List<Guid>, bool, string>();

                result.Add(null, false, "Null list");
                result.Add(new List<Guid>(), false, "Empty list");
                result.Add(new List<Guid> { "0".ToGuid() }, false, "Just GuidEmpty");
                result.Add(new List<Guid> { "1".ToGuid() }, true, "One valid Guid");
                result.Add(new List<Guid> { "0".ToGuid(), "1".ToGuid() }, true, "Two valid Guid");

                return result.Cast();
            }
        }
    }
}
