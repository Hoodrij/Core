using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(bool))] public class BoxColliderBinder : ABinder
    {
        [SerializeField] private BoxCollider _collider;


        private Func<bool> _getter;

        protected override void Bind(bool init)
        {
            _collider.enabled = _getter();
        }

        private void Awake()
        {
            Init(ref _getter);
        }
    }
}