using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(Action))] public class ButtonActionBinder : ABinder
    {
        [SerializeField] private Button _button;


        private Func<Action> _getter;

        protected override void Bind()
        {
            Action action = _getter.Invoke();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => { action.Invoke(); });
        }

        private void Awake()
        {
            if (_button == null)
                _button = GetComponent<Button>();

            Init(ref _getter);
        }

        protected void Reset()
        {
            _button = GetComponent<Button>();
        }
    }
}