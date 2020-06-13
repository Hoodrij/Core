using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(Quaternion))] public class RotationBinder : ABinder
    {
        private Func<Quaternion> _getter;
        [SerializeField] private RectTransform _targetRect;

        protected override void Bind()
        {
            _targetRect.rotation = _getter();
        }

        private void Awake()
        {
            if (_targetRect == null)
                _targetRect = GetComponent<RectTransform>();
            Init(ref _getter);
        }
    }
}