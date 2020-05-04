using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(Task<string>))] public class TextAsyncBinder : ABinder
    {
        private Func<Task<string>> _getter;
        [SerializeField] private Text _label;

        [SerializeField] private bool _makeUpperCase;

        protected override async void Bind(bool init)
        {
            string text = await _getter();

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