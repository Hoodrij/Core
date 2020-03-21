using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(Boolean))]
  public class ColliderBooleanBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private Collider _true;
    [SerializeField] private Collider _false;
#pragma warning restore 649

    private Func<Boolean> _getter;

    protected override void Bind(Boolean init)
    {
      var isTrue = _getter();

      if (_true != null) _true.enabled = isTrue;
      if (_false != null) _false.enabled = !isTrue;
    }

    private void Awake()
    {
      Init(ref _getter);
    }
  }
}