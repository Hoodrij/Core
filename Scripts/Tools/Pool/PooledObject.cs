using Core.Tools.Observables;
using UnityEngine;

namespace Core.Tools.Pool
{
    public class PooledObject : MonoBehaviour
    {
        public Event<PooledObject> PopEvent { get; } = new Event<PooledObject>();
        public Event<PooledObject> PoolEvent { get; } = new Event<PooledObject>();
        
        internal ObjectPool Pool { get; set; }

        internal void Pop()
        {
            PopEvent.Fire(this);
        }

        public void ReturnToPool()
        {
            PoolEvent.Fire(this);

            if (Pool != null)
                Pool.Push(this);
            else
                Destroy(gameObject);
        }
    }
}