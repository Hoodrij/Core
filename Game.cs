using Core.Ui;

namespace Core
{
	public class Game
	{
		public static AppEvents AppEvents { get; }
		public static UI UI { get; }

		static Game()
		{
			AppEvents = new AppEvents();
			UI = new UI();
		}
	}
}