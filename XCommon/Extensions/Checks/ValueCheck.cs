using System;

namespace XCommon.Extensions.Checks
{
    public static class ValueCheck
    {
        #region BiggerThan
        public static bool BiggerThan(this DateTime value, DateTime reference, bool removeMinutes = false)
        {
            if (removeMinutes)
            {
                return value.Date > reference.Date;
            }

            return value > reference;
        }

        public static bool BiggerThan(this int value, int reference)
        {
            return value > reference;
        }

        public static bool BiggerThan(this decimal value, decimal reference)
        {
            return value > reference;
        }
        #endregion

        #region LessThan
        public static bool LessThan(this DateTime value, DateTime reference, bool removeMinutes = false)
        {
            if (removeMinutes)
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
        public static bool InRange(this DateTime value, DateTime start, DateTime end, bool removeMinutes = false)
        {
            if (removeMinutes)
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
    }
}
