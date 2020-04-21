using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Core.Ui
{
    public abstract class UIRoot
    {
        public Transform Transform { get; internal set; }
        private Type[] rootsToClose;

        protected UIRoot()
        {
            rootsToClose = new[] {GetType()};

            UIRootCloseParamsAttribute paramsAttribute = GetType().GetCustomAttribute<UIRootCloseParamsAttribute>();
            if (paramsAttribute != null)
                rootsToClose = rootsToClose
                    .Concat(paramsAttribute.RootsToClose)
                    .ToArray();
        }

        public bool IsClosingOther(UIRoot other)
        {
            return rootsToClose.Contains(other.GetType());
        }
    }
}