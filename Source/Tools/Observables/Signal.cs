using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tools.Observables
{
    public class Signal
    {
        protected Dictionary<object, Action> Listeners { get; } = new Dictionary<object, Action>();
        
        public void Fire()
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
        
                Listeners[target].Invoke();
            }
        
            toRemove.ForEach(o => Listeners.Remove(o));
        }
        
        public void Listen(Action action)
        {
            Listeners.Add(action.Target, action);
        }
        
        public void Unsubscribe(Action action)
        {
            Listeners.Remove(action.Target);
        }
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