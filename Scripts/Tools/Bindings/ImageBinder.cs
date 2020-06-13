using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(Sprite))] public class ImageBinder : ABinder
    {
        private Func<Sprite> _getter;
        private Color _initialColor;

        [SerializeField] private bool _makeTransparentOnNullSprite;


        [SerializeField] private bool _override;
        [SerializeField] private Image _sprite;

        protected override void Bind()
        {
            if (_override)
                _sprite.overrideSprite = _getter();

            else
                _sprite.sprite = _getter();
            _sprite.SetAllDirty();

            if (_makeTransparentOnNullSprite)
                _sprite.color = _sprite.sprite == null ? new Color(0, 0, 0, 0) : _initialColor;
        }

        private void Awake()
        {
            if (_sprite == null)
                _sprite = GetComponent<Image>();

            _initialColor = _sprite.color;

            Init(ref _getter);
        }
    }
}