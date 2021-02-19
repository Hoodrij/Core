using System;
using System.Collections.Generic;
using Core.Tools.ExtensionMethods;

namespace Core.Tools.Observables
{
    public class Event
    {
        private readonly HashSet<WeakAction> listeners = new HashSet<WeakAction>();
        private readonly HashSet<WeakAction> oneshotListeners = new HashSet<WeakAction>();
    
        public void Fire()
        {
            listeners.RemoveWhere(action => !action.IsAlive);
            listeners.ForEach(action => action.Invoke());
            
            oneshotListeners.ForEach(action => { if (action.IsAlive) action.Invoke(); });
            oneshotListeners.Clear();
        }
    
        public void Listen(Action action) => listeners.Add(new WeakAction(action));
        public void Listen(Action action, object owner) => listeners.Add(new WeakAction(action, owner));
        internal void ListenOneshot(Action action) => oneshotListeners.Add(new WeakAction(action));
        internal void ListenOneshot(Action action, object owner) => oneshotListeners.Add(new WeakAction(action, owner));
    
        public void Unsubscribe(Action action) => listeners.RemoveWhere(weakAction => weakAction.Equals(action));
    
        public void Clear() => listeners.Clear();
    }

    public class Event<T>
    {
        private readonly HashSet<WeakAction<T>> listeners = new HashSet<WeakAction<T>>();
        private readonly HashSet<WeakAction<T>> oneshotListeners = new HashSet<WeakAction<T>>();

        public void Fire(T t)
        {
            listeners.RemoveWhere(action => !action.IsAlive);
            listeners.ForEach(action => action.Invoke(t));
            
            oneshotListeners.ForEach(action => { if (action.IsAlive) action.Invoke(t); });
            oneshotListeners.Clear();
        }

        public void Listen(Action<T> action) => listeners.Add(new WeakAction<T>(action));
        public void Listen(Action<T> action, object owner) => listeners.Add(new WeakAction<T>(action, owner));
        internal void ListenOneshot(Action<T> action) => oneshotListeners.Add(new WeakAction<T>(action));
        internal void ListenOneshot(Action<T> action, object owner) => oneshotListeners.Add(new WeakAction<T>(action, owner));

        public void Unsubscribe(Action<T> action) => listeners.RemoveWhere(weakAction => weakAction.Equals(action));

        public void Clear() => listeners.Clear();
    }
}