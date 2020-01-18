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

		private void Add(Service service)
		{
			services.Add(service);
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