namespace XCommon.CodeGenerator.TypeScript.Configuration
{
    public class NameOveride
    {
        public NameOveride(string nameSpace, string fileName)
        {
            NameSpace = nameSpace;
            FileName = fileName.Replace(".ts", string.Empty);
        }

        public string NameSpace { get; private set; }

        public string FileName { get; private set; }
    }
}
