using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.ECS
{
    public partial class World
    {
        private readonly List<System> systems = new List<System>();

        private void RegisterSystems(IEnumerable<Type> types)
        {
            systems.AddRange(types
                .Select(Activator.CreateInstance)
                .Cast<System>()
            );
        }
        
        private void Update()
        {
            systems.ForEach(s => s.Update());
        }
    }
}