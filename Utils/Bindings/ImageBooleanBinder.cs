using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bindings
{
  [BindTo(typeof(Boolean))]
  public class ImageBooleanBinder : ABinder
  {
    [SerializeField] private Image _widget;

#pragma warning disable 649
    [Space(5)] [SerializeField] private Sprite _ableSprite;
#pragma warning restore 649
#pragma warning disable 649
    [SerializeField] private Sprite _unableSprite;
#pragma warning restore 649


    private Func<Boolean> _getter;

    protected override void Bind(Boolean init)
    {
      _widget.sprite = _getter() ? _ableSprite : _unableSprite;
    }

    private void Awake()
    {
      if (_widget == null)
        _widget = GetComponent<Image>();
      Init(ref _getter);
    }
  }
}