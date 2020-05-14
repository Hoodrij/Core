using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(Texture2D))] public class RawImageBinder : ABinder
    {
        private Func<Texture2D> _getter;

        [SerializeField] private RawImage _texture;

        protected override void Bind(bool init)
        {
            _texture.texture = _getter();
        }

        private void Awake()
        {
            Init(ref _getter);
        }
    }
}