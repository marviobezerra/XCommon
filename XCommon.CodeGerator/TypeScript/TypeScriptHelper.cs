namespace XCommon.CodeGerator.TypeScript
{
	internal class TypeScriptHelper
    {
		public TypeScriptHelper()
		{
			EntityGenerator = new Entity();
			ResourceGenerator = new Resource();
		}

		internal Entity EntityGenerator { get; set; }

		internal Resource ResourceGenerator { get; set; }

		internal int Run()
		{
			EntityGenerator.Run();
			ResourceGenerator.Run();

			return 0;
		}
	}
}
