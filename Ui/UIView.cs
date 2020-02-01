using System;
using Bindings;
using UnityEngine;

namespace Core.Ui
{
	public abstract class UIView<TData> : UIView
	{
		protected new TData Data => (TData) base.Data;
	}

	[RequireComponent(typeof(UICloseEventComponent))]
	public abstract class UIView : APropertyBindableBehaviour
	{
		public UIInfo Info { get; private set; }
		internal Action CloseAction;

		protected object Data { get; private set; }

		private UICloseDelayer closeDelayer;

		internal void Open(object data, UIInfo info)
		{
			Data = data;
			Info = info;

			UICloseEventComponent closeEvent = GetComponent<UICloseEventComponent>();
			closeEvent.ListenCloseClick(Close);

			closeDelayer = GetComponent<UICloseDelayer>();

			Game.Injector.Inject(this);
			OnOpen();
			RebindAll();
		}

		protected virtual void OnOpen()
		{
		}

		internal virtual void OnClose()
		{
		}

		public void Close()
		{
			if (closeDelayer == null)
			{
				CloseAction.Invoke();
			}
			else
			{
				closeDelayer.BeginClose(CloseAction);
			}
		}
	}
}