using System;

namespace XCommon.Test.Patterns.Specification.Helper
{
    public class HelperSpecificationItemEntity
    {
        public int Id { get; set; }
    }

    public class SampleEmptyEntity
    {
        public SampleEmptyEntity(bool initNull)
        {
            if (initNull)
                return;

            String = "A";
            Int = 0;
            Decimal = 0;
            DateTime = System.DateTime.Now;
            Item = new HelperSpecificationItemEntity();
        }

        public HelperSpecificationItemEntity Item { get; set; }

        public string String { get; set; }

        public int? Int { get; set; }

        public decimal? Decimal { get; set; }

        public DateTime? DateTime { get; set; }
    }
}
