using System.Collections.Generic;
using Core.Abstract;

namespace Core
{
    public class Services
    {
        private readonly List<Service> services = new List<Service>();

        internal Services(IEnumerable<Service> setup)
        {
            Add(setup);
        }

        private void Add(IEnumerable<Service> setup)
        {
            foreach (var service in setup)
            {
                if (service == null) continue;
                services.Add(service);
            }
        }

        internal void Reset() => services.ForEach(service => service.Reset());
    }
}