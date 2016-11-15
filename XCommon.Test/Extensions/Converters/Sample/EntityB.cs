using System;
using XCommon.Util;

namespace XCommon.Test.Extensions.Converters.Sample
{
    public class EntityB
    {
        public Guid GuidValue { get; set; }

        public Guid? GuidValueNullable { get; set; }

        public int IntValue { get; set; }

        public int? IntValueNullable { get; set; }

        public DateTime DateTimeValue { get; set; }

        public DateTime? DateTimeValueNullable { get; set; }

        public bool BoolValue { get; set; }

        public bool? BoolValueNullAble { get; set; }

        public BooleanOption EnumValue { get; set; }

        public BooleanOption? EnumValueNullable { get; set; }

        public string StringValue { get; set; }
    }
}
