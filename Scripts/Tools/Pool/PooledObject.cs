using Core.Tools.Observables;
using UnityEngine;

namespace Core.Tools.Pool
{
    public sealed class PooledObject : MonoBehaviour
    {
        public Signal<PooledObject> OnPoped = new Signal<PooledObject>();
        public Signal<PooledObject> OnPooled = new Signal<PooledObject>();
        
        internal ObjectPool Pool { get; set; }

        internal void Pop()
        {
            OnPoped.Fire(this);
        }

        public void ReturnToPool()
        {
            OnPooled.Fire(this);

            if (Pool != null)
                Pool.Push(this);
            else
                Destroy(gameObject);
        }
    }
}