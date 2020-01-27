using System;
using UnityEngine;

namespace Bindings
{
  [BindTo(typeof(Enum))]
  public class AnimationEnumBinder : ABinder
  {
    private Func<Enum> _getter;

#pragma warning disable 649

    [Serializable]
    private struct EnumBindingPair
    {
      public string EnumVal;
      public string TriggerName;
    }

    [SerializeField] private Animator _animator;
    [SerializeField] private EnumBindingPair[] _stateObjects;
#pragma warning restore 649

    protected override void Bind(bool init)
    {
      var baseEnum = _getter();
      var valueName = Enum.GetName(baseEnum.GetType(), baseEnum);

      foreach (var bindingPair in _stateObjects)
        if (bindingPair.EnumVal == valueName)
        {
          _animator.SetTrigger(bindingPair.TriggerName);
        }
    }

    private void Awake()
    {
      if (_animator == null)
        _animator = GetComponent<Animator>();

      Init(ref _getter);
    }
  }
}