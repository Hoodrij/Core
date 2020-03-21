using System;
using Core.Tools.Observables;

namespace Core
{
	public class FaderAsyncAction
	{
		private Action action;
		private Signal onCompleted;
		private bool actionInvoked;

		public FaderAsyncAction(Action action, Signal onCompleted)
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