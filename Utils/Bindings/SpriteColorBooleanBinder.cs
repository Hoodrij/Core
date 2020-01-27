using System;
using UnityEngine;

namespace Bindings
{
  [BindTo(typeof(Boolean))]
  public class SpriteColorBooleanBinder : ABinder
  {
    [SerializeField] private Color _ableColor = Color.gray;
    [SerializeField] private Color _unableColor = Color.red;

    [SerializeField] private SpriteRenderer _widget;

    private Func<Boolean> _getter;

    protected override void Bind(Boolean init)
    {
      Color color = _getter() ? _ableColor : _unableColor;
      _widget.color = color;
    }

    private void Awake()
    {
      if (_widget == null)
        _widget = GetComponent<SpriteRenderer>();
      Init(ref _getter);
    }
  }
}