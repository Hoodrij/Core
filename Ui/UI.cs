using System;
using System.Collections.Generic;
using UnityEditor;

namespace Core.Ui
{
	public class UI
	{
		private UIController controller;
		private UIGenerator generator;

		public UI()
		{
			controller = new UIController();
			generator = new UIGenerator();
		}

		internal void Open(UIInfoAttribute info, UIData data = null, Action<UIView> onOpen = null)
		{
			controller.Open(info, data, onOpen);
		}
		
//		internal UIView Get(UIInfoAttribute info)
//		{
//			return controller.Get(info);
//		}

//		internal void CloseAll(UICloseParams closeParams = UICloseParams.PopupAndWindowAndTopWindow)
//		{
//			controller.CloseAll(closeParams);
//		}
		
		public void Add(IEnumerable<UIRoot> roots)
		{
			foreach (UIRoot root in roots)
			{
				generator.AddRoot(root);
			}
		}
	}
}
