using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
	public class Assets
	{
		public Object Load(string path)
		{
			return Resources.Load(path);
		}
		
		public T Load<T>(string path) where T : Component
		{
			return Resources.Load<T>(path);
		}

		public ResourceRequest LoadAsync<T>(string path, Action<T> callback = null) where T : Component
		{
			ResourceRequest request = Resources.LoadAsync<T>(path);
			request.completed += operation => callback?.Invoke(request.asset as T);
			return request;
		}
	}
}