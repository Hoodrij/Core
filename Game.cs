using Core.Ui;

namespace Core
{
	public class Game
	{
		public static AppEvents AppEvents { get; } = new AppEvents();
		public static Services Services { get; } = new Services();
		public static Models Models { get; } = new Models();
		public static UI UI { get; } = new UI();
		public static Fade Fade { get; } = new Fade();
	}
}