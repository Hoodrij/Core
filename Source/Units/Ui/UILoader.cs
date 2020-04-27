using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Assets;
using Core.Ui.Attributes;
using Core.Ui.Components;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Ui
{
    internal class UILoader
    {
        private IAssets Assets;
        
        private List<UIRoot> roots = new List<UIRoot>();
        private GameObject uiGo;
        private bool isInitialized;

        public UILoader(IAssets assets)
        {
            this.Assets = assets;

            SpawnDefaultCanvas();
        }

        private async void SpawnDefaultCanvas()
        {
            uiGo = await Assets.Spawn("UI", true);
            isInitialized = true;
        }

        public async void AddRoot(UIRoot root)
        {
            await new WaitUntil(() => isInitialized);
            
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
            await new WaitUntil(() => isInitialized);
            
            TView view = await Assets.Load<TView>(info.Path);
        
            Transform root = GetRoot(info.RootType).Transform;
            return Object.Instantiate(view, root);
        }

        public UIRoot GetRoot(Type type)
        {
            return roots.Find(root => root.GetType() == type);
        }
    }
}