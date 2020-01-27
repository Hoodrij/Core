using System;
using UnityEngine;

namespace Bindings
{
  [BindTo(typeof(Enum))]
  public class ShaderColorEnumBinder : GraphicColorEnumBinder
  {
#pragma warning disable 649
    [SerializeField] private string _propertyName;
#pragma warning restore 649

#pragma warning disable 649
    [SerializeField] private Renderer _rendererWidget;
#pragma warning restore 649

    protected override void Bind(Boolean init)
    {
      var baseEnum = _getter();
      var valueName = Enum.GetName(baseEnum.GetType(), baseEnum);

      foreach (var bindingPair in _stateObjects)
        if (bindingPair.EnumVal == valueName)
        {
          _rendererWidget.material.SetColor(_propertyName, bindingPair.Color);
        }
    }
  }
}