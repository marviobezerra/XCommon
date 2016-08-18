using System;

namespace XCommon.CodeGerator.TypeScript
{
	internal class TypeScriptHelper
    {
		public TypeScriptHelper()
		{
			EntityGenerator = new Entities();
			ResourceGenerator = new Resource();
            Index = new IndexExport();
		}

		internal Entities EntityGenerator { get; set; }

		internal Resource ResourceGenerator { get; set; }

        internal IndexExport Index { get; set; }

		internal int Run()
		{
			EntityGenerator.Run();
			ResourceGenerator.Run();
            Index.Run();

            Console.WriteLine("TypeScript code completed");

            return 0;
		}
	}
}
