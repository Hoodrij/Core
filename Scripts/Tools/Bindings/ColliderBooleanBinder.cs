using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(bool))] public class ColliderBooleanBinder : ABinder
    {
        [SerializeField] private Collider _false;


        private Func<bool> _getter;

        [SerializeField] private Collider _true;

        protected override void Bind()
        {
            bool isTrue = _getter();

            if (_true != null) _true.enabled = isTrue;
            if (_false != null) _false.enabled = !isTrue;
        }

        private void Awake()
        {
            Init(ref _getter);
        }
    }
}