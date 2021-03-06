using System;
using System.Linq;
using UnityEngine;

namespace Core.Tools.Bindings
{
    [ExecuteInEditMode] [RequireComponent(typeof(Renderer))]
    public class CanvasGroupAlfaToMaterialBinder : MonoBehaviour
    {
        [SerializeField] private string _alphaPropertyName;

        [ConditionalField("SetCanvasGroup")] [SerializeField]
        private CanvasGroup _canvasGroup;

        private Renderer _mesh;
        private float _previousAlpha;


        public bool SetCanvasGroup;

        protected void Update()
        {
            if (_mesh == null || _canvasGroup == null || string.IsNullOrEmpty(_alphaPropertyName))
                return;

            if (Math.Abs(_previousAlpha - _canvasGroup.alpha) < float.Epsilon)
                return;

            Color color = _mesh.sharedMaterial.color;
            _previousAlpha = color.a = _canvasGroup.alpha;

            _mesh.sharedMaterial.SetColor(_alphaPropertyName, color);
        }

        private void Awake()
        {
            if (_canvasGroup == null)
            {
                _mesh = GetComponent<Renderer>();
                _mesh.sharedMaterial = new Material(_mesh.sharedMaterial);
            }

            if (string.IsNullOrEmpty(_alphaPropertyName))
                _previousAlpha = _mesh.sharedMaterial.GetColor(_alphaPropertyName).a;

            if (_canvasGroup == null)
                _canvasGroup = GetComponentsInParent(typeof(CanvasGroup), true).Cast<CanvasGroup>().FirstOrDefault();
        }
    }
}