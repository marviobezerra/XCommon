namespace XCommon.Test.Patterns.Specification.Helper
{
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
}
