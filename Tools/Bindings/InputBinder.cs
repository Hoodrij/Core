using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(string))]
  public class InputBinder : ABinder
  {
#pragma warning disable 649
    [SerializeField] private InputField _input;
#pragma warning restore 649
    [SerializeField] private bool _onEndEdit = false;

    private Func<string> _getter;
    private Action<string> _setter;
    private Boolean _inget;

    private void Awake()
    {
      if (_input == null)
        _input.GetComponent<InputField>();
      Init(ref _getter);
      Init(ref _setter);
      if (_onEndEdit)
        _input.onEndEdit.AddListener(OnValueChanged);
      else
        _input.onValueChanged.AddListener(OnValueChanged);
    }

    protected override void Bind(bool init)
    {
      _inget = true;
      _input.text = _getter();
      _inget = false;
    }

    private void OnValueChanged(string value)
    {
      if (_inget)
        return;

      if (_setter == null)
        return;

      _setter(value);
    }
  }
}