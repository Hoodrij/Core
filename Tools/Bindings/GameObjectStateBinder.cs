using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(Int32))]
  public class GameObjectStateBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private GameObject[] _stateObjects;
#pragma warning restore 649

    private Func<Int32> _getter;

    protected override void Bind(Boolean init)
    {
      var state = _getter();

      foreach (var stateObject in _stateObjects)
        if (stateObject != null && stateObject.activeSelf)
          stateObject.SetActive(false);

      if (_stateObjects.Length >= state && _stateObjects[state] != null && !_stateObjects[state].activeSelf)
        _stateObjects[state].SetActive(true);
    }

    private void Awake()
    {
      Init(ref _getter);
    }
  }
}