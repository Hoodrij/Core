using System;
using System.Collections;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(bool))] public class AnimatorBinder : ABinder
    {
        [SerializeField] private Animator _animator;
        private Coroutine _coroutine;

        private Func<bool> _getter;
        [SerializeField] private bool _isBoolean;
        [SerializeField] private bool _isInverse;
        [SerializeField] private string _variableName;

        protected override void Bind()
        {
            if (_isBoolean)
                Coroutine(WaitAnimatorInitialization(SetBool));
            else if (IsGetter()) Coroutine(WaitAnimatorInitialization(SetTrigger));
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
            while (!_animator.isInitialized) yield return null;

            action();
            StopCoroutine();
        }

        private bool IsGetter()
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