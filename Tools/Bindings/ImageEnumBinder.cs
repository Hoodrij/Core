using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(Enum))]
  public class ImageEnumBinder : ABinder
  {
#pragma warning disable 649

    [Serializable]
    private struct EnumBindingPair
    {
      public String EnumVal;
      public Sprite Sprite;
    }

    [SerializeField] private Image _image;
    [SerializeField] private Boolean _makeTransparentOnNullSprite;
    [SerializeField] private EnumBindingPair[] _stateObjects;
#pragma warning restore 649

    private Func<Enum> _getter;
    private Color _initialColor;

    protected override void Bind(Boolean init)
    {
      var baseEnum = _getter();
      var valueName = Enum.GetName(baseEnum.GetType(), baseEnum);

      _image.sprite = null;

      foreach (var bindingPair in _stateObjects)
        if (bindingPair.EnumVal == valueName)
          _image.sprite = bindingPair.Sprite;

      if (_makeTransparentOnNullSprite)
        _image.color = _image.sprite == null ? new Color(0, 0, 0, 0) : _initialColor;
    }

    private void Awake()
    {
      if (_image == null)
        _image = GetComponent<Image>();

      _initialColor = _image.color;

      Init(ref _getter);
    }
  }
}