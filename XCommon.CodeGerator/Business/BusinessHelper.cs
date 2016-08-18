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

            TypeScript = new CodeGerator.TypeScript.TypeScriptHelper();
		}

		private BusinessConcrete Concret { get; set; }

		private BusinessContract Contract { get; set; }

		private BusinessData Data { get; set; }

		private BusinessEntity Entity { get; set; }

		private BusinessFactory Factory { get; set; }

        private TypeScript.TypeScriptHelper TypeScript { get; set; }

        internal int Run()
		{
			Data.Run();
			Entity.Run();
			Contract.Run();
			Concret.Run();
			Factory.Run();

            Console.WriteLine("C# code completed");

            TypeScript.Run();
            
            return 0;
		}
    }
}
