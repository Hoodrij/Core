using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Tools.ExtensionMethods;
using UnityAsync;

namespace Core.Tools.Collections
{
    public class SafeHashSet<T>
    {
        private readonly HashSet<T> collection = new HashSet<T>();
        private bool isIterating;

        public void ForEach(Action<T> action)
        {
            isIterating = true;
            collection.ForEach(action);
            isIterating = false;
        }
        
        public async void Add(T item)
        {
            await WaitUnlock();
            collection.Add(item);
        }

        public async void RemoveWhere(Predicate<T> match)
        {
            await WaitUnlock();
            collection.RemoveWhere(match);
        }

        public async void Clear()
        {
            await WaitUnlock();
            collection.Clear();
        }

        private async Task WaitUnlock()
        {
            if (isIterating)
                await Wait.While(() => isIterating);
        }
    }
}