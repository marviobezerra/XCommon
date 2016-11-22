using System;

namespace XCommon.Patterns.Ioc
{
    public class Map
	{
        internal Map()
        {

        }

		internal Type Contract { get; set; }
        
        public void To<TConcret>()
            => To<TConcret>(true, new object[] { });

        public void To<TConcret>(params object[] args)
            => To<TConcret>(true, args);

		public void To<TConcret>(bool canCache, params object[] args)
		{
            Type concretType = typeof(TConcret);

            Kernel.MapValidate(Contract, concretType, args);
			Kernel.Map(Contract, concretType, null, true, canCache, args, null);
		}

		public void To<TConcret>(TConcret instance)
		{
            Kernel.Map(Contract, typeof(TConcret), instance, true, true, null, null);
		}

		public void ToFunc(Func<object> resolver)
		{
            Kernel.Map(Contract, null, null, true, true, null, resolver);

        }
    }
}
