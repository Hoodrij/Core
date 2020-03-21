using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(String))]
  public class GameObjectStringBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private String _comparison;
#pragma warning restore 649
#pragma warning disable 649
    [SerializeField] private GameObject _equals;
#pragma warning restore 649
#pragma warning disable 649
    [SerializeField] private GameObject _notEquals;
#pragma warning restore 649

    private Func<String> _getter;

    protected override void Bind(Boolean init)
    {
      var equals = _getter() == _comparison;

      if (_equals != null) _equals.SetActive(equals);
      if (_notEquals != null) _notEquals.SetActive(!equals);
    }

    private void Awake()
    {
      Init(ref _getter);
    }
  }
}