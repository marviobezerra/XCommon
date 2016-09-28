using System;
using System.Collections.Generic;
using XCommon.Util;

namespace XCommon.UnitTest
{
	public abstract class Scenery<TKey, TParm>
	{
		public Scenery()
		{
			Config = new Dictionary<TKey, Pair<Type, IScenery<TParm>>>();
			SetUp();
		}

		protected Dictionary<TKey, Pair<Type, IScenery<TParm>>> Config { get; set; }

		protected abstract void SetUp();

		protected abstract TParm GetParam();

		public virtual void Load(params TKey[] scenery)
		{
			TParm param = GetParam();

			foreach (var item in scenery)
			{
				var config = Config[item];

				if (config.Item2 == null)
					config.Item2 = (IScenery<TParm>)Activator.CreateInstance(config.Item1);

				config.Item2.Run(param);
			}			
		}
	}
}
