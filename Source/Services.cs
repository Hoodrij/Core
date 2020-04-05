using System.Collections.Generic;
using Core.Abstract;

namespace Core
{
    public class Services
    {
        private List<Service> services = new List<Service>();

        public void Add(Service service)
        {
            services.Add(service);
        }

        public void Add(IEnumerable<Service> services)
        {
            foreach (var service in services)
            {
                if (service == null) continue;
                Add(service);
            }
        }

        internal void Reset() => services.ForEach(service => service.Reset());
    }
}