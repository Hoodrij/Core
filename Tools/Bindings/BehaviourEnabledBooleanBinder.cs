using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(bool))] public class BehaviourEnabledBooleanBinder : ABinder
    {
        [SerializeField] private Behaviour[] _false;


        private Func<bool> _getter;

        [SerializeField] private Behaviour[] _true;

        protected override void Bind(bool init)
        {
            var isTrue = _getter();

            if (_true != null)
                foreach (var behaviour in _true)
                    behaviour.enabled = isTrue;

            if (_false != null)
                foreach (var behaviour in _false)
                    behaviour.enabled = !isTrue;
        }

        private void Awake()
        {
            Init(ref _getter);
        }
    }
}