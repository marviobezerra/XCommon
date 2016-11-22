using System;

namespace XCommon.Patterns.Ioc
{
	internal class RepositoryType
	{
		internal Func<object> Resolver { get; set; }

        internal bool UseActicator { get; set; }

		internal bool UseResolver { get; set; }

		internal Type ConcretType { get; set; }

		internal object Instance { get; set; }

		internal object[] ConstructorParams { get; set; }
	}
}
