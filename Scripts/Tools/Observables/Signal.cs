using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using Core.Tools.ExtensionMethods;
using Core.Tools.Pool;
using UnityEngine;

namespace Core.Tools.Observables
{
    public class Signal
    {
        private readonly HashSet<WeakAction> listeners = new HashSet<WeakAction>();
    
        public void Fire()
        {
            listeners.RemoveWhere(weakAction => !weakAction.IsAlive);
    
            foreach (WeakAction weakAction in listeners)
            {
                weakAction.Invoke();
            }
        }
    
        public void Listen(Action action) => listeners.Add(new WeakAction(action));
    
        public void Unsubscribe(Action action) => listeners.RemoveWhere(weakAction => weakAction.Equals(action));
    
        public void Clear() => listeners.Clear();
    }

    public class Signal<T>
    {
        private readonly HashSet<WeakAction<T>> listeners = new HashSet<WeakAction<T>>();

        public void Fire(T t)
        {
            listeners.RemoveWhere(weakAction => !weakAction.IsAlive);

            foreach (WeakAction<T> weakAction in listeners)
            {
                weakAction.Invoke(t);
            }
        }

        public void Listen(Action<T> action) => listeners.Add(new WeakAction<T>(action));

        public void Unsubscribe(Action<T> action) => listeners.RemoveWhere(weakAction => weakAction.Equals(action));

        public void Clear() => listeners.Clear();
    }
}