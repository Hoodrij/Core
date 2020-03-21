using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(UnityEngine.Texture))]
  public class TextureBinder : ABinder
  {
    [SerializeField] private RawImage _texture;
#pragma warning disable 649
    [SerializeField] private Boolean _makeTransparentOnNullSprite;
#pragma warning restore 649

    private Func<UnityEngine.Texture> _getter;
    private Color _initialColor;

    protected override void Bind(Boolean init)
    {
      _texture.texture = _getter();

      if (_makeTransparentOnNullSprite)
        _texture.color = _texture.texture == null ? new Color(0, 0, 0, 0) : _initialColor;
    }

    private void Awake()
    {
      if (_texture == null)
        _texture = GetComponent<RawImage>();

      _initialColor = _texture.color;

      Init(ref _getter);
    }
  }
}