using System;

namespace XCommon.CodeGerator.Business
{
	internal class BusinessHelper
    {
		internal BusinessHelper()
		{
			Contract = new BusinessContract();
			Concret = new BusinessConcrete();
			Data = new BusinessData();
			Entity = new BusinessEntity();
			Factory = new BusinessFactory();

			Console.WriteLine("C# code completed");
		}

		private BusinessConcrete Concret { get; set; }

		private BusinessContract Contract { get; set; }

		private BusinessData Data { get; set; }

		private BusinessEntity Entity { get; set; }

		private BusinessFactory Factory { get; set; }

		internal int RunAll()
		{
			Data.Run();
			Entity.Run();
			Contract.Run();
			Concret.Run();
			Factory.Run();

			return 0;
		}
    }
}
