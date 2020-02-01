using System;
using System.Collections.Generic;
using Core.Utils.ExtensionMethods;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Ui
{
	internal class UILoader
	{
		private List<UIRoot> roots = new List<UIRoot>();
		private UITag uiGO;
		
		public UILoader()
		{
			UITag uiPrefab = GetUIPrefab();
			uiGO = Object.Instantiate(uiPrefab);
			Object.DontDestroyOnLoad(uiGO);
		}
		
		public void AddRoot(UIRoot root)
		{
			roots.Add(root);
			
			GameObject rootGO = new GameObject(root.GetType().Name, typeof(RectTransform));
			rootGO.transform.SetParent(uiGO.transform, false);
			
			RectTransform rectTransform = rootGO.GetComponent<RectTransform>();
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.offsetMin = Vector2.zero;
			rectTransform.offsetMax = Vector2.zero;
			
			root.Transform = rectTransform;
		}
		
		public void Load<TView>(UIInfoAttribute info, Action<TView> callback) where TView : UIView
		{
			if (info.AsyncLoad)
			{
				Resources.LoadAsync<UIView>(info.Path)
					.completed += async => Instantiate((async as ResourceRequest)?.asset);
			}
			else
			{
				var asset = Resources.Load<UIView>(info.Path);
				Instantiate(asset);
			}

			void Instantiate(Object resource)
			{
				Transform root = GetRoot(info.Root).Transform;
				UIView view = Object.Instantiate(resource, root) as UIView;
				callback.Invoke(view as TView);
			}
		}

		public UIRoot GetRoot(Type type)
		{
			return roots.Find(root => root.GetType() == type);
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