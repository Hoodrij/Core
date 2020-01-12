using Core.Ui;

namespace Core
{
	public class Game
	{
		public static AppEvents AppEvents { get; }
		public static UI UI { get; }
		public static Models Models { get; }
		public static Services Services { get; }

		static Game()
		{
			AppEvents = new AppEvents();
			UI = new UI();
			Models = new Models();
			Services = new Services();
		}
	}
}