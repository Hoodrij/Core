using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Abstract;

namespace Core
{
	public class Services
	{
		private List<Service> services = new List<Service>();
		private List<RuntimeService> runtimeServices = new List<RuntimeService>();

		public Services()
		{
			Game.AppEvents.OnUpdate.Listen(Update);
		}

		private void Update()
		{
			runtimeServices.ForEach(service => service.Update());
		}

		private void Add(Service service)
		{
			Game.Injector.Inject(service);
			service.Start();
			
			services.Add(service);
			
			if (service is RuntimeService runtimeService)
			{
				runtimeServices.Add(runtimeService);
			}
		}

		public void Add(IEnumerable<Service> services)
		{
			foreach (Service service in services)
			{
				Add(service);
			}
		}
	}
}