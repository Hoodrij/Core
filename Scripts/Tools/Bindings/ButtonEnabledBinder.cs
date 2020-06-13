using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(bool))] public class ButtonEnabledBinder : ABinder
    {
        [SerializeField] private Button _button;


        private Func<bool> _getter;

        [SerializeField] private bool _revert;

        protected override void Bind()
        {
            _button.interactable = _revert ? !_getter() : _getter();
        }

        private void Awake()
        {
            if (_button == null)
                _button = GetComponent<Button>();

            Init(ref _getter);
        }
    }
}