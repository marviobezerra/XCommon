using System.Collections.Generic;

namespace XCommon.CodeGenerator.Angular
{
	public interface IServiceWriter
    {
		void Run(string path, List<string> services);
	}
}
