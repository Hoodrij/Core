using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(bool))] public class GameObjectsBooleanBinder : ABinder
    {
        [SerializeField] private bool _disableObjectsOnAwake;


        [SerializeField] private GameObject[] _false;


        private Func<bool> _getter;

        [SerializeField] private GameObject[] _true;

        protected override void Bind()
        {
            bool isTrue = _getter();

            foreach (GameObject go in _true)
                if (go != null)
                    go.SetActive(isTrue);

            foreach (GameObject go in _false)
                if (go != null)
                    go.SetActive(!isTrue);
        }

        private void Awake()
        {
            if (_disableObjectsOnAwake)
            {
                foreach (GameObject go in _true)
                    if (go != null)
                        go.SetActive(false);

                foreach (GameObject go in _false)
                    if (go != null)
                        go.SetActive(false);
            }

            Init(ref _getter);
        }
    }
}