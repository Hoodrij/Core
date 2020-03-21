using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(Single))]
  public class ImageFillAmountBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private Image _sprite;
#pragma warning restore 649

    private Func<Single> _getter;

    protected override void Bind(Boolean init)
    {
      _sprite.fillAmount = _getter();
    }

    private void Awake()
    {
      Init(ref _getter);
    }
  }
}