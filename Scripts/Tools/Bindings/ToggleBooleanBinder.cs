using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(bool))] public class ToggleBooleanBinder : ABinder
    {
        private Func<bool> _getter;

        [SerializeField] private bool _revert;
        private Action<bool> _setter;
        [SerializeField] private Toggle _toggle;

        protected override void Bind()
        {
            _toggle.isOn = _revert ? !_getter() : _getter();
        }

        private void Awake()
        {
            if (_toggle == null)
                _toggle = GetComponent<Toggle>();

            Init(ref _getter);
            Init(ref _setter);
            if (_setter != null && _toggle != null)
                _toggle.onValueChanged.AddListener(Setter);
        }

        private void Setter(bool isOn)
        {
            _setter(isOn);
        }
    }
}