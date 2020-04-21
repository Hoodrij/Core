using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class Services : Module
    {
        private List<Service> services;

        public Services(IEnumerable<Service> setup)
        {
            services = setup.ToList();
        }
    }
}