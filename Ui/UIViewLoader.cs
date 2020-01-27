using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Ui
{
	public class UIViewLoader
	{
		public void Load(UIInfo info, Action<UIView> callback)
		{
			if (info.AsyncLoad)
			{
				Resources.LoadAsync<UIView>(info.Path)
					.completed += async => Instantiate((async as ResourceRequest)?.asset);
			}
			else
			{
				var asset = Resources.Load<UIView>(info.Path);
				Instantiate(asset);
			}

			void Instantiate(Object resource)
			{
				UIView view = Object.Instantiate(resource, info.Root.Transform) as UIView;
				callback.Invoke(view);
			}
		}
	}
}