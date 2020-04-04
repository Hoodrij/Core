using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(Enum))] public class ShaderColorEnumBinder : GraphicColorEnumBinder
    {
        [SerializeField] private string _propertyName;


        [SerializeField] private Renderer _rendererWidget;


        protected override void Bind(bool init)
        {
            var baseEnum = _getter();
            var valueName = Enum.GetName(baseEnum.GetType(), baseEnum);

            foreach (var bindingPair in _stateObjects)
                if (bindingPair.EnumVal == valueName)
                    _rendererWidget.material.SetColor(_propertyName, bindingPair.Color);
        }
    }
}