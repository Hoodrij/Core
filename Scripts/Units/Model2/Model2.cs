using System;
using Core.Tools.Observables;

namespace Core.Units.Model2
{
    public class Model2<T>
    {
        private T t;
        private readonly Signal<T> signal = new Signal<T>();

        public void Set(T t)
        {
            this.t = t;
        }

        public void Listen(Action<T> callback)
        {
            signal.Listen(callback);
        }
        public void Listen(Action callback)
        {
            signal.Listen(value => callback(), callback.Target);   
        }
        
        public static implicit operator T(Model2<T> model) => model.t;

        public override string ToString()
        {
            return t.ToString();
        }
    }
}