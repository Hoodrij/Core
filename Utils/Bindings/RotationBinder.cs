using System;
using UnityEngine;

namespace Bindings
{
  [BindTo(typeof(Quaternion))]
  public class RotationBinder : ABinder
  {
    [SerializeField] RectTransform _targetRect;
    private Func<Quaternion> _getter;

    protected override void Bind(bool init)
    {
      _targetRect.rotation = _getter();
    }

    private void Awake()
    {
      if (_targetRect == null)
        _targetRect = GetComponent<RectTransform>();
      Init(ref _getter);
    }
  }
}