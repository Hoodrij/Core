using System;

namespace Core.Tools.Observables
{
	public class Command : Signal
	{
		public new void Listen(Action action)
		{
			Listeners.Clear();
			base.Listen(action);
		}
	}

	public class Command<T1> : Signal<T1>
	{
		public new void Listen(Action<T1> action)
		{
			Listeners.Clear();
			base.Listen(action);
		}
	}
}