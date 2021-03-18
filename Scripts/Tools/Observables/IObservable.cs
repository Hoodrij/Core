using System;

namespace Core.Tools.Observables
{
    public interface IObservable
    {
        public void Listen(Action callback, object target = null);
    }
    public interface IObservable<T>
    {
        public void Listen(Action<T> callback, object target = null);
    }
}