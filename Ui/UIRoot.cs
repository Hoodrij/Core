using System;
using System.Linq;
using UnityEngine;

namespace Core.Ui
{
	public abstract class UIRoot
	{
		protected virtual Type[] rootsToClose { get; }

		protected UIRoot(params Type[] rootsToClose)
		{
			this.rootsToClose = rootsToClose;
		}

		public bool IsClosingOther(UIRoot other)
		{
			return false;
		}
	}
}