using System;
using Core.Tools.Observables;

namespace Core.Units.Model
{
    public class Model<T>
    {
        private T t;
        private readonly Signal<T> signal = new Signal<T>();

        public void Set(T t)
        {
            this.t = t;
            signal.Fire(t);
        }

        public void Listen(Action<T> callback)
        {
            signal.Listen(callback, callback.Target);
        }
        public void Listen(Action callback)
        {
            signal.Listen(value => callback(), callback.Target);   
        }
        
        public static implicit operator T(Model<T> model) => model.t;
        public static implicit operator string(Model<T> model) => model.t.ToString();

        public override string ToString()
        {
            return t.ToString();
        }
    }
}