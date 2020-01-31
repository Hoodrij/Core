using Core.Utils.ExtensionMethods;
using UnityEditor;
using UnityEngine;

namespace Core.Ui
{
	public class UIGenerator
	{
		UITag uiGO;
		
		public UIGenerator()
		{
			UITag uiPrefab = GetUIPrefab();
			uiGO = Object.Instantiate(uiPrefab);
			Object.DontDestroyOnLoad(uiGO);
		}
		
		internal void AddRoot(UIRoot root)
		{
//			GameObject rootGO = new GameObject(root.Name, typeof(RectTransform));
//			rootGO.transform.SetParent(uiGO.transform, false);
//			
//			RectTransform rectTransform = rootGO.GetComponent<RectTransform>();
//			rectTransform.anchorMin = Vector2.zero;
//			rectTransform.anchorMax = Vector2.one;
//			rectTransform.offsetMin = Vector2.zero;
//			rectTransform.offsetMax = Vector2.zero;
//			
//			root.Transform = rectTransform;
		}
		
		private UITag GetUIPrefab()
		{
			foreach (Object asset in PlayerSettings.GetPreloadedAssets())
			{
				if (!(asset is GameObject gameObject)) continue;

				UITag view = gameObject.GetComponent<UITag>();
				if (view == null) continue;

				return view;
			}

			return null;
		}
	}
}