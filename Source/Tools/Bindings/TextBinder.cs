using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(string))] public class TextBinder : ABinder
    {
        private Func<string> _getter;
        [SerializeField] private Text _label;

        [SerializeField] private bool _makeUpperCase;

        protected override void Bind(bool init)
        {
            var text = _getter();

            if (_makeUpperCase)
                text = text.ToUpper();

            _label.text = text;
        }

        private void Awake()
        {
            if (_label == null)
                _label = GetComponent<Text>();

            Init(ref _getter);
        }

        protected void Reset()
        {
            _label = GetComponent<Text>();
        }
    }
}