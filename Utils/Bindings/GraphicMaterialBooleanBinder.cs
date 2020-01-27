using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bindings
{
  [BindTo(typeof(Boolean))]
  public class GraphicMaterialBooleanBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private Material _true;
#pragma warning restore 649
#pragma warning disable 649
    [SerializeField] private Material _false;
#pragma warning restore 649

    [SerializeField] private Graphic[] _widgets = new Graphic[0];

    [SerializeField] private Graphic _widget;

    private Func<Boolean> _getter;

    protected override void Bind(Boolean init)
    {
      Material mat = _getter() ? _true : _false;
      SetMaterial(_widget, mat);
      for (int i = 0; i < _widgets.Length; ++i)
      {
        SetMaterial(_widgets[i], mat);
      }
    }

    private void SetMaterial(Graphic graphic, Material mat)
    {
      if (graphic != null)
      {
        graphic.material = mat;
        graphic.SetAllDirty();
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