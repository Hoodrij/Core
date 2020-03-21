using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(Color))]
  public class GraphicColorBinder : ABinder
  {
    [SerializeField] private Graphic _widget;

    private Func<Color> _getter;

    protected override void Bind(Boolean init)
    {
      _widget.color = _getter();
    }

    private void Awake()
    {
      if (_widget == null)
        _widget = GetComponent<Graphic>();

      Init(ref _getter);
    }
  }
}