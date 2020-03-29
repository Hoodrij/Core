using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(int))] public class Int32BinderRebindBinder : ABinder
    {
        [SerializeField] private ABinder _binder;
        private int _data;


        private Func<int> _getter;

        protected override void Bind(bool init)
        {
            var data = _getter();

            if (data != _data)
            {
                _data = data;
                _binder.Rebind();
            }
        }

        private void Update()
        {
            Bind(false);
        }

        private void Awake()
        {
            Init(ref _getter);
        }
    }
}