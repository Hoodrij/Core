using System;
using System.Collections;
using UnityEngine;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(Boolean))]
  public class AnimatorBinder : ABinder
  {
    [SerializeField] private Animator _animator;
#pragma warning disable 649
    [SerializeField] private String _variableName;
#pragma warning restore 649
#pragma warning disable 649
    [SerializeField] private Boolean _isBoolean;
#pragma warning restore 649
#pragma warning disable 649
    [SerializeField] private Boolean _isInverse;
#pragma warning restore 649

    private Func<Boolean> _getter;
    private Coroutine _coroutine;

    protected override void Bind(bool init)
    {
      if (_isBoolean)
      {
        Coroutine(WaitAnimatorInitialization(SetBool));
      }
      else if (IsGetter())
      {
        Coroutine(WaitAnimatorInitialization(SetTrigger));
      }
    }

    private void SetBool()
    {
      _animator.SetBool(_variableName, _getter());
    }

    private void SetTrigger()
    {
      _animator.SetTrigger(_variableName);
    }

    private void StopCoroutine()
    {
      if (_coroutine != null)
      {
        StopCoroutine(_coroutine);
        _coroutine = null;
      }
    }

    private void Coroutine(IEnumerator enumerator)
    {
      StopCoroutine();
      _coroutine = StartCoroutine(enumerator);
    }

    private IEnumerator WaitAnimatorInitialization(Action action)
    {
      while (!_animator.isInitialized)
      {
        yield return null;
      }

      action();
      StopCoroutine();
    }

    private Boolean IsGetter()
    {
      return _isInverse ? !_getter() : _getter();
    }

    protected override void OnDisable()
    {
      StopCoroutine();
      base.OnDisable();
    }

    private void Awake()
    {
      if (_animator == null)
        _animator = GetComponent<Animator>();
      Init(ref _getter);
    }
  }
}