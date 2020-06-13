using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(bool))] public class GraphicColorBooleanBinder : ABinder
    {
        [SerializeField] private Color _ableColor = Color.gray;

        private Func<bool> _getter;
        [SerializeField] private Color _unableColor = Color.red;

        [SerializeField] private Graphic _widget;

        protected override void Bind()
        {
            Color color = _getter() ? _ableColor : _unableColor;
            _widget.color = color;
        }

        private void Awake()
        {
            if (_widget == null)
                _widget = GetComponent<Graphic>();
            Init(ref _getter);
        }
    }
}