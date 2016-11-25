using System.Collections.Generic;

namespace XCommon.Util
{
    public class PairList<T1, TResult>
    {
        public PairList()
        {
            Source = new List<Pair<T1, TResult, string>>();
        }

        private List<Pair<T1, TResult, string>> Source { get; set; }

        public void Add(T1 param01, TResult result, string message = "")
        {
            Source.Add(new Pair<T1, TResult, string>(param01, result, message));
        }

        public void Clean()
        {
            Source.Clear();
        }

        public IEnumerable<object[]> Cast()
        {
            foreach (var item in Source)
                yield return new object[] { item.Item1, item.Item2, item.Item3 };
        }
    }

    public class PairList<T1, T2, TResult>
    {
        public PairList()
        {
            Source = new List<Pair<T1, T2, TResult, string>>();
        }

        private List<Pair<T1, T2, TResult, string>> Source { get; set; }

        public void Add(T1 param01, T2 param02, TResult result, string message = "")
        {
            Source.Add(new Pair<T1, T2, TResult, string>(param01, param02, result, message));
        }

        public void Clean()
        {
            Source.Clear();
        }

        public IEnumerable<object[]> Cast()
        {
            foreach (var item in Source)
                yield return new object[] { item.Item1, item.Item2, item.Item3, item.Item4 };
        }
    }

    public class PairList<T1, T2, T3, TResult>
    {
        public PairList()
        {
            Source = new List<Pair<T1, T2, T3, TResult, string>>();
        }

        private List<Pair<T1, T2, T3, TResult, string>> Source { get; set; }

        public void Add(T1 param01, T2 param02, T3 param03, TResult result, string message = "")
        {
            Source.Add(new Pair<T1, T2, T3, TResult, string>(param01, param02, param03, result, message));
        }

        public void Clean()
        {
            Source.Clear();
        }

        public IEnumerable<object[]> Cast()
        {
            foreach (var item in Source)
                yield return new object[] { item.Item1, item.Item2, item.Item3, item.Item4, item.Item5 };
        }
    }
}
