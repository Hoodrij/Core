using UnityEngine;

namespace Core.Tools.Bindings
{
    [RequireComponent(typeof(ABinder))] public class PeriodicRebinder : MonoBehaviour
    {
        private ABinder[] _binders;

        [SerializeField] private float _periodSeconds;


        private float _timer;

        private void Awake()
        {
            _binders = GetComponents<ABinder>();
        }

        private void OnEnable()
        {
            _timer = Time.time;
        }

        private void Update()
        {
            if (Time.time - _timer < _periodSeconds)
                return;

            _timer += _periodSeconds;
            foreach (var aBinder in _binders)
                aBinder.Rebind();
        }
    }
}