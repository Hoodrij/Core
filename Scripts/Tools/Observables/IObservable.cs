using System;

namespace Core.Tools.Observables
{
    public interface IObservable
    {
        public void Listen(Action action, object target = null);
        public void Unsubscribe(object owner);
    }
    public interface IObservable<T>
    {
        public void Listen(Action<T> action, object target = null);
        public void Unsubscribe(object owner);
    }
}