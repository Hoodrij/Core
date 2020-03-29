using UnityEngine;

namespace Core.Tools.Pool
{
    public abstract class APoolable : MonoBehaviour
    {
        protected internal ObjectPool Pool { get; set; }
        protected bool IsPooled { get; set; }

        public virtual void OnPop()
        {
            IsPooled = false;
        }

        protected virtual void OnReturnedToPool()
        {
            IsPooled = true;
        }

        public void ReturnToPool()
        {
            OnReturnedToPool();

            if (Pool != null)
                Pool.Push(this);
            else
                Destroy(gameObject);
        }
    }
}