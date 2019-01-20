namespace XCommon.CodeGenerator.CSharp.Configuration
{
	public class CSharpApplicationClass
	{
		public CSharpApplicationClass()
		{

		}

		public CSharpApplicationClass(string schema, string name)
		{
			Schema = schema;
			Name = name;
		}

		public string Schema { get; set; }

		public string Name { get; set; }

		public string NamespaceContext => $"XCommon.EF.Application.Context.{Schema}";

		public string NamespaceEntity => $"XCommon.Entity.{Schema}";

		public string NamespaceFilter => $"XCommon.Entity.{Schema}.Filter";

		public string NamespaceBusiness => $"XCommon.EF.Application.{Schema}.Implementation";

		public string NamespaceContract => $"XCommon.EF.Application.{Schema}.Interface";

		public string NamespaceQuery => $"XCommon.EF.Application.{Schema}.Implementation.Query";

		public string NamespaceValidate => $"XCommon.EF.Application.{Schema}.Implementation.Validate";
	}
}
