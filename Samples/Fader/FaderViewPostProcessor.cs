using System;
using Core.Utils.ExtensionMethods;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Samples.Fader
{
	public class FaderViewPostProcessor : AssetPostprocessor
	{
		private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
		{
			if (HasPreloadedFader()) return;
			
			foreach (string path in importedAssets)
			{
				Type type = AssetDatabase.GetMainAssetTypeAtPath(path);
				if (type != typeof(GameObject)) continue;
				
				GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(path);
				if (asset.GetComponent(typeof(IFaderView)) == null) continue;
				
				asset.AddToPreloadedAssets();
			}
		}

		private static bool HasPreloadedFader()
		{
			foreach (Object asset in PlayerSettings.GetPreloadedAssets())
			{
				if (!(asset is GameObject gameObject)) continue;

				Component view = gameObject.GetComponent(typeof(IFaderView));
				if (view != null) return true;
			}

			return false;
		}
	}
}