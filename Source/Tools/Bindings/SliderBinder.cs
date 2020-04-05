using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(float))] public class SliderBinder : ABinder
    {
        private Func<float> _getter;

        private bool _inset;
        private Action<float> _setter;

        [SerializeField] private Slider _slider;

        protected override void Bind(bool init)
        {
            if (_inset)
                return;

            //if( init )
            //	_slider.Set( _getter( ), false );

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