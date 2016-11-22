using System;

namespace XCommon.Patterns.Ioc
{
    public class Map<TContract>
	{
        internal Map()
        {
        }
        
        public void To<TConcret>(params object[] args)
		{
            Type concretType = typeof(TConcret);
            Type contracttType = typeof(TContract);

            Kernel.MapValidate(contracttType, concretType, args);
			Kernel.Map(contracttType, concretType, null, args, null);
		}

		public void To(TContract instance)
		{
            Kernel.Map(typeof(TContract), instance.GetType(), instance, null, null);
		}

		public void To(Func<TContract> resolver)
		{
            Kernel.Map(typeof(TContract), null, null, null, () => resolver());

        }
    }
}
