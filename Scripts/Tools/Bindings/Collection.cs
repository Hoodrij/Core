using System.Collections;
using System.Collections.Generic;

namespace Core.Tools.Bindings
{
    public struct Collection : IEnumerable
    {
        public static Collection Default = new Collection();

        private readonly IEnumerable _collection;

        public IEqualityComparer<object> Comparer { get; }

        public bool IsEmpty => _collection == null || !_collection.GetEnumerator().MoveNext();

        public Collection(IEnumerable collection, IEqualityComparer<object> comparer = null)
        {
            _collection = collection;
            Comparer = comparer;
        }

        public IEnumerator GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
}