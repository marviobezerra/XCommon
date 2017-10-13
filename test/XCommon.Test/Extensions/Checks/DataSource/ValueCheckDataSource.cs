using System;
using System.Collections.Generic;
using XCommon.Extensions.Converters;
using XCommon.Util;

namespace XCommon.Test.Extensions.Checks.DataSource
{
	public static class ValueCheckDataSource
    {
        public static IEnumerable<object[]> GreaterThanDateTimeDataSource
        {
            get
            {
                var result = new PairList<Pair<DateTime, DateTime>, bool, string>();

                result.Add(new Pair<DateTime, DateTime>(DateTime.Now, DateTime.Now), false, "Equals date");

                return result.Cast();                
            }
        }

        public static IEnumerable<object[]> GreaterThanDateTimeNoTimeDataSource
        {
            get
            {
                var result = new PairList<Pair<DateTime, DateTime>, bool, string>();

                result.Add(new Pair<DateTime, DateTime>(DateTime.Now, DateTime.Now), false, "Equals date");

                return result.Cast();                
            }
        }

        public static IEnumerable<object[]> GreaterThanIntDataSource
        {
            get
            {
                var result = new PairList<int, int, bool, string>();

                result.Add(0, 0, false, "Equals value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> GreaterThanDecimalDataSource
        {
            get
            {
                var result = new PairList<decimal, decimal, bool, string>();

                result.Add(0, 0, false, "Equals value");
                
                return result.Cast();                
            }
        }

        public static IEnumerable<object[]> LessThanDateTimeDataSource
        {
            get
            {
                var result = new PairList<Pair<DateTime, DateTime>, bool, string>();
				var value = DateTime.Now;
                result.Add(new Pair<DateTime, DateTime>(value, value), false, "Equals date");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> LessThanDateTimeNoTimeDataSource
        {
            get
            {
                var result = new PairList<Pair<DateTime, DateTime>, bool, string>();

                result.Add(new Pair<DateTime, DateTime>(DateTime.Now, DateTime.Now), false, "Equals date");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> LessThanIntDataSource
        {
            get
            {
                var result = new PairList<int, int, bool, string>();

                result.Add(0, 0, false, "Equals value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> LessThanDecimalDataSource
        {
            get
            {
                var result = new PairList<decimal, decimal, bool, string>();

                result.Add(0, 0, false, "Equals value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> InRangeDateTimeDataSource
        {
            get
            {
                var result = new PairList<Pair<DateTime, DateTime, DateTime>, bool, string>();

                result.Add(new Pair<DateTime, DateTime, DateTime>(DateTime.Now, DateTime.Now.AddSeconds(-1), DateTime.Now.AddSeconds(1)), true, "Equals date");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> InRangeDateTimeNoTimeDataSource
        {
            get
            {
                var result = new PairList<Pair<DateTime, DateTime, DateTime>, bool, string>();

                result.Add(new Pair<DateTime, DateTime, DateTime>(DateTime.Now, DateTime.Now, DateTime.Now), true, "Equals date");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> InRangeIntDataSource
        {
            get
            {
                var result = new PairList<int, int, int, bool, string>();

                result.Add(0, 0, 0, true, "Equals value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> InRangeDecimalDataSource
        {
            get
            {
                var result = new PairList<decimal, decimal, decimal, bool, string>();

                result.Add(0, 0, 0, true, "Equals value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> GuidValidList
        {
            get
            {
                var result = new PairList<List<Guid>, bool, string>();

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
                var result = new PairList<List<Guid>, bool, string>();

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
