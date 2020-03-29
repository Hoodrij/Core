using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(Enum))] public class GraphicColorEnumBinder : ABinder
    {
        protected Func<Enum> _getter;


        [SerializeField] protected EnumBindingPair[] _stateObjects;

        [SerializeField] private Graphic _widget;

        protected override void Bind(bool init)
        {
            var baseEnum = _getter();
            var valueName = Enum.GetName(baseEnum.GetType(), baseEnum);

            foreach (var bindingPair in _stateObjects)
                if (bindingPair.EnumVal == valueName)
                    _widget.color = bindingPair.Color;
        }

        protected void Awake()
        {
            Init(ref _getter);
        }

        [Serializable] protected struct EnumBindingPair
        {
            public string EnumVal;
            public Color Color;
        }
    }
}