using System;

namespace Core.Utils.Observables
{
	public class Command : Event
	{
		public new void Listen(Action action)
		{
			Listeners.Clear();
			base.Listen(action);
		}
	}

	public class Command<T1> : Event<T1>
	{
		public new void Listen(Action<T1> action)
		{
			Listeners.Clear();
			base.Listen(action);
		}
	}
}