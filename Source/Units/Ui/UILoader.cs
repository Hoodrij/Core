using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Assets;
using Core.Ui;
using Core.Ui.Components;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Ui
{
    internal class UILoader
    {
        private IAssets assets;
        
        private List<UIRoot> roots = new List<UIRoot>();
        private GameObject uiGo;

        public UILoader(IAssets assets)
        {
            this.assets = assets;
            
            uiGo = Object.Instantiate(Resources.Load<GameObject>("UI"));
        }

        public async void AddRoot(UIRoot root)
        {
            roots.Add(root);

            GameObject rootGO = new GameObject(root.GetType().Name, typeof(RectTransform));
            rootGO.transform.SetParent(uiGo.transform, false);

            RectTransform rectTransform = rootGO.GetComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            root.Transform = rectTransform;
        }

        public async Task<TView> Load<TView>(UIInfoAttribute info) where TView : UIView
        {
            TView view = await assets.Load<TView>(info.Path);
            
            Transform root = GetRoot(info.RootType).Transform;
            return Object.Instantiate(view, root);
        }

        public UIRoot GetRoot(Type type)
        {
            return roots.Find(root => root.GetType() == type);
        }
    }
}