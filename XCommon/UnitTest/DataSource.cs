using System.Collections.Generic;
using System.Linq;

namespace XCommon.UnitTest
{
	public abstract class DataSource<TBase, TEntity>
		where TBase : DataSource<TBase, TEntity>, new()
	{
		protected static readonly TBase Instance = new TBase();

		private static List<DataItem<TEntity>> _Data { get; set; }

		protected abstract List<DataItem<TEntity>> LoadData();

		public static List<TEntity> DataList
		{
			get
			{
				if (_Data == null)
					_Data = Instance.LoadData();

				return _Data.Select(c => c.Item).ToList();
			}
		}

		public static IEnumerable<object[]> Data
		{
			get
			{
				if (_Data == null)
					_Data = Instance.LoadData();

				foreach (var item in _Data)
				{
					yield return item.Cast();
				}
			}
		}
	}

	public abstract class DataSource<TBase, TEntity, TFilter> : DataSource<TBase, TEntity>
		where TBase : DataSource<TBase, TEntity, TFilter>, new()
	{
		private static List<DataItem<TFilter>> _Filter { get; set; }

		protected abstract List<DataItem<TFilter>> LoadFilter();

		public static List<TFilter> FilterList
		{
			get
			{
				if (_Filter == null)
					_Filter = Instance.LoadFilter();

				return _Filter.Select(c => c.Item).ToList();
			}
		}

		public static IEnumerable<object[]> Filter
		{
			get
			{
				if (_Filter == null)
					_Filter = Instance.LoadFilter();

				foreach (var item in _Filter)
				{
					yield return item.Cast();
				}
			}
		}
	}
}
