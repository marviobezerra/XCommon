using System;

namespace XCommon.Extensions.Util
{
    public class Indexer<TIndex, TValue>
    {
        private readonly Func<TIndex, TValue> _Getter;
        private readonly Action<TIndex, TValue> _Setter;

        public Indexer(Func<TIndex, TValue> getter, Action<TIndex, TValue> setter)
        {
            _Getter = getter;
            _Setter = setter;
        }

        public TValue this[TIndex i]
        {
            get
            {
                return _Getter(i);
            }
            set
            {
                _Setter(i, value);
            }
        }
    }
}
