namespace XCommon.UnitTest
{
	public interface IScenery<TContext>
	{
		void Run(TContext context);
	}
}
