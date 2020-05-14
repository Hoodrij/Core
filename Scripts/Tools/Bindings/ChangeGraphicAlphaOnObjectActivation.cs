using UnityEngine;
using UnityEngine.UI;

namespace Core.Tools.Bindings
{
    public class ChangeGraphicAlphaOnObjectActivation : MonoBehaviour
    {
        [SerializeField] private float _onActive = 1;
        [SerializeField] private float _onInactive;

        [SerializeField] private Graphic _widget;


        private void OnEnable()
        {
            Color color = _widget.color;
            color.a = _onActive;
            _widget.color = color;
        }

        private void OnDisable()
        {
            Color color = _widget.color;
            color.a = _onInactive;
            _widget.color = color;
        }
    }
}