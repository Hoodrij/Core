using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(Boolean))]
  public class GraphicBooleanAlphaBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] [Range(0.0F, 1.0F)] private float _falseVal;
#pragma warning restore 649
#pragma warning disable 649
    [SerializeField] [Range(0.0F, 1.0F)] private float _trueVal;
#pragma warning restore 649

    [SerializeField] private bool _applyToChilds = false;

    [SerializeField] private Graphic _widget;

    private Func<Boolean> _getter;

    protected override void Bind(bool init)
    {
      if (_widget != null)
      {
        var color = _widget.color;
        color.a = _getter() ? _trueVal : _falseVal;
        _widget.color = color;
      }

      if (_applyToChilds)
      {
        foreach (Graphic item in GetComponentsInChildren<Graphic>())
        {
          var col = item.color;
          col.a = _getter() ? _trueVal : _falseVal;
          item.color = col;
        }
      }
    }

    private void Awake()
    {
      if (_widget == null)
        _widget = GetComponent<Graphic>();

      Init(ref _getter);
    }
  }
}