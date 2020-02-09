using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Ui
{
	internal class UILoader
	{
		private List<UIRoot> roots = new List<UIRoot>();
		private GameObject uiGO;

		public void SetCanvas(GameObject go)
		{
			uiGO = go;
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
				Game.Assets.LoadAsync<TView>(info.Path, Instantiate);
			}
			else
			{
				var asset = Game.Assets.Load<TView>(info.Path);
				Instantiate(asset);
			}

			void Instantiate(Object resource)
			{
				Transform root = GetRoot(info.RootType).Transform;
				UIView view = Object.Instantiate(resource, root) as UIView;
				callback.Invoke(view as TView);
			}
		}

		public UIRoot GetRoot(Type type)
		{
			return roots.Find(root => root.GetType() == type);
		}
	}
}