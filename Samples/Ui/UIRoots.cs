using Core.Ui;

namespace Core.Samples.Ui
{
    public static class UIRoots
    {
        [UIRootInfo(typeof(Menu), 1, typeof(Window), typeof(Popup))]
        public class Menu : UIRoot { }

        [UIRootInfo(typeof(Window), 2, typeof(Popup))] 
        public class Window : UIRoot { }

        [UIRootInfo(typeof(Popup), 3)] 
        public class Popup : UIRoot { }

        [UIRootInfo(typeof(Hud), 4)] 
        public class Hud : UIRoot { }

        [UIRootInfo(typeof(Overlay), 5)] 
        public class Overlay : UIRoot { }
    }
}