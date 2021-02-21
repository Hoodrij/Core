using System;
using System.Collections.Generic;

namespace Core.Ui
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UIRootInfoAttribute : Attribute
    {
        public readonly int Order;
        private readonly List<Type> rootsToClose = new List<Type>();

        public UIRootInfoAttribute(Type selfType, int order)
        {
            Order = order;
            rootsToClose.Add(selfType);
        }

        public UIRootInfoAttribute(Type selfType, int order, params Type[] close) : this(selfType, order)
        {
            rootsToClose.AddRange(close);
        }
        
        public bool IsClosingOther(Type other)
        {
            return rootsToClose.Contains(other);
        }
    }
}