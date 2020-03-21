using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(Boolean))]
  public class SliderBooleanBinder : ABinder
  {
    [SerializeField] private Slider _slider;

    private Func<Boolean> _getter;
    private Action<Boolean> _setter;

    protected override void Bind(Boolean init)
    {
      //if( init )
      //	_slider.Set( _getter( ) ? 1 : 0, false );

      //else
      _slider.value = _getter() ? 1 : 0;
    }

    private void Awake()
    {
      if (_slider == null)
        _slider = GetComponent<Slider>();

      Init(ref _getter);
      Init(ref _setter);

      _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(Single value)
    {
      _setter(value > 0.5f);
    }
  }
}