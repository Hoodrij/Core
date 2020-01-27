using System;

namespace Core.Ui
{
	public class UI
	{
		private UIController controller;
		private UIGenerator generator;

		public UI()
		{
			controller = new UIController();
		}

		internal void Open(UIInfo info, UIData data = null, Action<UIView> onOpen = null)
		{
			controller.Open(info, data, onOpen);
		}
		
		internal UIView Get(UIInfo info)
		{
			return controller.Get(info);
		}

//		internal void CloseAll(UICloseParams closeParams = UICloseParams.PopupAndWindowAndTopWindow)
//		{
//			controller.CloseAll(closeParams);
//		}
		
		internal void AddRoot(UIRoot root)
		{
			generator.AddRoot(root);
		}
	}
}
