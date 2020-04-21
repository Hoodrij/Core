using System;
using System.Collections.Generic;
using Core.Tools.ExtensionMethods;
using Core.Tools.Pool;
using UnityEngine;

namespace Core.Tools.Observables
{
    public class Signal
    {
        protected Dictionary<object, Action> Listeners { get; } = new Dictionary<object, Action>();
        
        public void Fire()
        {
            List<object> toRemove = ListPool<object>.Get();
        
            foreach (object target in Listeners.Keys)
            {
                if (target == null || target.Equals(null))
                {
                    toRemove.Add(target);
                    continue;
                }
        
                if (target is MonoBehaviour monoBeh && !monoBeh.gameObject.activeInHierarchy) continue;
        
                Listeners[target].Invoke();
            }
        
            toRemove.ForEach(o => Listeners.Remove(o));
            ListPool<object>.Release(toRemove);
        }
        
        public void Listen(Action action)
        {
            Listeners.Set(action.Target, action);
        }
        
        public void Unsubscribe(Action action) => Listeners.Remove(action.Target);

        public void Clear() => Listeners.Clear();
    }

    public class Signal<T>
    {
        protected Dictionary<object, Action<T>> Listeners { get; } = new Dictionary<object, Action<T>>();

        public void Fire(T arg)
        {
            List<object> toRemove = ListPool<object>.Get();

            foreach (object target in Listeners.Keys)
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
            ListPool<object>.Release(toRemove);
        }

        public void Listen(Action<T> action)
        {
            Listeners.Add(action.Target, action);
        }

        public void Unsubscribe(Action<T> action) => Listeners.Remove(action.Target);
        
        public void Clear() => Listeners.Clear();
    }
}