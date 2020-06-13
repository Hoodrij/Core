﻿using System;
using System.Reflection;
using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.Bindings;
using Core.Ui.Components;
using Core.Units;
using UnityEngine;

namespace Core.Ui
{
    [DisallowMultipleComponent] 
    public abstract class UIView : ABindableBehaviour
    {
        internal UIInfoAttribute Info => GetType().GetCustomAttribute<UIInfoAttribute>();
        internal Action CloseInstructions;

        internal object data;

        internal void Open(object data)
        {
            Injector.Instance.Populate(this);
            this.data = data;

            OnOpen();
            RebindAll();
        }

        public async void Close()
        {
            UICloseDelayerComponent closeDelayer = GetComponent<UICloseDelayerComponent>();
            if (closeDelayer != null)
            {
                await closeDelayer.WaitClose();
            }

            CloseInstructions.Invoke();
        }
        
        protected virtual void OnOpen() { }
        internal virtual void OnClose() { }
    }

    #region Extended generics

    public abstract class UIView<TView> : UIView where TView : UIView
    {
        internal static UIInfoAttribute Info => typeof(TView).GetCustomAttribute<UIInfoAttribute>();

        public static async Task<TView> Open() => await UI.Instance.Open<TView>();
        public static TView Get() => (TView) UI.Instance.Get<TView>();
    }

    public abstract class UIView<TView, TData> : UIView<TView> where TView : UIView
    {
        protected TData Data => (TData) data;

        public static async Task<TView> Open(TData data) => await UI.Instance.Open<TView>(data);
        [Obsolete("Requires Data", true)] public new static void Open() { }
    }

    #endregion
}