using System;
using System.Collections;
using System.Collections.Generic;
using Core.Utils.ExtensionMethods;
using UnityEditor;
using UnityEngine;
using Event = Core.Utils.Observables.Event;
using Object = UnityEngine.Object;

namespace Core
{
	public class Fader
	{
		private Queue<PreloaderAsyncAction> actions = new Queue<PreloaderAsyncAction>();
		private IFaderView view;

		public Fader()
		{
			Game.Coroutiner.StartCoroutine(Worker());
		}

		IEnumerator Worker()
		{
			while (true)
			{
				if (!actions.IsEmpty())
				{
					if (view == null || view.IsShown)
					{
						var action = actions.Dequeue();
						action.Invoke(EndInternal);
					}
				}
				yield return null;
			}
		}

		public void SetView(IFaderView view)
		{
			this.view = view;
		}

		public void AddAction(Action action, Event onCompleted = null)
		{
			onCompleted?.Listen(EndInternal);
			actions.Enqueue(new PreloaderAsyncAction(action, onCompleted));
			StartInternal();
		}

		private void StartInternal()
		{
			if (view == null) return;
			if (!actions.IsEmpty() && !view.IsShown)
			{
				view.ShowView();
			}
		}

		private void EndInternal()
		{
			if (view == null) return;
			if (actions.IsEmpty())
			{
				view.HideView();
			}
		}

		private class PreloaderAsyncAction
		{
			private Action action;
			private Event onCompleted;
			private bool actionInvoked;

			public PreloaderAsyncAction(Action action, Event onCompleted)
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
}