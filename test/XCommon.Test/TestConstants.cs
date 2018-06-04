using System;

namespace XCommon.Test
{
	public static class TestConstants
    {
		public static bool IsMyMachine => Environment.MachineName.ToUpper() == "BrainiacXXX".ToUpper();
	}
}
