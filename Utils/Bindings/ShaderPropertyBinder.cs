using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bindings
{
  [BindTo(typeof(float))]
  public class ShaderPropertyBinder : ABinder
  {
#pragma warning disable 649

    [Serializable]
    private struct EnumBindingPair
    {
      public String EnumVal;
      public Color Color;
    }

    [Header("Use this")] [SerializeField] private Renderer _widget;

    [Header("Or this")] [SerializeField] private Image _uiImage;
    [SerializeField] private string _propertyName;

#pragma warning restore 649

    private Func<float> _getter;

    protected override void Bind(Boolean init)
    {
      var baseFloat = _getter();

      if (_widget != null)
        _widget.material.SetFloat(_propertyName, baseFloat);
      if (_uiImage != null)
        _uiImage.material.SetFloat(_propertyName, baseFloat);
    }

    private void Awake()
    {
      Init(ref _getter);
    }
  }
}