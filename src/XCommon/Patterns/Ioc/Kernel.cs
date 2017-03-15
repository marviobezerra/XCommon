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
		}

		private static Dictionary<Type, RepositoryType> Repository { get; set; }

		public static int Count
		{
			get
			{
				return Repository.Count;
			}
		}

		#region Resolve
		public static TContract Resolve<TContract>()
			=> (TContract)Resolve(typeof(TContract), true, true);

		public static TContract Resolve<TContract>(bool canCache, bool forceResolve = true)
			=> (TContract)Resolve(typeof(TContract), canCache, forceResolve);

		public static void Resolve(object target)
		{
			foreach (var item in target.GetAttributes<InjectAttribute>())
			{
				item.Property.SetValue(target, Resolve(item.Property.PropertyType, item.Attribute.CanCache, item.Attribute.ForceResolve), null);
			}
		}

		internal static object Resolve(Type contract, bool canCache, bool forceResolve)
		{
			lock (Repository)
			{
				Repository.TryGetValue(contract, out RepositoryType repositoryItem);

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
			throw new Exception(string.Format("It is impossible to resolve the type: {0}", contract.FullName));
		}

		private static object ResolveByFunction(RepositoryType repositoryItem, bool canCache)
		{
			if (!canCache)
			{
				repositoryItem.CountNewInstance++;
				return repositoryItem.Resolver();
			}

			if (repositoryItem.Instance == null)
			{
				repositoryItem.CountNewInstance++;
				repositoryItem.Instance = repositoryItem.Resolver();

				return repositoryItem.Instance;
			}

			repositoryItem.CountCacheInstance++;
			return repositoryItem.Instance;
		}

		private static object ResolveByActivator(RepositoryType repositoryItem, bool canCache)
		{
			if (!repositoryItem.UseActicator)
			{
				repositoryItem.CountCacheInstance++;
				return repositoryItem.Instance;
			}

			if (!canCache)
			{
				repositoryItem.CountNewInstance++;
				return Activator.CreateInstance(repositoryItem.ConcretType);
			}

			if (repositoryItem.Instance == null)
			{
				repositoryItem.CountNewInstance++;

				repositoryItem.Instance = Activator.CreateInstance(repositoryItem.ConcretType, repositoryItem.ConstructorParams);
				return repositoryItem.Instance;
			}

			repositoryItem.CountCacheInstance++;

			return repositoryItem.Instance;
		}

		#endregion

		#region Map
		public static Map<TContract> Map<TContract>()
			where TContract : class
		{
			return new Map<TContract>();
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
				var contrusctorParameters = constructor.GetParameters();

				if (contrusctorParameters.Length != args.Length)
				{
					continue;
				}

				for (var i = 0; i < contrusctorParameters.Length; i++)
				{
					var paramContract = contrusctorParameters[i].ParameterType;
					var paramConcrect = args[i].GetType();

					count += paramConcrect.IsBasedIn(paramContract)
						? 1
						: 0;
				}

				if (count == args.Length)
				{
					return;
				}
			}

			throw new Exception($"There isn't constructor for class {concret.Name} with the informed params");
		}

		private static void MapValidateTypes(Type contract, Type concret, object[] args)
		{
			if (concret.GetTypeInfo().IsInterface || concret.GetTypeInfo().IsAbstract)
			{
				throw new Exception($"The final class {concret.Name} needs cannot be an interface or abstract class");
			}

			if (contract.GetTypeInfo().IsInterface && !concret.GetTypeInfo().GetInterfaces().Contains(contract))
			{
				throw new Exception($"The class {concret.Name} doesn't implement the interface {contract.Name}");
			}

			if (!contract.GetTypeInfo().IsInterface && contract.GetTypeInfo().IsAbstract && !concret.IsBasedIn(contract))
			{
				throw new Exception($"The class {concret.Name} doesn't implement the abstract {contract.Name}");
			}
		}

		internal static void Map(Type contract, Type concret, object instance, object[] constructorParams, Func<object> resolver)
		{
			lock (Repository)
			{
				Repository[contract] = new RepositoryType
				{
					Instance = instance,
					ConstructorParams = constructorParams,
					Resolver = resolver,
					ConcretType = concret,
					UseResolver = resolver != null,
					UseActicator = instance == null
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
			{
				return false;
			}

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

		public static List<KernelReportEntity> Report()
		{
			var result = new List<KernelReportEntity>();

			foreach (var item in Repository)
			{
				var report = new KernelReportEntity
				{
					MapFrom = item.Key.GetClassName(),
					MapTo = item.Value.ConcretType?.GetClassName(),
					CountCacheInstance = item.Value.CountCacheInstance,
					CountNewInstance = item.Value.CountNewInstance
				};

				result.Add(report);
			}

			return result;
		}

		public static void Reset()
		{
			Repository = new Dictionary<Type, RepositoryType>();
		}
	}
}
