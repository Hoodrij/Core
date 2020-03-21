using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(Single))]
  public class SliderMaxValueBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private Slider _slider;
#pragma warning restore 649

    private Func<Single> _getter;
    private Action<Single> _setter;

    private Boolean _inset;

    protected override void Bind(Boolean init)
    {
      if (_inset)
        return;

      //if( init )
      //	_slider.Set( _getter( ), false );

      else
        _slider.maxValue = _getter();
    }

    private void Awake()
    {
      Init(ref _getter);
      Init(ref _setter, false);

      _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(Single value)
    {
      if (_setter == null)
        return;

      _inset = true;
      _setter(value);
      _inset = false;
    }
  }
}