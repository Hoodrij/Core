﻿using System;

namespace Core.Tools.Observables
{
    [Serializable] 
    public class Observable<T>
    {
        public T Value
        {
            get => value;
            set
            {
                if (this.value != null && this.value.Equals(value)) return;

                this.value = value;
                @event.Fire(value);
            }
        }

        private T value;
        private Event<T> @event = new Event<T>();

        public Observable() { }

        public Observable(T value = default)
        {
            this.value = value;
        }

        public void Listen(Action<T> action)
        {
            @event.Listen(action);
        }

        public static implicit operator T(Observable<T> observable)
        {
            return observable.Value;
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
    }
}