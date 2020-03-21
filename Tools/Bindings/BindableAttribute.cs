using System;

namespace Core.Tools.Bindings
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
  public class BindableAttribute : Attribute
  {
    private Type _argumentType;

    public Type ArgumentType
    {
      get { return _argumentType; }
    }

    public Type ReturnType;

    public BindableAttribute()
    {
    }

    public BindableAttribute(Type argumentType)
    {
      _argumentType = argumentType;
    }
  }

  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
  public class BindableDescAttribute : Attribute
  {
    private String _description;
    private Boolean _isWarning;

    public string Description
    {
      get { return _description; }
    }

    public bool IsWarning
    {
      get { return _isWarning; }
    }

    public BindableDescAttribute(String desc)
    {
      _description = desc;
    }

    public BindableDescAttribute(String desc, Boolean isWarning)
    {
      _description = desc;
      _isWarning = isWarning;
    }
  }
}