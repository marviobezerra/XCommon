using System;
using System.Collections.Generic;
using XCommon.Patterns.Ioc;

namespace XCommon.CodeGenerator.Core
{
	public abstract class BaseRunner
    {
		[Inject]
		protected GeneratorConfig Config { get; set; }

		[Inject]
		protected ILog Log { get; set; }

		protected List<BaseWriter> Writers { get; set; }


		public BaseRunner()
		{
			Kernel.Resolve(this);
			Writers = new List<BaseWriter>();
		}

		public virtual int Run()
		{
			foreach (var writter in Writers)
			{
				if (!writter.Write())
				{
					return -1;
				}
			}

			return 0;
		}
	}
}
