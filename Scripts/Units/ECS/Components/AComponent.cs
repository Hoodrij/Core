using UnityEngine;

namespace Core.ECS
{
    [RequireComponent(typeof(Entity))]
    public abstract class AComponent : MonoBehaviour
    {
        internal Entity Entity { get; set; }
        
        private void OnEnable()
        {
            if (Entity == null)
            {
                Entity = GetComponent<Entity>();
                Entity.Register(this);
            }
        }

        private void OnDisable()
        {
            Entity.Remove(this);
        }
    }
}