namespace XCommon.Test.Patterns.Specification.Helper
{
    public class GenerictValueEntity<TValue>
    {
        public GenerictValueEntity(TValue value)
        {
            Value = value;
        }

        public TValue Value { get; set; }
    }

    public class BiggerThanEntity<TType>
    {
        public BiggerThanEntity(TType value, TType compare)
        {
            Value = value;
            Compare = compare;
        }

        public TType Value { get; set; }

        public TType Compare { get; set; }
    }

    public class LessThanEntity<TType>
    {
        public LessThanEntity(TType value, TType compare)
        {
            Value = value;
            Compare = compare;
        }

        public TType Value { get; set; }

        public TType Compare { get; set; }
    }

    public class InRangeEntity<TType>
    {
        public InRangeEntity(TType value, TType start, TType end)
        {
            Value = value;
            Start = start;
            End = end;
        }

        public TType Value { get; set; }

        public TType Start { get; set; }

        public TType End { get; set; }
    }
}
