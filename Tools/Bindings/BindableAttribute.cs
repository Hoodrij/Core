using System;

namespace Core.Tools.Bindings
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class BindableAttribute : Attribute
    {
        public Type ReturnType;

        public BindableAttribute() { }

        public BindableAttribute(Type argumentType)
        {
            ArgumentType = argumentType;
        }

        public Type ArgumentType { get; }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class BindableDescAttribute : Attribute
    {
        public BindableDescAttribute(string desc)
        {
            Description = desc;
        }

        public BindableDescAttribute(string desc, bool isWarning)
        {
            Description = desc;
            IsWarning = isWarning;
        }

        public string Description { get; }

        public bool IsWarning { get; }
    }
}