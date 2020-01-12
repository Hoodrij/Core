using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core
{
	public class Models
	{
		private Dictionary<Type, IModel> map = new Dictionary<Type, IModel>();

		public Models()
		{
			Type providerType = Assembly.GetExecutingAssembly()
				.GetTypes()
				.LastOrDefault(type => type.IsAssignableFrom(typeof(IModelsProvider)) && !type.IsInterface);

			if (providerType == null) return;

			IModelsProvider provider = (IModelsProvider)Activator.CreateInstance(providerType);

			foreach (Type type in provider.Get())
			{
				Add(type);
			}
		}

		public void Add(Type type)
		{
			IModel model = (IModel)Activator.CreateInstance(type);

			map.Add(type, model);
		}

		public T Get<T>() where T : IModel
		{
			Type type = typeof(T);

			if (!map.ContainsKey(type))
			{
				Add(type);
			}

			return (T)map[typeof(T)];
		}
	}
}
