using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tools.Observables
{
    public class Signal : Signal<bool>
    {
        public void Fire() => base.Fire(true);
        public void Listen(Action action) => base.Listen(b => action());
        [Obsolete("Use Fire()", true)] public new void Fire(bool b) { }
        [Obsolete("Use Listen(action)", true)] public new void Listen(Action<bool> action) { }
    }

    public class Signal<T>
    {
        protected Dictionary<object, Action<T>> Listeners { get; } = new Dictionary<object, Action<T>>();

        public void Fire(T arg)
        {
            var toRemove = new List<object>();

            foreach (var target in Listeners.Keys)
            {
                if (target == null || target.Equals(null))
                {
                    toRemove.Add(target);
                    continue;
                }

                if (target is MonoBehaviour monoBeh && !monoBeh.gameObject.activeInHierarchy) continue;

                Listeners[target].Invoke(arg);
            }

            toRemove.ForEach(o => Listeners.Remove(o));
        }

        public void Listen(Action<T> action)
        {
            Listeners.Add(action.Target, action);
        }

        public void Unsubscribe(Action<T> action)
        {
            Listeners.Remove(action.Target);
        }
    }
}