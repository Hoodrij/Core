using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(Enum))] public class ImageEnumBinder : ABinder
    {
        private Func<Enum> _getter;

        [SerializeField] private Image _image;
        private Color _initialColor;
        [SerializeField] private bool _makeTransparentOnNullSprite;
        [SerializeField] private EnumBindingPair[] _stateObjects;

        protected override void Bind(bool init)
        {
            var baseEnum = _getter();
            var valueName = Enum.GetName(baseEnum.GetType(), baseEnum);

            _image.sprite = null;

            foreach (var bindingPair in _stateObjects)
                if (bindingPair.EnumVal == valueName)
                    _image.sprite = bindingPair.Sprite;

            if (_makeTransparentOnNullSprite)
                _image.color = _image.sprite == null ? new Color(0, 0, 0, 0) : _initialColor;
        }

        private void Awake()
        {
            if (_image == null)
                _image = GetComponent<Image>();

            _initialColor = _image.color;

            Init(ref _getter);
        }


        [Serializable] private struct EnumBindingPair
        {
            public string EnumVal;
            public Sprite Sprite;
        }
    }
}