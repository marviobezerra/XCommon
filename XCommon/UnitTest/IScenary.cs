namespace XCommon.UnitTest
{
	public interface IScenary<TContext>
	{
		void Run(TContext context);
	}
}
