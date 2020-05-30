using System;
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
        internal Action CloseAction;

        internal object data;

        internal void Initialize(object data)
        {
            Injector.Instance.Populate(this);
            this.data = data;

            OnOpen();
            RebindAll();
        }

        protected virtual void OnOpen() { }
        internal virtual void OnClose() { }

        public async void Close()
        {
            UICloseDelayerComponent closeDelayer = GetComponent<UICloseDelayerComponent>();
            if (closeDelayer != null)
            {
                await closeDelayer.WaitClose();
            }

            CloseAction?.Invoke();
        }
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