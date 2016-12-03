using XCommon.Patterns.Ioc;

namespace XCommon.Test.Patterns.Ioc.Sample
{
    public class SampleBusiness
    {
        public SampleBusiness()
        {
            Kernel.Resolve(this);
        }

        [Inject]
        public IAnimal Animal { get; set; }

        [Inject]
        public Vehicle Vehicle { get; set; }
    }

    public class SampleNoCacheBusiness
    {
        public SampleNoCacheBusiness()
        {
            Kernel.Resolve(this);
        }

        [Inject(canCache: false)]
        public IAnimal Animal { get; set; }

        [Inject]
        public Vehicle Vehicle { get; set; }
    }

    public class SampleNoForceBusiness
    {
        public SampleNoForceBusiness()
        {
            Kernel.Resolve(this);
        }

        [Inject(forceResolve: false)]
        public IAnimal Animal { get; set; }

        [Inject(forceResolve: false)]
        public Vehicle Vehicle { get; set; }
    }
}
