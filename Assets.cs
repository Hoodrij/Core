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

		public T Spawn<T>(string path, bool persistent = false) where T : class
		{
			Object prefab = Load(path);

			GameObject go = Object.Instantiate(prefab) as GameObject;

			if (persistent)
			{
				Object.DontDestroyOnLoad(go);
			}

			if (typeof(T) == typeof(GameObject))
			{
				return go as T;
			}

			return go.GetComponent(typeof(T)) as T;
		}

		public GameObject Spawn(string path, bool persistent = false)
		{
			return Spawn<GameObject>(path, persistent);
		}
	}
}