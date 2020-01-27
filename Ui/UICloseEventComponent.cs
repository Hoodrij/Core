using System;
using UnityEngine;
using Event = Core.Utils.Observables.Event;

namespace Core.Ui
{
	public class UICloseEventComponent : MonoBehaviour
	{
		private Event OnClose = new Event();

		internal void ListenCloseClick(Action action)
		{
			OnClose.Listen(action);
		}

		// button inspector
		public void Close()
		{
			OnClose.Fire();
		}
	}
}
