using System;
using System.Collections;
using System.Collections.Generic;
using Core.Samples.Fader;
using Core.Tools.ExtensionMethods;
using Core.Tools.Observables;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
	public class Fader
	{
		private Queue<FaderAsyncAction> actions = new Queue<FaderAsyncAction>();
		private IFaderView view;

		public Fader()
		{
			SetView(Game.Assets.Spawn<IFaderView>("BaseFaderView", true));
			
			Game.Coroutiner.Start(Worker());
		}

		IEnumerator Worker()
		{
			while (true)
			{
				if (!actions.IsEmpty())
				{
					if (view != null)
					{
						yield return Game.Coroutiner.Start(view.WaitForShown());
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

		public void AddAction(Action action, Signal onCompleted = null)
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