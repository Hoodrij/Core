using System.Collections.Generic;
using System.Linq;
using Core.Services;

namespace Core.Units
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