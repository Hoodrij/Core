using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(Boolean))]
  public class BoxColliderBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private BoxCollider _collider;
#pragma warning restore 649

    private Func<Boolean> _getter;

    protected override void Bind(Boolean init)
    {
      _collider.enabled = _getter();
    }

    private void Awake()
    {
      Init(ref _getter);
    }
  }
}