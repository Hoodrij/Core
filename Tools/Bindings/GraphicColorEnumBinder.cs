using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(Enum))]
  public class GraphicColorEnumBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private Graphic _widget;
#pragma warning restore 649

    [SerializeField] protected GraphicColorEnumBinder.EnumBindingPair[] _stateObjects;

    protected Func<Enum> _getter;

    protected override void Bind(Boolean init)
    {
      var baseEnum = _getter();
      var valueName = Enum.GetName(baseEnum.GetType(), baseEnum);

      foreach (var bindingPair in _stateObjects)
        if (bindingPair.EnumVal == valueName)
        {
          _widget.color = bindingPair.Color;
        }
    }

    protected void Awake()
    {
      Init(ref _getter);
    }

    [Serializable]
    protected struct EnumBindingPair
    {
      public String EnumVal;
      public Color Color;
    }
  }
}