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
			foreach (string importedAsset in importedAssets)
			{
				Object asset = AssetDatabase.LoadAssetAtPath<Object>(importedAsset);
				GameObject gameObject = asset as GameObject;

				if (gameObject != null && gameObject.GetComponent(typeof(IFaderView)) == null) continue;
				
				asset.AddToPreloadedAssets();
			}
		}
	}
}