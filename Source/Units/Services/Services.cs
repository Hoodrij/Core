using System.Collections.Generic;
using System.Linq;
using Core.GameSetup;
using Core.Tools;

namespace Core
{
    public class Services : Unit
    {
        [inject] IGameSetup Setup;
        [inject] Events Events;

        private List<Service> services;

        protected override void OnStart()
        {
            "[Services] [OnStart]".log();

            services = services ?? Setup.Services().ToList();
            
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