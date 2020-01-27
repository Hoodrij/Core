using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bindings
{
  [BindTo(typeof(Boolean))]
  public class GraphicColorBooleanBinder : ABinder
  {
    [SerializeField] private Color _ableColor = Color.gray;
    [SerializeField] private Color _unableColor = Color.red;

    [SerializeField] private Graphic _widget;

    private Func<Boolean> _getter;

    protected override void Bind(Boolean init)
    {
      Color color = _getter() ? _ableColor : _unableColor;
      _widget.color = color;
    }

    private void Awake()
    {
      if (_widget == null)
        _widget = GetComponent<Graphic>();
      Init(ref _getter);
    }
  }
}