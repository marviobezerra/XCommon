using System.Collections.Generic;

namespace XCommon.CodeGeneratorV2.Angular
{
	public interface IComponentWriter
    {
		void Run(string path, string module, string feature, List<string> components);
	}
}
