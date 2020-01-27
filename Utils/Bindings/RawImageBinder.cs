using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bindings
{
  [BindTo(typeof(Texture2D))]
  public class RawImageBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private RawImage _texture;
#pragma warning restore 649

    private Func<Texture2D> _getter;

    protected override void Bind(Boolean init)
    {
      _texture.texture = _getter();
    }

    private void Awake()
    {
      Init(ref _getter);
    }
  }
}