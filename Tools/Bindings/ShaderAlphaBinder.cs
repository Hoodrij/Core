using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(float))]
  public class ShaderAlphaBinder : ABinder
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

#pragma warning restore 649

    private Func<float> _getter;

    protected override void Bind(Boolean init)
    {
      var baseFloat = _getter();

      if (_widget != null)
      {
        Color color = _widget.material.GetColor("_Color");
        _widget.material.SetColor("_Color", WithAlpha(color, baseFloat));
      }

      if (_uiImage != null)
      {
        Color color = _uiImage.material.GetColor("_Color");
        _uiImage.material.SetColor("_Color", WithAlpha(color, baseFloat));
      }
    }

    public static Color WithAlpha(Color color, float a)
    {
      return new Color(color.r, color.g, color.b, a);
    }

    private void Awake()
    {
      Init(ref _getter);
    }
  }
}