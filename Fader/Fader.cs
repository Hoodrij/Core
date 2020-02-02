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
					if (view == null || view.IsShown)
					{
						var action = actions.Dequeue();
						action.Invoke(HideView);
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
			onCompleted?.Listen(HideView);
			actions.Enqueue(new FaderAsyncAction(action, onCompleted));
			ShowView();
		}

		private void ShowView()
		{
			if (view == null) return;
			if (!actions.IsEmpty() && !view.IsShown)
			{
				view.ShowView();
			}
		}

		private void HideView()
		{
			if (view == null) return;
			if (actions.IsEmpty())
			{
				view.HideView();
			}
		}
	}
}