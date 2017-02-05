using System;
using System.Collections.Generic;
using System.Linq;

namespace XCommon.Extensions.Checks
{
    public static class ValueCheck
    {
        #region GreaterThan
        public static bool GreaterThan(this DateTime value, DateTime reference, bool removeTime = false)
        {
            if (removeTime)
            {
                return value.Date > reference.Date;
            }

            return value > reference;
        }

        public static bool GreaterThan(this int value, int reference)
        {
            return value > reference;
        }

        public static bool GreaterThan(this decimal value, decimal reference)
        {
            return value > reference;
        }
        #endregion

        #region LessThan
        public static bool LessThan(this DateTime value, DateTime reference, bool removeTime = false)
        {
            if (removeTime)
            {
                return value.Date < reference.Date;
            }

            return value < reference;
        }

        public static bool LessThan(this int value, int reference)
        {
            return value < reference;
        }

        public static bool LessThan(this decimal value, decimal reference)
        {
            return value < reference;
        }
        #endregion

        #region InRange
        public static bool InRange(this DateTime value, DateTime start, DateTime end, bool removeTime = false)
        {
            if (removeTime)
            {
                return (start.Date <= value.Date) && (value.Date <= end.Date);
            }

            return (start <= value) && (value <= end);
        }

        public static bool InRange(this int value, int start, int end)
        {
            return (start <= value) && (value <= end);
        }

        public static bool InRange(this decimal value, decimal start, decimal end)
        {
            return (start <= value) && (value <= end);
        }
        #endregion

        public static bool IsValidList(this List<Guid> list, bool ignoreEmpty = false)
        {
            return list != null 
                && list.Any() 
                && (ignoreEmpty ? list.Any(c => c != Guid.Empty) : true);
        }
    }
}
