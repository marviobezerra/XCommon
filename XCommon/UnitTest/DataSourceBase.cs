using System.Collections.Generic;
using System.Linq;

namespace XCommon.UnitTest
{
	public abstract class DataSourceBase<TBase, TEntity>
		where TBase : DataSourceBase<TBase, TEntity>, new()
	{
		protected static readonly TBase Instance = new TBase();

		private static List<DataItem<TEntity>> DataLoad { get; set; }

		protected abstract List<DataItem<TEntity>> Load();

		public static List<TEntity> Data
		{
			get
			{
				if (DataLoad == null)
					DataLoad = Instance.Load();

				return DataLoad.Select(c => c.Item).ToList();
			}
		}

		public static IEnumerable<object[]> DataSource
		{
			get
			{
				if (DataLoad == null)
					DataLoad = Instance.Load();

				foreach (var item in DataLoad)
				{
					yield return item.Cast();
				}
			}
		}
	}
}
