namespace XCommon.CodeGenerator.TypeScript.Configuration
{
	public class TypeScriptNameOverride
    {
		public TypeScriptNameOverride(string nameSpace, string fileName)
		{
			NameSpace = nameSpace;
			FileName = fileName.Replace(".ts", string.Empty);
		}

		public string NameSpace { get; private set; }

		public string FileName { get; private set; }
	}
}
