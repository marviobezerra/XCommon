using System;
using System.Collections.Generic;
using System.Text;
using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Core.DataBase;

namespace XCommon.CodeGenerator.CSharp
{
	public interface ICSharpUnitTestWriter
	{
		void WriteTests(DataBaseTable table);
	}
}
