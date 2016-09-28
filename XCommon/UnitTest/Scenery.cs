using System;
using System.Collections.Generic;
using XCommon.Util;

namespace XCommon.UnitTest
{
	public abstract class Scenery<TKey, TParm>
	{
		public Scenery()
		{
			Config = new Dictionary<TKey, Pair<Type, IScenary<TParm>>>();
			SetUp();
		}

		protected Dictionary<TKey, Pair<Type, IScenary<TParm>>> Config { get; set; }

		protected abstract void SetUp();

		protected abstract TParm GetParam();

		public virtual void Load(TKey scenary)
		{
			var config = Config[scenary];

			if (config.Item2 == null)
				config.Item2 = (IScenary<TParm>)Activator.CreateInstance(config.Item1);

			config.Item2.Run(GetParam());
		}
	}
}
