using System;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
public class BindAttribute : Attribute
{
    public Type ReturnType;

    public BindAttribute() { }

    public BindAttribute(Type argumentType)
    {
        ArgumentType = argumentType;
    }

    public Type ArgumentType { get; }
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
public class BindDescAttribute : Attribute
{
    public BindDescAttribute(string desc)
    {
        Description = desc;
    }

    public BindDescAttribute(string desc, bool isWarning)
    {
        Description = desc;
        IsWarning = isWarning;
    }

    public string Description { get; }

    public bool IsWarning { get; }
}