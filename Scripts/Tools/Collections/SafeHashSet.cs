﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityAsync;

namespace Core.Tools.Collections
{
    public class SafeHashSet<T>
    {
        private readonly HashSet<T> collection = new HashSet<T>();
        private readonly HashSet<T> markedForRemoving = new HashSet<T>();
        private bool isIterating;

        public void ForEach(Action<T> action)
        {
            isIterating = true;
            foreach (T t in collection)
            {
                if (!markedForRemoving.Contains(t)) 
                    action(t);
            }
            isIterating = false;
        }
        
        public async void Add(T item)
        {
            await WaitUnlock();
            collection.Add(item);
        }

        public async void RemoveWhere(Predicate<T> match)
        {
            foreach (T t in collection)
            {
                if (match.Invoke(t))
                    markedForRemoving.Add(t);
            }
            await WaitUnlock();
            collection.RemoveWhere(match);
            markedForRemoving.Clear();
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