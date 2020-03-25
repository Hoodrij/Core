﻿﻿using System;
using System.Reflection;
  using System.Threading.Tasks;
  using Core.Tools.Bindings;
  using UnityEngine;

namespace Core.Ui
{
	public abstract class UIView<TView, TData> : UIView<TView> where TView : UIView
	{
		protected TData Data => (TData) base.data;

		public static async Task<TView> Open(TData data)
		{
			return await Game.UI.Open<TView>(data);
		}
		
		[Obsolete("Requires Data", true)]
		public new static void Open() { }
	}

	public abstract class UIView<TView> : UIView where TView : UIView
	{
		internal static UIInfoAttribute Info => typeof(TView).GetCustomAttribute<UIInfoAttribute>();
		
		public static async Task<TView> Open()
		{
			return await Game.UI.Open<TView>();
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