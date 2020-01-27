using System;
using System.Linq;

namespace Core.Ui
{
	public class UIRoot
	{
		public string Name;
		public Func<UIRoot[]> RootsToClose;

		public UIRoot(string name, Func<UIRoot[]> rootsToClose = null)
		{
			Name = name;
			RootsToClose = rootsToClose;
			
			Game.UI.AddRoot(this);
		}
		
		public bool IsClosingOther(UIRoot other)
		{
			return false;
		}
	}
}