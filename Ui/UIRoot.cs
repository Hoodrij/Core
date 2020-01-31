using System;
using System.Linq;
using UnityEngine;

namespace Core.Ui
{
	public class UIRoot
	{
		public string Name;
		public Transform Transform;
		
		private Func<UIRoot[]> rootsToClose;
		

		public UIRoot(string name, Func<UIRoot[]> rootsToClose = null)
		{
			Name = name;
			this.rootsToClose = rootsToClose;
			
			Game.UI.AddRoot(this);
		}
		
		public bool IsClosingOther(UIRoot other)
		{
			return rootsToClose().Contains(other);
		}
	}
}