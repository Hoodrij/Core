using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(bool))] public class GraphicBooleanAlphaBinder : ABinder
    {
        [SerializeField] private bool _applyToChilds;

        [SerializeField] [Range(0.0F, 1.0F)] private float _falseVal;

        private Func<bool> _getter;


        [SerializeField] [Range(0.0F, 1.0F)] private float _trueVal;

        [SerializeField] private Graphic _widget;

        protected override void Bind()
        {
            if (_widget != null)
            {
                Color color = _widget.color;
                color.a = _getter() ? _trueVal : _falseVal;
                _widget.color = color;
            }

            if (_applyToChilds)
                foreach (Graphic item in GetComponentsInChildren<Graphic>())
                {
                    Color col = item.color;
                    col.a = _getter() ? _trueVal : _falseVal;
                    item.color = col;
                }
        }

        private void Awake()
        {
            if (_widget == null)
                _widget = GetComponent<Graphic>();

            Init(ref _getter);
        }
    }
}