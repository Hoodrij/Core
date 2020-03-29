using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(bool))] public class GraphicMaterialBooleanBinder : ABinder
    {
        [SerializeField] private Material _false;
        [SerializeField] private Material _true;
        [SerializeField] private Graphic _widget;
        [SerializeField] private Graphic[] _widgets = new Graphic[0];

        private Func<bool> _getter;

        protected override void Bind(bool init)
        {
            var mat = _getter() ? _true : _false;
            SetMaterial(_widget, mat);
            for (var i = 0; i < _widgets.Length; ++i) SetMaterial(_widgets[i], mat);
        }

        private void SetMaterial(Graphic graphic, Material mat)
        {
            if (graphic != null)
            {
                graphic.material = mat;
                graphic.SetAllDirty();
            }
        }

        private void Awake()
        {
            if (_widget == null)
                _widget = GetComponent<Graphic>();
            Init(ref _getter);
        }
    }
}