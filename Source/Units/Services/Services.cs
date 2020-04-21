using System.Collections.Generic;
using System.Linq;
using Core.GameSetup;
using Core.Tools;

namespace Core
{
    public class Services : Unit
    {
        [inject] Events Events;

        private List<Service> services;

        public Services(IGameSetup setup)
        {
            services = setup.Services().ToList();
        }

        protected override void OnStart()
        {
            "[Services] [OnStart]".log();
            
            foreach (Service service in services)
            {
                Injector.Instance.Populate(service);
                service.Life = Life.Derive(service.GetType().Name);
                service.OnStart();
                
                if (service is IUpdateHandler iUpdate)
                {
                    Events.OnUpdate.Listen(iUpdate.Update);
                }
            }
        }
    }
}