using System;
using UnityEngine;

namespace Core.Tools.Bindings
{
  [RequireComponent(typeof(ABinder))]
  public class PeriodicRebinder : MonoBehaviour
  {
#pragma warning disable 649
    [SerializeField] private Single _periodSeconds;
#pragma warning restore 649

    private Single _timer;
    private ABinder[] _binders;

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