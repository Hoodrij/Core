using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Core.Ui
{
	public abstract class UIRoot
	{
		public Transform Transform { get; internal set; }
		private Type[] rootsToClose;

		protected UIRoot()
		{
			UIRootCloseParamsAttribute paramsAttribute = GetType().GetCustomAttribute<UIRootCloseParamsAttribute>();
			if (paramsAttribute != null)
			{
				rootsToClose = paramsAttribute.RootsToClose;
			}
		}

		public bool IsClosingOther(UIRoot other)
		{
			return rootsToClose.Contains(other.GetType());
		}
	}
}