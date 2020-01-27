using System;
using UnityEngine;

namespace Bindings
{
  [BindTo(typeof(Boolean))]
  public class GameObjectsBooleanBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private GameObject[] _true;
#pragma warning restore 649
#pragma warning disable 649
    [SerializeField] private GameObject[] _false;
#pragma warning restore 649

#pragma warning disable 649
    [SerializeField] private Boolean _disableObjectsOnAwake;
#pragma warning restore 649

    private Func<Boolean> _getter;

    protected override void Bind(Boolean init)
    {
      var isTrue = _getter();

      foreach (var go in _true)
      {
        if (go != null) go.SetActive(isTrue);
      }

      foreach (var go in _false)
      {
        if (go != null) go.SetActive(!isTrue);
      }
    }

    private void Awake()
    {
      if (_disableObjectsOnAwake)
      {
        foreach (var go in _true)
        {
          if (go != null) go.SetActive(false);
        }

        foreach (var go in _false)
        {
          if (go != null) go.SetActive(false);
        }
      }

      Init(ref _getter);
    }
  }
}