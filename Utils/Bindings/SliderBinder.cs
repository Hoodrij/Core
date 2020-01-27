using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bindings
{
  [BindTo(typeof(float))]
  public class SliderBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private Slider _slider;
#pragma warning restore 649


    private Func<float> _getter;
    private Action<float> _setter;

    private bool _inset;

    protected override void Bind(bool init)
    {
      if (_inset)
        return;

      //if( init )
      //	_slider.Set( _getter( ), false );

      else
        _slider.value = _getter();
    }

    private void Awake()
    {
      Init(ref _getter);
      Init(ref _setter, false);

      _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
      if (_setter == null)
        return;

      _inset = true;
      _setter(value);
      _inset = false;
    }
  }
}