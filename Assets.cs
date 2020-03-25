using System;
using System.Threading.Tasks;
using Core.Tools.ExtensionMethods;
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

		public async Task<T> LoadAsync<T>(string path) where T : Component
		{
			var asset = await Resources.LoadAsync<T>(path);
			return asset as T;
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