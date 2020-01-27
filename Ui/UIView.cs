using System;
using Bindings;
using UnityEngine;

namespace Core.Ui
{
	public abstract class UIView<TData> : UIView where TData : UIData
	{
		protected new TData Data => base.Data as TData;
	}

	[RequireComponent(typeof(UICloseEventComponent))]
	public abstract class UIView : APropertyBindableBehaviour
	{
		public UIInfo Info { get; private set; }
		internal Action CloseAction;

		protected UIData Data { get; private set; }

		private UICloseDelayer closeDelayer;

		internal void Open(UIData data, UIInfo info)
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