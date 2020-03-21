using System;

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
				signal.Fire(value);
			}
		}

		private T value;
		private Signal<T> signal = new Signal<T>();

		public Observable()
		{

		}

		public Observable(T value = default)
		{
			this.value = value;
		}

		public void Listen(Action<T> action)
		{
			signal.Listen(action);
		}

		public static implicit operator T(Observable<T> observable)
		{
			return observable.Value;
		}

		public static implicit operator Observable<T>(T observable)
		{
			return new Observable<T>(observable);
		}

		public bool Equals(Observable<T> other)
		{
			return other.value.Equals(value);
		}

		public override bool Equals(object other)
		{
			return other != null
				   && other is Observable<T>
				   && ((Observable<T>)other).value.Equals(value);
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}
	}
}