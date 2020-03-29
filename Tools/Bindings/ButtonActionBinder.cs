using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(Action))] public class ButtonActionBinder : ABinder
    {
        [SerializeField] private Button _button;


        private Func<Action> _getter;

        protected override void Bind(bool init)
        {
            var action = _getter();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => { action.Invoke(); });
        }

        private void Awake()
        {
            Init(ref _getter);
        }
    }
}