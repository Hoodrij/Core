using System;
using System.Collections.Generic;
using System.Linq;
using Core.Tools;

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
        
        protected void Foreach<T>(Action<T> func) where T : AComponent
        {
            List<T> list = World.GetComponentsList<T>();

            foreach (T t in list)
            {
                func.Invoke(t);
            }
        }
    }
}