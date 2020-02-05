using System;
using Core.Ui;
using Core.Utils.ExtensionMethods;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
	public class Assets
	{
		public Object Get<T>(string path) where T : Object
		{
			return Resources.Load<T>(path);
		}

		public ResourceRequest GetAsync<T>(string path, Action<T> callback = null) where T : Object
		{
			ResourceRequest request = Resources.LoadAsync<T>(path);
			request.completed += operation => callback?.Invoke(request.asset as T);
			return request;
		}

		public T GetPreloaded<T>() where T : Component
		{
			return GetPreloaded(typeof(T)) as T;
		}

		public Component GetPreloaded(Type type)
		{
			foreach (Object asset in PlayerSettings.GetPreloadedAssets())
			{
				if (!(asset is GameObject gameObject)) continue;

				Component component = gameObject.GetComponent(type);
				if (component == null) continue;

				return component;
			}

			return null;
		}
	}
}