using System;
using Core.Tools;
using Core.Tools.Observables;

namespace Core.Units.Model
{
    public abstract class Model
    {
        protected Model()
        {
            Injector.Instance.Populate(this);
        }
    }
    
    public abstract class Model<T> : Model
    {
        private readonly Observable<T> value = new Observable<T>();

        public void Set(T t)
        {
            value.Set(t);
        }
        
        public void Listen(Action<T> callback)
        {
            value.Listen(callback);
        }
        public void Listen(Action callback)
        {
            value.Listen(t => callback(), callback.Target);   
        }
        
        public static implicit operator T(Model<T> model) => model.value;
        public static implicit operator string(Model<T> model) => model.value.ToString();

        public override string ToString()
        {
            return value.ToString();
        }
    }
}