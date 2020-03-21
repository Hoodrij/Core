using System.Collections;
using System.Collections.Generic;

namespace Core.Tools.Bindings
{
  public struct Collection : IEnumerable
  {
    public static Collection Default = new Collection();

    private readonly IEnumerable _collection;
    private readonly IEqualityComparer<object> _comparer;

    public IEqualityComparer<object> Comparer
    {
      get { return _comparer; }
    }

    public bool IsEmpty
    {
      get { return _collection == null || !_collection.GetEnumerator().MoveNext(); }
    }

    public Collection(IEnumerable collection, IEqualityComparer<object> comparer = null)
    {
      _collection = collection;
      _comparer = comparer;
    }

    public IEnumerator GetEnumerator()
    {
      return _collection.GetEnumerator();
    }
  }
}