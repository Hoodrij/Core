using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bindings
{
  [BindTo(typeof(Action))]
  public class ButtonActionBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private Button _button;
#pragma warning restore 649

    private Func<Action> _getter;

    protected override void Bind(bool init)
    {
      var action = _getter();
      _button.onClick.RemoveAllListeners();
      _button.onClick.AddListener(() => { action.Invoke(); });
    }

    private void Awake()
    {
      Init(ref _getter);
    }
  }
}