using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Core.Ui
{
    public class UI
    {
        private readonly UIController controller;
        private readonly UILoader loader;

        internal UI(IEnumerable<UIRoot> setup)
        {
            loader = new UILoader();
            controller = new UIController(loader);

            Add(setup);
        }

        private void Add(IEnumerable<UIRoot> setup)
        {
            foreach (UIRoot root in setup)
            {
                if (root == null) continue;
                loader.AddRoot(root);
            }
        }

        internal async Task<TView> Open<TView>(object data = null) where TView : UIView => await controller.Open<TView>(data);

        internal TView Get<TView>() where TView : UIView => controller.Get<TView>();
        internal UIRoot GetRoot(Type type) => loader.GetRoot(type);
        
        internal void CloseAll() => controller.CloseAll();
    }
}