using System;
using System.Collections.Generic;
using Core.Abstract;

namespace Core
{
	public class Models
	{
		private Dictionary<Type, IModel> map;

		public Models()
		{
			map = new Dictionary<Type, IModel>();
		}
		
		private void Add(IModel model)
		{
			map.Add(model.GetType(), model);
			Game.Injector.Add(model);
		}

		public void Add(IEnumerable<IModel> models)
		{
			foreach (IModel model in models)
			{
				Add(model);
			}
		}

		public T Get<T>() where T : IModel
		{
			return (T) map[typeof(T)];
		}
	}
}