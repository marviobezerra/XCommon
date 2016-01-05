using System;

namespace XCommon.Patterns.Ioc
{
    public class Map<TContract>
    {
        internal Type Contract { get; set; }

        internal Map(Type type)
        {
            Contract = type;
        }

        public void To<TConcret>()
        {
            To<TConcret>(true);
        }

        public void To<TConcret>(params object[] args)
        {
            To<TConcret>(true, args);
        }

        public void To<TConcret>(bool canCache, params object[] args)
        {
            if (typeof(TConcret).CheckIsInterface() || typeof(TConcret).CheckIsAbstract())
                throw new Exception("O mapeamento não pode terminar por uma interface ou classe abstrata");

            RepositoryManager.Add(Contract, typeof(TConcret), true, canCache, args);
        }

        public void To<TConcret>(TConcret instance, params object[] args)
        {
            RepositoryManager.Add(Contract, typeof(TConcret), instance, true, true, args);
        }

        public void ToFunc(Func<object> resolver)
        {
            RepositoryManager.Add<TContract>(Contract, resolver);
        }
    }
}
