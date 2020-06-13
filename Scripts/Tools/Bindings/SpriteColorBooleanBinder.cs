using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(bool))] public class SpriteColorBooleanBinder : ABinder
    {
        [SerializeField] private Color _ableColor = Color.gray;

        private Func<bool> _getter;
        [SerializeField] private Color _unableColor = Color.red;

        [SerializeField] private SpriteRenderer _widget;

        protected override void Bind()
        {
            Color color = _getter() ? _ableColor : _unableColor;
            _widget.color = color;
        }

        private void Awake()
        {
            if (_widget == null)
                _widget = GetComponent<SpriteRenderer>();
            Init(ref _getter);
        }
    }
}