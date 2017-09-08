namespace XCommon.CodeGeneratorV2.CSharp.Configuration
{
	public class CSharpEntityConfig : CSharpProjectConfig
    {
		public CSharpEntityConfig()
		{
			ExecuteEntity = true;
			ExecuteFilter = true;
		}

		public bool ExecuteFilter { get; set; }

		public bool ExecuteEntity { get; set; }
	}
}
