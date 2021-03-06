﻿using System;
using System.Collections.Generic;

namespace XCommon.UnitTest
{
    public abstract class SceneryManager<TKey, TParam>
	{
		private static readonly object Lock = new object();

		public SceneryManager()
		{
			Config = new Dictionary<TKey, IScenery<TParam>>();
			SetUp();
		}

		protected Dictionary<TKey, IScenery<TParam>> Config { get; set; }

		protected abstract void SetUp();

		public abstract void Reset();

        protected abstract TParam BeforeRun();

        protected abstract void AfterRun(TParam param);

		public virtual void Load(params TKey[] sceneries)
		{
			lock (Lock)
			{
                
                var param = BeforeRun();

				foreach (var item in sceneries)
				{
                    Config[item].Run(param);
				}

                AfterRun(param);
			}
		}
	}
}
