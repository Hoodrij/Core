using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bindings
{
  [BindTo(typeof(Boolean))]
  public class UiSelectableBinder : ABinder
  {
    [SerializeField] private Selectable _selectable;
#pragma warning disable 649
    [SerializeField] private Boolean _revert;
#pragma warning restore 649

    private Func<Boolean> _getter;

    protected override void Bind(Boolean init)
    {
      _selectable.interactable = _revert ? !_getter() : _getter();
    }

    private void Awake()
    {
      if (_selectable == null)
        _selectable = GetComponent<Selectable>();

      Init(ref _getter);
    }
  }
}