using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(string))] public class StringEqualBinder : ABinder
    {
        [SerializeField] private StringBindingPair[] _binds;


        private Func<string> _getter;

        protected override void Bind()
        {
            string key = _getter();

            GameObject activeItem = null;
            foreach (StringBindingPair i in _binds)
            {
                if (i.Str == key)
                    activeItem = i.Obj;
                else
                    i.Obj.SetActive(false);

                if (activeItem != null) activeItem.SetActive(true);
            }
        }

        private void Awake()
        {
            Init(ref _getter);
        }

        [Serializable] private struct StringBindingPair
        {
            public string Str;
            public GameObject Obj;
        }
    }
}