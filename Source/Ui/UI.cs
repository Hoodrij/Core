﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Core.Ui
{
    public class UI
    {
        private UIController controller;
        private UILoader loader;

        internal UI()
        {
            loader = new UILoader();
            controller = new UIController(loader);
        }

        public void Add(UIRoot root) => loader.AddRoot(root);

        public void Add(IEnumerable<UIRoot> roots)
        {
            foreach (var root in roots)
            {
                if (root == null) continue;
                Add(root);
            }
        }

        internal async Task<TView> Open<TView>(object data = null) where TView : UIView => await controller.Open<TView>(data);

        internal TView Get<TView>() where TView : UIView => controller.Get<TView>();
        internal UIRoot GetRoot(Type type) => loader.GetRoot(type);
        
        internal void CloseAll() => controller.CloseAll();
    }
}