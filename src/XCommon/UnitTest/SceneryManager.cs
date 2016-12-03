using System;
using System.Collections.Generic;
using XCommon.Util;

namespace XCommon.UnitTest
{
	public abstract class SceneryManager<TKey>
	{
		private static readonly object Lock = new object();

		public SceneryManager()
		{
			Config = new Dictionary<TKey, Type>();
			SetUp();
		}

		protected Dictionary<TKey, Type> Config { get; set; }

		protected abstract void SetUp();

        protected abstract void Preload();

		public virtual void Load(params TKey[] sceneries)
		{
			lock (Lock)
			{
                Preload();

				foreach (var item in sceneries)
				{
                    var scenery = (IScenery)Activator.CreateInstance(Config[item]);
                    scenery.Run();
				}
			}
		}
	}
}
