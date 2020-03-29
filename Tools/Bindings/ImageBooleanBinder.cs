using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(bool))] public class ImageBooleanBinder : ABinder
    {
        [Space(5)] [SerializeField] private Sprite _ableSprite;


        private Func<bool> _getter;


        [SerializeField] private Sprite _unableSprite;
        [SerializeField] private Image _widget;

        protected override void Bind(bool init)
        {
            _widget.sprite = _getter() ? _ableSprite : _unableSprite;
        }

        private void Awake()
        {
            if (_widget == null)
                _widget = GetComponent<Image>();
            Init(ref _getter);
        }
    }
}