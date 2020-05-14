using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(Enum))] public class AnimationEnumBinder : ABinder
    {
        [SerializeField] private Animator _animator;
        private Func<Enum> _getter;
        [SerializeField] private EnumBindingPair[] _stateObjects;

        protected override void Bind(bool init)
        {
            Enum baseEnum = _getter();
            string valueName = Enum.GetName(baseEnum.GetType(), baseEnum);

            foreach (EnumBindingPair bindingPair in _stateObjects)
                if (bindingPair.EnumVal == valueName)
                    _animator.SetTrigger(bindingPair.TriggerName);
        }

        private void Awake()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();

            Init(ref _getter);
        }

        [Serializable] private struct EnumBindingPair
        {
            public string EnumVal;
            public string TriggerName;
        }
    }
}