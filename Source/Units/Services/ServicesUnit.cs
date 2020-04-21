using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class ServicesUnit : Unit
    {
        private List<Service> services;

        public ServicesUnit(IEnumerable<Service> setup)
        {
            services = setup.ToList();
        }
    }
}