﻿using System;

namespace Core.Utils.Observables
{
	[Serializable]
	public class ValueObservable<T>
	{
		public T Value
		{
			get => value;
			set
			{
				if (this.value != null && this.value.Equals(value)) return;

				this.value = value;
				signal.Fire(value);
			}
		}

		private T value;
		private Signal<T> signal = new Signal<T>();

		public ValueObservable()
		{

		}

		public ValueObservable(T value = default)
		{
			this.value = value;
		}

		public void Listen(Action<T> action)
		{
			signal.Listen(action);
		}

		public static implicit operator T(ValueObservable<T> observable)
		{
			return observable.Value;
		}

		public static implicit operator ValueObservable<T>(T observable)
		{
			return new ValueObservable<T>(observable);
		}

		public bool Equals(ValueObservable<T> other)
		{
			return other.value.Equals(value);
		}

		public override bool Equals(object other)
		{
			return other != null
				   && other is ValueObservable<T>
				   && ((ValueObservable<T>)other).value.Equals(value);
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}
	}
}