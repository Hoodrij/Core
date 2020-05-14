using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class)]
    public class BindToAttribute : PropertyAttribute
    {
        public Type BindToType;

        public BindToAttribute(Type bindToType)
        {
            BindToType = bindToType;
        }
    }
}