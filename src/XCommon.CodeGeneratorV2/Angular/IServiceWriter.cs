using System.Collections.Generic;

namespace XCommon.CodeGeneratorV2.Angular
{
	public interface IServiceWriter
    {
		void Run(string path, List<string> services);
	}
}
