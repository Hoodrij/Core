using System;
using Core.Utils.Observables;

namespace Core
{
	public class FaderAsyncAction
	{
		private Action action;
		private Event onCompleted;
		private bool actionInvoked;

		public FaderAsyncAction(Action action, Event onCompleted)
		{
			this.action = action;
			this.onCompleted = onCompleted;
		}

		public void Invoke(Action callback)
		{
			if (actionInvoked) return;
			actionInvoked = true;

			action?.Invoke();

			if (onCompleted == null)
			{
				callback();
			}
		}
	}
}