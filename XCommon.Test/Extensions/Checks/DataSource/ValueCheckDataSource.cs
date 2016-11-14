using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                List<DataItem<Pair<DateTime, DateTime>>> result = new List<DataItem<Pair<DateTime, DateTime>>>();

                result.Add(new DataItem<Pair<DateTime, DateTime>>(new Pair<DateTime, DateTime>(DateTime.Now, DateTime.Now), false, "Equals date"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> BiggerThanDateTimeNoTimeDataSource
        {
            get
            {
                List<DataItem<Pair<DateTime, DateTime>>> result = new List<DataItem<Pair<DateTime, DateTime>>>();

                result.Add(new DataItem<Pair<DateTime, DateTime>>(new Pair<DateTime, DateTime>(DateTime.Now, DateTime.Now), false, "Equals date"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> BiggerThanIntDataSource
        {
            get
            {
                List<DataItem<Pair<int, int>>> result = new List<DataItem<Pair<int, int>>>();

                result.Add(new DataItem<Pair<int, int>>(new Pair<int, int>(0, 0), false, "Equals value"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> BiggerThanDecimalDataSource
        {
            get
            {
                List<DataItem<Pair<decimal, decimal>>> result = new List<DataItem<Pair<decimal, decimal>>>();

                result.Add(new DataItem<Pair<decimal, decimal>>(new Pair<decimal, decimal>(0, 0), false, "Equals value"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> LessThanDateTimeDataSource
        {
            get
            {
                List<DataItem<Pair<DateTime, DateTime>>> result = new List<DataItem<Pair<DateTime, DateTime>>>();

                result.Add(new DataItem<Pair<DateTime, DateTime>>(new Pair<DateTime, DateTime>(DateTime.Now, DateTime.Now), false, "Equals date"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> LessThanDateTimeNoTimeDataSource
        {
            get
            {
                List<DataItem<Pair<DateTime, DateTime>>> result = new List<DataItem<Pair<DateTime, DateTime>>>();

                result.Add(new DataItem<Pair<DateTime, DateTime>>(new Pair<DateTime, DateTime>(DateTime.Now, DateTime.Now), false, "Equals date"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> LessThanIntDataSource
        {
            get
            {
                List<DataItem<Pair<int, int>>> result = new List<DataItem<Pair<int, int>>>();

                result.Add(new DataItem<Pair<int, int>>(new Pair<int, int>(0, 0), false, "Equals value"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> LessThanDecimalDataSource
        {
            get
            {
                List<DataItem<Pair<decimal, decimal>>> result = new List<DataItem<Pair<decimal, decimal>>>();

                result.Add(new DataItem<Pair<decimal, decimal>>(new Pair<decimal, decimal>(0, 0), false, "Equals value"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }
        public static IEnumerable<object[]> InRangeDateTimeDataSource
        {
            get
            {
                List<DataItem<Pair<DateTime, DateTime, DateTime>>> result = new List<DataItem<Pair<DateTime, DateTime, DateTime>>>();

                result.Add(new DataItem<Pair<DateTime, DateTime, DateTime>>(new Pair<DateTime, DateTime, DateTime>(DateTime.Now, DateTime.Now, DateTime.Now), true, "Equals date"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> InRangeDateTimeNoTimeDataSource
        {
            get
            {
                List<DataItem<Pair<DateTime, DateTime, DateTime>>> result = new List<DataItem<Pair<DateTime, DateTime, DateTime>>>();

                result.Add(new DataItem<Pair<DateTime, DateTime, DateTime>>(new Pair<DateTime, DateTime, DateTime>(DateTime.Now, DateTime.Now, DateTime.Now), true, "Equals date"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> InRangeIntDataSource
        {
            get
            {
                List<DataItem<Pair<int, int, int>>> result = new List<DataItem<Pair<int, int, int>>>();

                result.Add(new DataItem<Pair<int, int, int>>(new Pair<int, int, int>(0, 0, 0), true, "Equals value"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> InRangeDecimalDataSource
        {
            get
            {
                List<DataItem<Pair<decimal, decimal, decimal>>> result = new List<DataItem<Pair<decimal, decimal, decimal>>>();

                result.Add(new DataItem<Pair<decimal, decimal, decimal>>(new Pair<decimal, decimal, decimal>(0, 0, 0), true, "Equals value"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }
    }
}
