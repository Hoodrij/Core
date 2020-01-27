using Core.Abstract;
using UnityEditor;
using UnityEngine;

namespace Core.Samples.Fader
{
	public class FaderSpawnService : Service
	{
		public FaderSpawnService()
		{
			SpawnFaderView();
		}

		private void SpawnFaderView()
		{
			foreach (Object asset in PlayerSettings.GetPreloadedAssets())
			{
				if (!(asset is GameObject gameObject)) continue;

				Component view = gameObject.GetComponent(typeof(IFaderView));
				if (view == null) continue;

				Component instantiatedView = Object.Instantiate(view);
				Object.DontDestroyOnLoad(instantiatedView);
				break;
			}
		}
	}
}