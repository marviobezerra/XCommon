using System.Collections.Generic;
using XCommon.CodeGenerator.Core.DataBase;
using XCommon.CodeGenerator.CSharp.Configuration;
using XCommon.CodeGenerator.TypeScript.Configuration;
using XCommon.Patterns.Ioc;

namespace XCommon.CodeGenerator
{
	public class GeneratorConfig
	{
		private IReadOnlyList<DataBaseSchema> _DataBaseItems;

		public CSharpConfig CSharp { get; set; }

		public TypeScriptConfig TypeScript { get; set; }

		public DataBaseConfig DataBase { get; set; }

		public IReadOnlyList<DataBaseSchema> DataBaseItems
		{
			get
			{
				if (_DataBaseItems == null)
				{
					var dataBaseReader = Kernel.Resolve<IDataBaseRead>();
					_DataBaseItems = dataBaseReader.Read();
				}

				return _DataBaseItems;
			}
		}
	}
}
