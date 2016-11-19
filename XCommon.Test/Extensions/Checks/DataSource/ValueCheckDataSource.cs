using System;
using System.Collections.Generic;
using XCommon.UnitTest;

namespace XCommon.Test.Extensions.Checks.DataSource
{
    public static class ValueCheckDataSource
    {
        public static IEnumerable<object[]> BiggerThanDateTimeDataSource
        {
            get
            {
                DataList<DateTime, DateTime, bool> result = new DataList<DateTime, DateTime, bool>();

                result.Add(DateTime.Now, DateTime.Now, false, "Equals date");

                return result.Cast();                
            }
        }

        public static IEnumerable<object[]> BiggerThanDateTimeNoTimeDataSource
        {
            get
            {
                DataList<DateTime, DateTime, bool> result = new DataList<DateTime, DateTime, bool>();

                result.Add(DateTime.Now, DateTime.Now, false, "Equals date");
                result.Add(DateTime.Now.AddDays(-1), DateTime.Now, false, "Equals date");

                return result.Cast();                
            }
        }

        public static IEnumerable<object[]> BiggerThanIntDataSource
        {
            get
            {
                DataList<int, int, bool> result = new DataList<int, int, bool>();

                result.Add(0, 0, false, "Equals value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> BiggerThanDecimalDataSource
        {
            get
            {
                DataList<decimal, decimal, bool> result = new DataList<decimal, decimal, bool>();

                result.Add(0, 0, false, "Equals value");
                
                return result.Cast();                
            }
        }

        public static IEnumerable<object[]> LessThanDateTimeDataSource
        {
            get
            {
                DataList<DateTime, DateTime, bool> result = new DataList<DateTime, DateTime, bool>();

                result.Add(DateTime.Now, DateTime.Now, false, "Equals date");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> LessThanDateTimeNoTimeDataSource
        {
            get
            {
                DataList<DateTime, DateTime, bool> result = new DataList<DateTime, DateTime, bool>();

                result.Add(DateTime.Now, DateTime.Now, false, "Equals date");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> LessThanIntDataSource
        {
            get
            {
                DataList<int, int, bool> result = new DataList<int, int, bool>();

                result.Add(0, 0, false, "Equals value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> LessThanDecimalDataSource
        {
            get
            {
                DataList<decimal, decimal, bool> result = new DataList<decimal, decimal, bool>();

                result.Add(0, 0, false, "Equals value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> InRangeDateTimeDataSource
        {
            get
            {
                DataList<DateTime, DateTime, DateTime, bool> result = new DataList<DateTime, DateTime, DateTime, bool>();

                result.Add(DateTime.Now, DateTime.Now, DateTime.Now, true, "Equals date");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> InRangeDateTimeNoTimeDataSource
        {
            get
            {
                DataList<DateTime, DateTime, DateTime, bool> result = new DataList<DateTime, DateTime, DateTime, bool>();

                result.Add(DateTime.Now, DateTime.Now, DateTime.Now, true, "Equals date");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> InRangeIntDataSource
        {
            get
            {
                DataList<int, int, int, bool> result = new DataList<int, int, int, bool>();

                result.Add(0, 0, 0, true, "Equals value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> InRangeDecimalDataSource
        {
            get
            {
                DataList<decimal, decimal, decimal, bool> result = new DataList<decimal, decimal, decimal, bool>();

                result.Add(0, 0, 0, true, "Equals value");

                return result.Cast();
            }
        }
    }
}
