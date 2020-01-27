using System;
using UnityEngine;

namespace Bindings
{
  [BindTo(typeof(Boolean))]
  public class GameObjectEnableBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private GameObject _gameObject;
#pragma warning restore 649

#pragma warning disable 649
    [SerializeField] private Boolean _disableObjectsOnAwake;
#pragma warning restore 649

#pragma warning disable 649
    [SerializeField] private Boolean _revert;
#pragma warning restore 649

    private Func<Boolean> _getter;

    protected override void Bind(Boolean init)
    {
      var state = _revert ? !_getter() : _getter();

      if (_gameObject != null) _gameObject.SetActive(state);
    }

    private void Awake()
    {
      if (_disableObjectsOnAwake)
      {
        if (_gameObject != null) _gameObject.SetActive(false);
      }


      Init(ref _getter);
    }
  }
}