using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(Boolean))]
  public class ButtonEnabledBinder : ABinder
  {
    [SerializeField] private Button _button;
#pragma warning disable 649
    [SerializeField] private Boolean _revert;
#pragma warning restore 649

    private Func<Boolean> _getter;

    protected override void Bind(Boolean init)
    {
      _button.interactable = _revert ? !_getter() : _getter();
    }

    private void Awake()
    {
      if (_button == null)
        _button = GetComponent<Button>();

      Init(ref _getter);
    }
  }
}