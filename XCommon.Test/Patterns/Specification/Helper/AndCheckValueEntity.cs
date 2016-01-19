using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommon.Patterns.Specification.Entity.Implementation;

namespace XCommon.Test.Patterns.Specification.Helper
{
    public class AndCheckValueEntity
    {
        public AndCheckValueEntity(AndCheckCompareType compareType, bool valid)
        {
            switch (compareType)
            {
                case AndCheckCompareType.BiggerThan:
                    InitBiggerThan(valid);
                    break;
                case AndCheckCompareType.LessThan:
                    InitLessThan(valid);
                    break;
                case AndCheckCompareType.InRange:
                default:
                    InitInRange(valid);
                    break;
            }
        }

        private void InitBiggerThan(bool valid)
        {
            IntValue = 5;
            IntStart = valid ? 4 : 5;

            DecimalValue = 5;
            DecimalStart = valid ? 4 : 5;

            DateTimeValue = DateTime.Now;
            DateTimeStart = DateTime.Now.AddDays(valid ? -1 : 1);
        }

        private void InitLessThan(bool valid)
        {
            IntValue = 5;
            IntStart = valid ? 6 : 4;

            DecimalValue = 5;
            DecimalStart = valid ? 6 : 4;

            DateTimeValue = DateTime.Now;
            DateTimeStart = DateTime.Now.AddDays(valid ? 1 : -1);
        }

        private void InitInRange(bool valid)
        {
            IntValue = 5;
            IntStart = valid ? 4 : 5;
            IntEnd = valid ? 10 : 4;

            DecimalValue = 5;
            DecimalStart = valid ? 4 : 5;
            DecimalEnd = valid ? 10 : 4;

            DateTimeValue = DateTime.Now;
            DateTimeStart = DateTime.Now.AddDays(valid ? -1 : 1);
            DateTimeEnd = DateTime.Now.AddDays(valid ? 1 : -1);
        }
        
        public int IntValue { get; set; }

        public int IntStart { get; set; }

        public int IntEnd { get; set; }

        public decimal DecimalValue { get; set; }

        public decimal DecimalStart { get; set; }

        public decimal DecimalEnd { get; set; }

        public DateTime DateTimeValue { get; set; }

        public DateTime DateTimeStart { get; set; }

        public DateTime DateTimeEnd { get; set; }
    }
}
