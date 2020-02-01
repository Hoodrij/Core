using System;
using System.Collections.Generic;
using System.Linq;
using Core.Utils.ExtensionMethods;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Ui
{
	public class UIController
	{
		private List<UIView> opened;
		private UIViewLoader loader;

		internal UIController()
		{
			opened = new List<UIView>();
			loader = new UIViewLoader();
		}

		internal UIView Get(UIInfo info)
		{
			UIView uiView = opened.Find(view => view.Info == info);
			return uiView;
		}

		internal void Open(UIInfo info, object data = null, Action<UIView> onOpen = null)
		{
			loader.Load(info, view =>
			{
				opened.Where(openedView => info.Root.IsClosingOther(openedView.Info.Root))
					.ToList()
					.ForEach(openedView => openedView.Close());

				Object.DontDestroyOnLoad(view);
				view.Open(data, info);
				opened.Add(view);
				onOpen?.Invoke(view);

				view.CloseAction = () =>
				{
					if (view == null) return;

					opened.Remove(view);
					view.OnClose();
					Object.Destroy(view.gameObject);
				};
			});
		}

//		internal void CloseAll(UICloseParams closeParams = UICloseParams.PopupAndWindowAndTopWindow)
//		{
//			List<Transform> rootToClose = closeParams.GetRoots().ToList();
//
//			opened.FindAll(view => rootToClose.Contains(view.Info.Root))
//				.ForEach(view =>
//				{
//					if (view != null)
//					{
//						view.CloseAction();
//					}
//				});
//		}
	}
}