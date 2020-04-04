using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(string))] public class GameObjectStringBinder : ABinder
    {
        [SerializeField] private string _comparison;


        [SerializeField] private GameObject _equals;


        private Func<string> _getter;


        [SerializeField] private GameObject _notEquals;

        protected override void Bind(bool init)
        {
            var equals = _getter() == _comparison;

            if (_equals != null) _equals.SetActive(equals);
            if (_notEquals != null) _notEquals.SetActive(!equals);
        }

        private void Awake()
        {
            Init(ref _getter);
        }
    }
}