using System;
using System.Reflection;
using Core.Tools.Bindings;
using UnityEngine;

namespace Core.Ui
{
    public abstract class UIView<TView, TData> : UIView<TView> where TView : UIView
    {
        protected TData Data => (TData) data;

        // public static async Task<TView> Open(TData data) => await Game.UI.Open<TView>(data);
        [Obsolete("Requires Data", true)] public new static void Open() { }
    }

    public abstract class UIView<TView> : UIView where TView : UIView
    {
        internal static UIInfoAttribute Info => typeof(TView).GetCustomAttribute<UIInfoAttribute>();

        // public static async Task<TView> Open() => await Game.UI.Open<TView>();
        // public static TView Get() => (TView) Game.UI.Get<TView>();
    }

    [RequireComponent(typeof(UICloseEventComponent))]
    public abstract class UIView : APropertyBindableBehaviour
    {
        internal UIInfoAttribute Info => GetType().GetCustomAttribute<UIInfoAttribute>();
        internal Action CloseAction;

        internal object data;

        internal void Initialize(object data)
        {
            // Models.Populate(this);
            this.data = data;

            UICloseEventComponent closeEvent = GetComponent<UICloseEventComponent>();
            closeEvent.ListenCloseClick(Close);

            OnOpen();
            RebindAll();
        }

        protected virtual void OnOpen() { }
        internal virtual void OnClose() { }

        public async void Close()
        {
            UICloseDelayer closeDelayer = GetComponent<UICloseDelayer>();
            if (closeDelayer != null)
            {
                await closeDelayer.BeginClose();
            }
            CloseAction();
        }
    }
}