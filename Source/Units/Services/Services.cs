using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class Services : Unit
    {
        private List<Service> services;

        public Services(IEnumerable<Service> setup)
        {
            services = setup.ToList();
        }
    }
}