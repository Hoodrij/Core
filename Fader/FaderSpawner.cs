using UnityEditor;
using UnityEngine;

namespace Core
{
	public class FaderSpawner
	{
		public IFaderView SpawnView()
		{
			Component prefab = GetFaderViewPrefab();

			Component instantiatedView = Object.Instantiate(prefab);
			Object.DontDestroyOnLoad(instantiatedView);

			return instantiatedView as IFaderView;
		}

		private Component GetFaderViewPrefab()
		{
			foreach (Object asset in PlayerSettings.GetPreloadedAssets())
			{
				if (!(asset is GameObject gameObject)) continue;

				Component view = gameObject.GetComponent(typeof(IFaderView));
				if (view == null) continue;

				return view;
			}

			return null;
		}
	}
}