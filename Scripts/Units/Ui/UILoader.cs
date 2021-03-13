using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Units;
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
            uiGo.name = "UI";
        }

        private UIRoot AddRoot(UIRoot root)
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
            RefreshRootsOrder();
            return root;
        }

        private void RefreshRootsOrder()
        {
            roots = roots.OrderBy(root => root.Order).ToList();
            roots.ForEach(root => root.Transform.SetSiblingIndex(root.Order));
        }

        public async Task<TView> Load<TView>(UIInfoAttribute info) where TView : UIView
        {
            TView prefab = await assets.Load<TView>(info.Path);

            Transform root = GetRoot(info.RootType).Transform;
            TView view = Object.Instantiate(prefab, root);
            return view;
        }

        private UIRoot GetRoot(Type type)
        {
            UIRoot uiRoot = roots.Find(root => root.GetType() == type) 
                            ?? AddRoot((UIRoot) Activator.CreateInstance(type));
            return uiRoot;
        }
    }
}