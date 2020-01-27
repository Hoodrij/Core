using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bindings
{
  [BindTo(typeof(Boolean))]
  public class ToggleBooleanBinder : ABinder
  {
    [SerializeField] Toggle _toggle;
#pragma warning disable 649
    [SerializeField] Boolean _revert;
#pragma warning restore 649
    private Func<Boolean> _getter;
    private Action<Boolean> _setter;

    protected override void Bind(Boolean init)
    {
      _toggle.isOn = _revert ? !_getter() : _getter();
    }

    private void Awake()
    {
      if (_toggle == null)
        _toggle = GetComponent<Toggle>();

      Init(ref _getter);
      Init(ref _setter);
      if (_setter != null && _toggle != null)
        _toggle.onValueChanged.AddListener(Setter);
    }

    private void Setter(Boolean isOn)
    {
      _setter(isOn);
    }
  }
}