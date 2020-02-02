using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Object = UnityEngine.Object;

namespace Core.Ui
{
	internal class UIController
	{
		private List<UIView> opened;
		private UILoader loader;

		public UIController(UILoader loader)
		{
			opened = new List<UIView>();
			this.loader = loader;
		}

		public UIView Get<TView>() where TView : UIView
		{
			return opened.Find(view => view.GetType() == typeof(TView));
		}

		public void Open<TView>(object data = null, Action<TView> onOpen = null) where TView : UIView
		{
			UIInfoAttribute info = UIView<TView>.Info;
			
			loader.Load<TView>(info, view =>
			{
				opened.Where(openedView => info.Root.IsClosingOther(openedView.Info.Root))
					.ToList()
					.ForEach(openedView => openedView.Close());

				Object.DontDestroyOnLoad(view);
				view.Initialize(data);
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

		internal void CloseAll()
		{
			opened.ForEach(view => view.Close());
		}
	}
}