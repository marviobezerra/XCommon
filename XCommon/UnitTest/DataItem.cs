namespace XCommon.UnitTest
{
	public class DataItem<TEntity>
	{
		public TEntity Item { get; set; }

		public bool Valid { get; set; }

		public string Message { get; set; }

		public object[] Cast()
		{
			return new object[] { Item, Valid, Message };
		}
	}
}
