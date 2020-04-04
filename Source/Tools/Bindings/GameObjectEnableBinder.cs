using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(bool))] public class GameObjectEnableBinder : ABinder
    {
        [SerializeField] private bool _disableObjectsOnAwake;

        [SerializeField] private GameObject _gameObject;


        private Func<bool> _getter;


        [SerializeField] private bool _revert;

        protected override void Bind(bool init)
        {
            var state = _revert ? !_getter() : _getter();

            if (_gameObject != null) _gameObject.SetActive(state);
        }

        private void Awake()
        {
            if (_disableObjectsOnAwake)
                if (_gameObject != null)
                    _gameObject.SetActive(false);


            Init(ref _getter);
        }
    }
}