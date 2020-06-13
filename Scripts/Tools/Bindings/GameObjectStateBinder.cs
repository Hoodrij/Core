using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(int))] public class GameObjectStateBinder : ABinder
    {
        private Func<int> _getter;

        [SerializeField] private GameObject[] _stateObjects;

        protected override void Bind()
        {
            int state = _getter();

            foreach (GameObject stateObject in _stateObjects)
                if (stateObject != null && stateObject.activeSelf)
                    stateObject.SetActive(false);

            if (_stateObjects.Length >= state && _stateObjects[state] != null && !_stateObjects[state].activeSelf)
                _stateObjects[state].SetActive(true);
        }

        private void Awake()
        {
            Init(ref _getter);
        }
    }
}