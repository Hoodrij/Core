using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(Enum))] public class GameObjectEnumBinder : ABinder
    {
        private Func<Enum> _getter;

        [SerializeField] private EnumBindingPair[] _stateObjects;

        protected override void Bind(bool init)
        {
            var baseEnum = _getter();
            var valueName = Enum.GetName(baseEnum.GetType(), baseEnum);

            //DON'T CHANGE! Done on purpose for case when one gameobject set for two enum states
            {
                foreach (var bindingPair in _stateObjects)
                    if (bindingPair.TargetObject != null)
                        bindingPair.TargetObject.SetActive(false);

                foreach (var bindingPair in _stateObjects)
                    if (bindingPair.TargetObject != null && bindingPair.EnumVal == valueName)
                        bindingPair.TargetObject.SetActive(true);
            }
        }

        private void Awake()
        {
            Init(ref _getter);
        }


        [Serializable] private struct EnumBindingPair
        {
            public string EnumVal;
            public GameObject TargetObject;
        }
    }
}