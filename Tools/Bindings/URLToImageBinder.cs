using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(string))] public class URLToImageBinder : ABinder
    {
        private Func<string> _getter;

        [SerializeField] private GameObject _spinner;

        [SerializeField] private Image _sprite;


        [SerializeField] private bool _transparentOnNull;

        protected override void Bind(bool init)
        {
            if (string.IsNullOrEmpty(_getter()))
                return;

            //TODO: show loading
            if (_sprite.sprite == null || _sprite.sprite.texture == null)
            {
                //_sprite.color = Color.gray;
                if (_spinner != null)
                    _spinner.SetActive(true);
                StartCoroutine(FetchTexture(_getter()));
            }
        }

        private void Awake()
        {
            if (_sprite == null)
                _sprite = GetComponent<Image>();
            if (_transparentOnNull && (_sprite.sprite == null || _sprite.sprite.texture == null))
                _sprite.CrossFadeAlpha(0, 0, true);
            Init(ref _getter);
        }

        private IEnumerator FetchTexture(string url)
        {
            var www = new WWW(url);
            yield return www;
            //_sprite.color = Color.white;

            var tex = www.texture;
            _sprite.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);

            if (_transparentOnNull)
                _sprite.CrossFadeAlpha(1, 0.3f, true);

            if (_spinner != null)
                _spinner.SetActive(false);
        }
    }
}