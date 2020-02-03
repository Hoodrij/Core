using System;
using System.Reflection;
using Bindings;
using Core.Utils.ExtensionMethods;
using UnityEngine;

namespace Core.Ui
{
	public abstract class UIView<TView, TData> : UIView<TView> where TView : UIView
	{
		protected TData Data => (TData) base.data;

		public static void Open(TData data, Action<TView> callback = null)
		{
			Game.UI.Open(data, callback);
		}
		
		[Obsolete("Need Data", true)]
		public new static void Open(Action<TView> callback = null) { }
	}

	public abstract class UIView<TView> : UIView where TView : UIView
	{
		internal static UIInfoAttribute Info => typeof(TView).GetCustomAttribute<UIInfoAttribute>();
		
		public static void Open(Action<TView> callback = null)
		{
			Game.UI.Open(null, callback);
		}
	} 

	[RequireComponent(typeof(UICloseEventComponent))]
	public abstract class UIView : APropertyBindableBehaviour
	{
		internal UIInfoAttribute Info => GetType().GetCustomAttribute<UIInfoAttribute>();
		internal Action CloseAction;
		
		internal object data;

		internal void Initialize(object data)
		{
			this.data = data;
			Game.Models.Populate(this);

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