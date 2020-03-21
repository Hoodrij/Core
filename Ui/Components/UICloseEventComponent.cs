using System;
using Core.Tools.Observables;
using UnityEngine;

namespace Core.Ui
{
	public class UICloseEventComponent : MonoBehaviour
	{
		private Signal OnClose = new Signal();

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
