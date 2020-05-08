using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(Task<Sprite>))] public class ImageAsyncBinder : ABinder
    {
        private Func<Task<Sprite>> _getter;
        private Color _initialColor;

        [SerializeField] private bool _makeTransparentOnNullSprite;

        [SerializeField] private bool _override;
        [SerializeField] private Image _sprite;

        protected override async void Bind(bool init)
        {
            if (_override)
                _sprite.overrideSprite = await _getter();

            else
                _sprite.sprite = await _getter();
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