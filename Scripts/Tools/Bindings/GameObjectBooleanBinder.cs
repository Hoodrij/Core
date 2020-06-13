using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(bool))] public class GameObjectBooleanBinder : ABinder
    {
        [SerializeField] private bool _disableObjectsOnAwake;


        [SerializeField] private GameObject _false;


        private Func<bool> _getter;


        [SerializeField] private GameObject _true;


        protected override void Bind()
        {
            bool isTrue = _getter();

            if (_true != null) _true.SetActive(isTrue);
            if (_false != null) _false.SetActive(!isTrue);
        }

        private void Awake()
        {
            if (_disableObjectsOnAwake)
            {
                if (_true != null) _true.SetActive(false);
                if (_false != null) _false.SetActive(false);
            }

            Init(ref _getter);
        }
    }
}