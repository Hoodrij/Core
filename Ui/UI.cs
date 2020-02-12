﻿using System;
using System.Collections.Generic;
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
		
		public void Add(IEnumerable<UIRoot> roots)
		{
			foreach (UIRoot root in roots)
			{
				loader.AddRoot(root);
			}
		}

		internal void Open<TView>(object data = null, Action<TView> onOpen = null) where TView : UIView 
			=> controller.Open(data, onOpen);
		
		internal UIView Get<TView>() where TView : UIView => controller.Get<TView>();
		
		internal UIRoot GetRoot(Type type) => loader.GetRoot(type);

		internal void CloseAll() => controller.CloseAll();
	}
}
