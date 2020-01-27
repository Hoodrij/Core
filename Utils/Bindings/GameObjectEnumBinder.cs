using System;
using UnityEngine;

namespace Bindings
{
  [BindTo(typeof(Enum))]
  public class GameObjectEnumBinder : ABinder
  {
#pragma warning disable 649

    [Serializable]
    private struct EnumBindingPair
    {
      public String EnumVal;
      public GameObject TargetObject;
    }

    [SerializeField] private EnumBindingPair[] _stateObjects;
#pragma warning restore 649

    private Func<Enum> _getter;

    protected override void Bind(Boolean init)
    {
      var baseEnum = _getter();
      var valueName = Enum.GetName(baseEnum.GetType(), baseEnum);

      //DON'T CHANGE! Done on purpose for case when one gameobject set for two enum states
      {
        foreach (var bindingPair in _stateObjects)
          if (bindingPair.TargetObject != null)
            bindingPair.TargetObject.SetActive(false);

        foreach (var bindingPair in _stateObjects)
          if (bindingPair.TargetObject != null && bindingPair.EnumVal == valueName)
            bindingPair.TargetObject.SetActive(true);
      }
    }

    private void Awake()
    {
      Init(ref _getter);
    }
  }
}