using System;
using UnityEngine;

namespace Bindings
{
  [BindTo(typeof(Int32))]
  public class Int32BinderRebindBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private ABinder _binder;
#pragma warning restore 649

    private Func<Int32> _getter;
    private Int32 _data;

    protected override void Bind(Boolean init)
    {
      var data = _getter();

      if (data != _data)
      {
        _data = data;
        _binder.Rebind();
      }
    }

    private void Update()
    {
      Bind(false);
    }

    private void Awake()
    {
      Init(ref _getter);
    }
  }
}