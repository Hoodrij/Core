using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(Enum))] public class ShaderColorEnumBinder : GraphicColorEnumBinder
    {
        [SerializeField] private string _propertyName;


        [SerializeField] private Renderer _rendererWidget;


        protected override void Bind()
        {
            Enum baseEnum = _getter();
            string valueName = Enum.GetName(baseEnum.GetType(), baseEnum);

            foreach (EnumBindingPair bindingPair in _stateObjects)
                if (bindingPair.EnumVal == valueName)
                    _rendererWidget.material.SetColor(_propertyName, bindingPair.Color);
        }
    }
}