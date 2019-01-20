using System.Collections.Generic;

namespace XCommon.Util
{
    public class PairList<T>
    {
        private List<T> Source { get; set; }

        public PairList()
        {
            Source = new List<T>();
        }

        public void Add(T param)
        {
            Source.Add(param);
        }

        public void Clean()
        {
            Source.Clear();
        }

        public IEnumerable<object[]> Cast()
        {
            foreach (var item in Source)
			{
				yield return new object[] { item };
			}
		}
    }

    public class PairList<T1, T2>
    {
        public PairList()
        {
            Source = new List<Pair<T1, T2>>();
        }

        private List<Pair<T1, T2>> Source { get; set; }

        public void Add(T1 param01, T2 param02)
        {
            Source.Add(new Pair<T1, T2>(param01, param02));
        }

        public void Clean()
        {
            Source.Clear();
        }

        public IEnumerable<object[]> Cast()
        {
            foreach (var item in Source)
			{
				yield return new object[] { item.Item1, item.Item2 };
			}
		}
    }

    public class PairList<T1, T2, T3>
    {
        public PairList()
        {
            Source = new List<Pair<T1, T2, T3>>();
        }

        private List<Pair<T1, T2, T3>> Source { get; set; }

        public void Add(T1 param01, T2 param02, T3 param03)
        {
            Source.Add(new Pair<T1, T2, T3>(param01, param02, param03));
        }

        public void Clean()
        {
            Source.Clear();
        }

        public IEnumerable<object[]> Cast()
        {
            foreach (var item in Source)
			{
				yield return new object[] { item.Item1, item.Item2, item.Item3 };
			}
		}
    }

    public class PairList<T1, T2, T3, T4>
    {
        public PairList()
        {
            Source = new List<Pair<T1, T2, T3, T4>>();
        }

        private List<Pair<T1, T2, T3, T4>> Source { get; set; }

        public void Add(T1 param01, T2 param02, T3 param03, T4 param04)
        {
            Source.Add(new Pair<T1, T2, T3, T4>(param01, param02, param03, param04));
        }

        public void Clean()
        {
            Source.Clear();
        }

        public IEnumerable<object[]> Cast()
        {
            foreach (var item in Source)
			{
				yield return new object[] { item.Item1, item.Item2, item.Item3, item.Item4 };
			}
		}
    }

    public class PairList<T1, T2, T3, T4, T5>
    {
        public PairList()
        {
            Source = new List<Pair<T1, T2, T3, T4, T5>>();
        }

        private List<Pair<T1, T2, T3, T4, T5>> Source { get; set; }

        public void Add(T1 param01, T2 param02, T3 param03, T4 param04, T5 param05)
        {
            Source.Add(new Pair<T1, T2, T3, T4, T5>(param01, param02, param03, param04, param05));
        }

        public void Clean()
        {
            Source.Clear();
        }

        public IEnumerable<object[]> Cast()
        {
            foreach (var item in Source)
			{
				yield return new object[] { item.Item1, item.Item2, item.Item3, item.Item4, item.Item5 };
			}
		}
    }
}
