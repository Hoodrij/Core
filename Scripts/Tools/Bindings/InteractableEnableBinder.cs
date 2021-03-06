using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(bool))] public class InteractableEnableBinder : ABinder
    {
        private Func<bool> _getter;

        [SerializeField] private bool _revert;
        [SerializeField] private Selectable _selectable;

        protected override void Bind()
        {
            _selectable.interactable = _revert ? !_getter() : _getter();
        }

        private void Awake()
        {
            if (_selectable == null)
                _selectable = GetComponent<Selectable>();

            Init(ref _getter);
        }
    }
}