using Core.Ui;

namespace Core.Samples.Ui
{
	public class UIRoots
	{
		[UIRootCloseParams(typeof(Window), typeof(Popup))]
		public class Menu : UIRoot { }
		
		[UIRootCloseParams(typeof(Popup))]
        public class Window : UIRoot { }
        
        public class Popup : UIRoot { }
        
        public class Hud : UIRoot { }
        
        public class Overlay : UIRoot { }
	}
}