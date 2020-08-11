using System;
using System.Collections.Generic;

namespace Core.ECS
{
    public partial class World : Unit
    {
        [Inject] Units.Life Life;
        
        public World(IEnumerable<Type> systems)
        {
            RegisterSystems(systems);  
            
            Life.OnUpdate.Listen(Update);
        }
    }
}