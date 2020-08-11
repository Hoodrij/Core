using System;
using System.Collections.Generic;

namespace Core.ECS
{
    public partial class World
    {
        // object == List<AComponent>
        private readonly Dictionary<Type, object> componentsMap = new Dictionary<Type, object>();
        
        internal void RegisterComponent<T>(T comp) where T : AComponent
        {
            List<T> list = GetComponentsList<T>();
            list.Add(comp);
        }
        
        internal void UnregisterComponent<T>(T comp) where T : AComponent
        {
            List<T> list = GetComponentsList<T>();
            list.Remove(comp);
        }

        internal List<T> GetComponentsList<T>() where T : AComponent
        {
            Type type = typeof(T);
            if (!componentsMap.ContainsKey(type))
            {
                componentsMap.Add(type, new List<T>());
            }
            return componentsMap[type] as List<T>;
        }
    }
}