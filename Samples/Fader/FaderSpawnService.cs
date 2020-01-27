using Core.Abstract;
using UnityEditor;
using UnityEngine;

namespace Core.Samples.Fader
{
	public class FaderSpawnService : Service
	{
		public FaderSpawnService()
		{
			Component prefab = GetFaderViewPrefab();

			Component instantiatedView = Object.Instantiate(prefab);
			Object.DontDestroyOnLoad(instantiatedView);
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