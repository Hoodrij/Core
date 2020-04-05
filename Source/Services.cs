using System.Collections.Generic;
using Core.Abstract;

namespace Core
{
    public class Services
    {
        private List<IService> services = new List<IService>();

        public void Add(IService service)
        {
            services.Add(service);
            
            Game.Models.Populate(service);
            
            if (service is IUpdate iupdate)
            {
                Game.Life.OnUpdate.Listen(iupdate.Update);
            }
        }

        public void Add(IEnumerable<IService> services)
        {
            foreach (var service in services)
            {
                if (service == null) continue;
                Add(service);
            }
        }
    }
}