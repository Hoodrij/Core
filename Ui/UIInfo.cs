using System;
using UnityEngine;

namespace Core.Ui
{
	public abstract class UIInfo
	{
		public bool AsyncLoad;
		public Type Type;
		public string Path;
		public UIRoot Root;

		protected UIInfo(Type type, string path, UIRoot root, bool asyncLoad = true)
		{
			Type = type;
			Path = path;
			Root = root;
			AsyncLoad = asyncLoad;
		}

		public abstract void Open(Action<UIView> onOpen = null);
	}



	public class UIInfo<TView> : UIInfo where TView : UIView
	{
		public UIInfo(string path, UIRoot root, bool asyncLoad = true) : base(typeof(TView), path, root, asyncLoad) { }

		public void Open(Action<TView> onOpen = null)
		{
			Game.UI.Open(this, null, view => onOpen?.Invoke((TView)view));
		}

		[Obsolete("Requires Type", true)]
		public override void Open(Action<UIView> onOpen = null) => Open(onOpen);
	}



	public class UIInfo<TView, TData> : UIInfo<TView> where TView : UIView<TData>
	{
		public UIInfo(string path, UIRoot root, bool asyncLoad = true) : base(path, root, asyncLoad) { }

		public void Open(TData data = default, Action<TView> onOpen = null)
		{
			Game.UI.Open(this, data, view => onOpen?.Invoke((TView)view));
		}

		[Obsolete("Requires Data", true)]
		public override void Open(Action<UIView> onOpen = null) => Open(onOpen);
	}
}