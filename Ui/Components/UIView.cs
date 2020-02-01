using System;
using System.Reflection;
using Bindings;
using Core.Utils.ExtensionMethods;
using UnityEngine;

namespace Core.Ui
{
	public abstract class UIView<TView, TData> : UIView where TView : UIView
	{
		protected new TData Data => (TData) base.Data;

		public static void Open(TData data)
		{
			UIInfoAttribute uiInfo = UIView<TView>.GetUIInfo();
			
		}
	}

	public abstract class UIView<TView> : UIView where TView : UIView
	{
		public static void Open()
		{
			UIInfoAttribute uiInfo = GetUIInfo();

		}
		
		protected internal static UIInfoAttribute GetUIInfo()
		{
			Type type = typeof(TView);
			UIInfoAttribute uiInfo = type.GetCustomAttribute<UIInfoAttribute>();
			return uiInfo;
		}
	} 

	[RequireComponent(typeof(UICloseEventComponent))]
	public abstract class UIView : APropertyBindableBehaviour
	{
		protected object Data { get; private set; }
		
		internal Action CloseAction;

		internal void Open(object data)
		{
			Data = data;
			Game.Injector.Inject(this);

			UICloseEventComponent closeEvent = GetComponent<UICloseEventComponent>();
			closeEvent.ListenCloseClick(Close);

			OnOpen();
			RebindAll();
		}

		protected virtual void OnOpen() { }
		internal virtual void OnClose() { }

		public void Close()
		{
			UICloseDelayer closeDelayer = GetComponent<UICloseDelayer>();
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