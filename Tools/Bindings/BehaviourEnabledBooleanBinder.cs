using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(Boolean))]
  public class BehaviourEnabledBooleanBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private Behaviour[] _true;
#pragma warning restore 649
#pragma warning disable 649
    [SerializeField] private Behaviour[] _false;
#pragma warning restore 649

    private Func<Boolean> _getter;

    protected override void Bind(Boolean init)
    {
      var isTrue = _getter();

      if (_true != null)
      {
        foreach (var behaviour in _true)
        {
          behaviour.enabled = isTrue;
        }
      }

      if (_false != null)
      {
        foreach (var behaviour in _false)
        {
          behaviour.enabled = !isTrue;
        }
      }
    }

    private void Awake()
    {
      Init(ref _getter);
    }
  }
}