using System.Reflection;
using UnityEngine;

namespace Core.Ui
{
    public abstract class UIRoot
    {
        public int Order { get; }
        public Transform Transform { get; internal set; }

        protected UIRoot()
        {
            UIRootInfoAttribute info = GetType().GetCustomAttribute<UIRootInfoAttribute>();

            Order = info.Order;
        }
    }
}