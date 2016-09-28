namespace XCommon.Util
{
	public class Pair<T1, T2>
	{
		public Pair()
		{
		}

		public Pair(T1 item1, T2 item2)
		{
			Item1 = item1;
			Item2 = item2;
		}

		public T1 Item1 { get; set; }

		public T2 Item2 { get; set; }
	}

	public class Pair<T1, T2, T3>
	{
		public Pair()
		{
		}

		public Pair(T1 item1, T2 item2, T3 item3)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
		}

		public T1 Item1 { get; set; }

		public T2 Item2 { get; set; }

		public T3 Item3 { get; set; }
	}

	public class Pair<T1, T2, T3, T4>
	{
		public Pair()
		{
		}

		public Pair(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
		}

		public T1 Item1 { get; set; }

		public T2 Item2 { get; set; }

		public T3 Item3 { get; set; }

		public T4 Item4 { get; set; }
	}

	public class Pair<T1, T2, T3, T4, T5>
	{
		public Pair()
		{
		}

		public Pair(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
		}

		public T1 Item1 { get; set; }

		public T2 Item2 { get; set; }

		public T3 Item3 { get; set; }

		public T4 Item4 { get; set; }

		public T5 Item5 { get; set; }
	}
}
