using System;

namespace Core.Tools.Observables
{
    public class Command
    {
        private readonly Signal signal = new Signal();
        
        public void Listen(Action action)
        {
            signal.Clear();
            signal.Listen(action);
        }
        
        public void Fire() => signal.Fire();
    }

    public class Command<T>
    {
        private readonly Signal<T> signal = new Signal<T>();
        
        public void Listen(Action<T> action)
        {
            signal.Clear();
            signal.Listen(action);
        }
        
        public void Fire(T t) => signal.Fire(t);
    }
}