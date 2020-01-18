using System;

namespace Core.Utils.Observables
{
	public class Command : Event
	{
		public new void Listen(Action action)
		{
			eventHandlers.Clear();
			base.Listen(action);
		}
	}

	public class Command<T1> : Event<T1>
	{
		public new void Listen(Action<T1> action)
		{
			eventHandlers.Clear();
			base.Listen(action);
		}
	}

	public class Command<T1, T2> : Event<T1, T2>
	{
		public new void Listen(Action<T1, T2> action)
		{
			eventHandlers.Clear();
			base.Listen(action);
		}
	}
}