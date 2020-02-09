using System;
using System.Collections;
using System.Collections.Generic;
using Core.Samples.Fader;
using Core.Utils.ExtensionMethods;
using UnityEditor;
using UnityEngine;
using Event = Core.Utils.Observables.Event;
using Object = UnityEngine.Object;

namespace Core
{
	public class Fader
	{
		private Queue<FaderAsyncAction> actions = new Queue<FaderAsyncAction>();
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
					if (view != null)
					{
						yield return Game.Coroutiner.StartCoroutine(view.WaitForShown());
					}
					
					var action = actions.Dequeue();
					action.Invoke(TryHideView);
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
			onCompleted?.Listen(TryHideView);
			actions.Enqueue(new FaderAsyncAction(action, onCompleted));
		}

		private void TryHideView()
		{
			if (view == null) return;
			if (actions.IsEmpty())
			{
				view.Hide();
			}
		}
	}
}