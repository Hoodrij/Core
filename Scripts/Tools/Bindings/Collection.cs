using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tools.Bindings
{
    public struct Collection<TComponent, TData> : IEnumerable<TData>
    {
        public delegate void CollectionSetup(TComponent item, TData data, bool isNew);
        private readonly IEnumerable<TData> _collection;

        public CollectionSetup SetupAction { get; }
        public bool IsEmpty => _collection == null || !_collection.GetEnumerator().MoveNext();

        public Collection(IEnumerable<TData> collection, CollectionSetup setupAction)
        {
            _collection = collection;
            SetupAction = setupAction;
        }

        IEnumerator<TData> IEnumerable<TData>.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
}