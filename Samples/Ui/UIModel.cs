using Core.Abstract;
using Core.Ui;

namespace Core.Samples.Ui
{
	public class UIRoots
	{
		public static UIRoot Menu = new UIRoot("Menu", () => new [] { Window, Popup });
		public static UIRoot Window = new UIRoot("Window", () => new [] { Popup });
		public static UIRoot Popup = new UIRoot("Popup");
		public static UIRoot Hud = new UIRoot("Hud");
		public static UIRoot Overlay = new UIRoot("Overlay");
	}
	
	public class UIModel : IModel
	{
		private const string UI_PREFABS_PATH = "UI Prefabs/";
		
		public readonly UIInfo SampleView = new UIInfo<SampleView>(UI_PREFABS_PATH + "Menu", UIRoots.Menu);
	}
}