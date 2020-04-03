using System.Collections.Generic;
using Core.Abstract;

namespace Core
{
    public class Services
    {
        private List<Service> services = new List<Service>();
        private List<RuntimeService> runtimeServices = new List<RuntimeService>();

        public Services()
        {
            Game.Life.OnUpdate.Listen(Update);
        }

        private void Update()
        {
            runtimeServices.ForEach(service => service.Update());
        }

        private void Add(Service service)
        {
            services.Add(service);

            if (service is RuntimeService runtimeService) runtimeServices.Add(runtimeService);
        }

        public void Add(IEnumerable<Service> services)
        {
            foreach (var service in services) Add(service);
        }
    }
}