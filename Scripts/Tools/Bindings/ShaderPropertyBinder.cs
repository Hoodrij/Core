using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(float))] public class ShaderPropertyBinder : ABinder
    {
        private Func<float> _getter;
        [SerializeField] private string _propertyName;

        [Header("Or this")] [SerializeField] private Image _uiImage;

        [Header("Use this")] [SerializeField] private Renderer _widget;

        protected override void Bind()
        {
            float baseFloat = _getter();

            if (_widget != null)
                _widget.material.SetFloat(_propertyName, baseFloat);
            if (_uiImage != null)
                _uiImage.material.SetFloat(_propertyName, baseFloat);
        }

        private void Awake()
        {
            Init(ref _getter);
        }


        [Serializable] private struct EnumBindingPair
        {
            public string EnumVal;
            public Color Color;
        }
    }
}