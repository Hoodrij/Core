using System;
using System.Collections.Generic;
using Core.Tools.ExtensionMethods;

namespace Core.Tools.Observables
{
    public class Signal
    {
        private readonly HashSet<WeakAction> listeners = new HashSet<WeakAction>();
        public int ListenersCount => listeners.Count;
    
        public void Fire()
        {
            listeners.RemoveWhere(action => !action.IsAlive);
            listeners.ForEach(action => action.Invoke());
        }
    
        public void Listen(Action action) => listeners.Add(new WeakAction(action));
        public void Listen(Action action, object owner) => listeners.Add(new WeakAction(action, owner));
    
        public void Unsubscribe(Action action) => listeners.RemoveWhere(weakAction => weakAction.Equals(action));
    
        public void Clear() => listeners.Clear();
    }

    public class Signal<T>
    {
        private readonly HashSet<WeakAction<T>> listeners = new HashSet<WeakAction<T>>();

        public void Fire(T t)
        {
            listeners.RemoveWhere(action => !action.IsAlive);
            listeners.ForEach(action => action.Invoke(t));
        }

        public void Listen(Action<T> action) => listeners.Add(new WeakAction<T>(action));
        public void Listen(Action<T> action, object owner) => listeners.Add(new WeakAction<T>(action, owner));

        public void Unsubscribe(Action<T> action) => listeners.RemoveWhere(weakAction => weakAction.Equals(action));

        public void Clear() => listeners.Clear();
    }
}