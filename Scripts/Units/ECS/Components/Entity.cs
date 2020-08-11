using System;
using System.Collections.Generic;
using Core.Tools;
using UnityEngine;

namespace Core.ECS
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-1)]
    public class Entity : MonoBehaviour
    {
        private readonly Dictionary<Type, Component> map = new Dictionary<Type, Component>();
        private World World => Injector.Instance.Get<World>();

        private void Awake()
        {
            if (World == null) return;
            
            foreach(Component component in GetComponents<Component>())
            {
                Register(component);
            }
        }
        
        internal void Register(Component component)
        {
            map.Add(component.GetType(), component);
            if (component is AComponent aComponent)
            {
                aComponent.Entity = this;
                World?.RegisterComponent((dynamic) aComponent);
            }
        }
        
        internal void Remove(Component component)
        {
            map.Remove(component.GetType());
            if (component is AComponent aComponent)
            {
                World?.UnregisterComponent(aComponent);
            }
        }

        public T Get<T>() where T : Component
        {
            Component component = Get(typeof(T));
            return component == null
                ? null
                : component as T;
        }
        
        public Component Get(Type type)
        {
            return map.TryGetValue(type, out Component result) 
                ? result
                : null;
        }
    }
}