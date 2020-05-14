using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(float))] public class ImageFillAmountBinder : ABinder
    {
        private Func<float> _getter;

        [SerializeField] private Image _sprite;

        protected override void Bind(bool init)
        {
            _sprite.fillAmount = _getter();
        }

        private void Awake()
        {
            Init(ref _getter);
        }
    }
}