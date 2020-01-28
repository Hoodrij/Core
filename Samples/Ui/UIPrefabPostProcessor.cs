using System;
using Core.Ui;
using Core.Utils.ExtensionMethods;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Samples.Ui
{
    public class UIPrefabPostProcessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (HasPreloadedUIPrefab()) return;
			
            foreach (string path in importedAssets)
            {
                Type type = AssetDatabase.GetMainAssetTypeAtPath(path);
                if (type != typeof(GameObject)) continue;
				
                GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (asset.GetComponent(typeof(UITag)) == null) continue;
				
                asset.AddToPreloadedAssets();
            }
        }

        private static bool HasPreloadedUIPrefab()
        {
            foreach (Object asset in PlayerSettings.GetPreloadedAssets())
            {
                if (!(asset is GameObject gameObject)) continue;

                Component view = gameObject.GetComponent(typeof(UITag));
                if (view != null) return true;
            }

            return false;
        }
    }
}