using System;

namespace Core.Tools.Observables
{
    [Serializable] 
    public class Observable<T> : IObservable<T>
    {
        private T value;
        private Event<T> @event = new Event<T>();

        public Observable() { }

        public Observable(T value = default)
        {
            this.value = value;
        }
        
        public void Set(T newValue)
        {
            if (value != null && value.Equals(newValue)) return;

            value = newValue;
            @event.Fire(value);
        }

        public void Listen(Action<T> action, object target = null)
        {
            @event.Listen(action, target ?? action.Target);
        }

        public static implicit operator T(Observable<T> observable)
        {
            return observable.value;
        }

        public bool Equals(Observable<T> other)
        {
            return other.value.Equals(value);
        }

        public override bool Equals(object other)
        {
            return other is Observable<T> observable && observable.value.Equals(value);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}