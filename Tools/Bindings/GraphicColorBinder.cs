using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(Color))] public class GraphicColorBinder : ABinder
    {
        private Func<Color> _getter;
        [SerializeField] private Graphic _widget;

        protected override void Bind(bool init)
        {
            _widget.color = _getter();
        }

        private void Awake()
        {
            if (_widget == null)
                _widget = GetComponent<Graphic>();

            Init(ref _getter);
        }
    }
}