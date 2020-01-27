using System;
using UnityEngine;

namespace Bindings
{
  [BindTo(typeof(bool))]
  public class GameObjectBooleanBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private bool _disableObjectsOnAwake;
#pragma warning restore 649

#pragma warning disable 649
    [SerializeField] private GameObject _false;
#pragma warning restore 649

    private Func<bool> _getter;

#pragma warning disable 649
    [SerializeField] private GameObject _true;
#pragma warning restore 649

    protected override void Bind(bool init)
    {
      var isTrue = _getter();

      if (_true != null) _true.SetActive(isTrue);
      if (_false != null) _false.SetActive(!isTrue);
    }

    private void Awake()
    {
      if (_disableObjectsOnAwake)
      {
        if (_true != null) _true.SetActive(false);
        if (_false != null) _false.SetActive(false);
      }

      Init(ref _getter);
    }
  }
}