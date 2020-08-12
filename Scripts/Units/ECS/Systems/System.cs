using System;
using System.Collections.Generic;
using System.Linq;
using Core.Tools;
using UnityEngine;

namespace Core.ECS
{
    public abstract class System
    {
        private World World => Injector.Instance.Get<World>();
        
        protected internal abstract void Update();

        protected bool Get<T>(out T result) where T : AComponent
        {
            Type type = typeof(T);
            result = null;
            List<T> list = World.GetComponentsList<T>();
            
            result = list.FirstOrDefault();
            return result;
        }
        
        protected void Foreach<T>(Action<T> action) where T : AComponent
        {
            List<T> list = World.GetComponentsList<T>();
        
            foreach (T t in list)
            {
                action.Invoke(t);
            }
        }

        protected void Foreach<T, T2>(Action<T, T2> action) where T : AComponent where T2 : Component
        {
            List<T> list = World.GetComponentsList<T>();
        
            foreach (T t in list)
            {
                T2 t2 = t.Entity.Get<T2>();
                if (!t2) continue;
                
                action.Invoke(t, t2);
            }
        }
    }
}