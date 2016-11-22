namespace XCommon.Test.Patterns.Ioc.Sample
{
    public abstract class TestAbstract
    {
        public abstract int Test();

        public int Test(int value)
        {
            return value++;
        }
    }
}
