namespace XCommon.CodeGerator.Business
{
	public class BusinessHelper
    {
		public BusinessHelper()
		{
			Contract = new BusinessContract();
			Concret = new BusinessConcret();
			Data = new BusinessData();
			Entity = new BusinessEntity();
			Factory = new BusinessFactory();
		}

		private BusinessConcret Concret { get; set; }

		private BusinessContract Contract { get; set; }

		private BusinessData Data { get; set; }

		private BusinessEntity Entity { get; set; }

		private BusinessFactory Factory { get; set; }

		public int RunAll()
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
