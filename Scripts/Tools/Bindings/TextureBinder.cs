using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(Texture))] public class TextureBinder : ABinder
    {
        private Func<Texture> _getter;
        private Color _initialColor;

        [SerializeField] private bool _makeTransparentOnNullSprite;
        [SerializeField] private RawImage _texture;

        protected override void Bind()
        {
            _texture.texture = _getter();

            if (_makeTransparentOnNullSprite)
                _texture.color = _texture.texture == null ? new Color(0, 0, 0, 0) : _initialColor;
        }

        private void Awake()
        {
            if (_texture == null)
                _texture = GetComponent<RawImage>();

            _initialColor = _texture.color;

            Init(ref _getter);
        }
    }
}