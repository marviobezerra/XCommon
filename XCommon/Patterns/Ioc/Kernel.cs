using System;
using System.Collections.Generic;
using System.Reflection;
using XCommon.Extensions.Util;
using XCommon.Util;
using System.Linq;

namespace XCommon.Patterns.Ioc
{
    public static class Kernel
    {
        static Kernel()
        {
            Reset();
            KernelMap = new Map();
        }

        private static Map KernelMap { get; set; }

        private static Dictionary<Type, RepositoryType> Repository { get; set; }

        public static int Count
        {
            get
            {
                return Repository.Count;
            }
        }

        #region Resolve
        public static TContract Resolve<TContract>(bool canCache = true, bool forceResolve = true)
            => (TContract)Resolve(typeof(TContract), canCache, forceResolve);

        public static void Resolve(object target)
        {
            foreach (AttributeDetail<InjectAttribute> item in target.GetAttributes<InjectAttribute>())
            {
                item.Property.SetValue(target, Resolve(item.Property.PropertyType, item.Attribute.CanCache, item.Attribute.ForceResolve), null);
            }
        }

        public static object Resolve(Type contract, bool canCache, bool forceResolve)
        {
            lock (Repository)
            {
                RepositoryType repositoryItem = null;
                Repository.TryGetValue(contract, out repositoryItem);

                if (repositoryItem == null)
                {
                    return forceResolve
                        ? ResolveByException(contract)
                        : null;
                }

                return repositoryItem.UseResolver
                    ? ResolveByFunction(repositoryItem, canCache)
                    : ResolveByActivator(repositoryItem, canCache);
            }
        }

        private static object ResolveByException(Type contract)
        {
            throw new Exception(string.Format("Não é possivel resolver o tipo: {0}", contract.FullName));
        }

        private static object ResolveByFunction(RepositoryType repositoryItem, bool canCache)
        {
            if (!canCache || !repositoryItem.CanCache)
                return repositoryItem.Resolver();

            if (repositoryItem.Instance == null && repositoryItem.CanCache)
                repositoryItem.Instance = repositoryItem.Resolver();

            return repositoryItem.Instance;
        }

        private static object ResolveByActivator(RepositoryType repositoryItem, bool canCache)
        {
            if (!canCache || !repositoryItem.CanCache)
                return Activator.CreateInstance(repositoryItem.ConcretType);

            if (repositoryItem.Instance == null)
                repositoryItem.Instance = Activator.CreateInstance(repositoryItem.ConcretType, repositoryItem.ConstructorParams);

            return repositoryItem.Instance;
        }

        #endregion

        #region Map
        public static Map Map<TContract>()
            where TContract : class
        {
            KernelMap.Contract = typeof(TContract);
            return KernelMap;
        }

        internal static void MapValidate(Type contract, Type concret, object[] args)
        {
            args = args ?? new object[] { };

            MapValidateTypes(contract, concret, args);
            MapValidateConstructors(contract, concret, args);
        }

        internal static void MapValidateConstructors(Type contract, Type concret, object[] args)
        {
            var constructors = concret.GetTypeInfo().GetConstructors();

            foreach (var constructor in constructors)
            {
                var count = 0;
                var parameters = constructor.GetParameters();

                if (parameters.Length != args.Length)
                    continue;

                for (int i = 0; i < parameters.Length; i++)
                {
                    count += parameters[i].ParameterType == args[i].GetType()
                        ? 1
                        : 0;
                }

                if (count == args.Length)
                    return;
            }

            throw new Exception($"There is no constructor for class {concret.Name} with the informed params");
        }

        private static void MapValidateTypes(Type contract, Type concret, object[] args)
        {
            if (concret.GetTypeInfo().IsInterface || concret.GetTypeInfo().IsAbstract)
                throw new Exception($"The final class {concret.Name} needs cannot be an interface or abstract class");

            if (contract.GetTypeInfo().IsInterface && !concret.GetTypeInfo().GetInterfaces().Contains(contract))
                throw new Exception($"The class {concret.Name} doesn't implement the interface {contract.Name}");

            if (!contract.GetTypeInfo().IsInterface && contract.GetTypeInfo().IsAbstract && !concret.GetTypeInfo().GetNestedTypes().Contains(contract))
                throw new Exception($"The class {concret.Name} doesn't implement the abstract {contract.Name}");
        }

        internal static void Map(Type contract, Type concret, object instance, bool overrideExistence, bool canCache, object[] constructorParams, Func<object> resolver)
        {
            lock (Repository)
            {
                if (Repository.ContainsKey(contract) && !overrideExistence)
                    return;

                Repository[contract] = new RepositoryType
                {
                    CanCache = canCache,
                    Instance = instance,
                    ConstructorParams = constructorParams,
                    Resolver = resolver,
                    ConcretType = concret,
                    UseResolver = resolver != null
                };
            }
        }
        #endregion

        #region Remove
        internal static bool Remove<TConctract>()
            => Remove(typeof(TConctract));

        internal static bool Remove(Type contract)
        {
            if (contract == null)
                return false;

            lock (Repository)
            {
                if (Repository.ContainsKey(contract))
                {
                    Repository.Remove(contract);
                    return true;
                }

                return false;
            }
        }
        #endregion

        public static void Reset()
        {
            Repository = new Dictionary<Type, RepositoryType>();
        }


    }
}
