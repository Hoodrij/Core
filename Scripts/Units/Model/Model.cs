using System;
using Core.Tools;
using Core.Tools.Observables;

namespace Core.Units.Model
{
    public abstract class Model
    {
        protected Model()
        {
            Injector.Instance.Add(this);
            Injector.Instance.Populate(this);
        }
    }
    
    public abstract class Model<T> : Model, IObservable, Tools.Observables.IObservable<T>
    {
        private readonly Observable<T> value = new Observable<T>();

        public void Set(T t)
        {
            value.Set(t);
        }
        
        public void Listen(Action<T> callback, object target = null)
        {
            value.Listen(callback, target ?? callback.Target);
        }
        public void Listen(Action callback, object target = null)
        {
            value.Listen(t => callback(), target ?? callback.Target);   
        }
        
        public static implicit operator T(Model<T> model) => model.value;
        public static implicit operator string(Model<T> model) => model.value.ToString();

        public override string ToString()
        {
            return value.ToString();
        }
    }
}