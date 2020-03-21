using Core.Ui;

namespace Core
{
	public class Game
	{
		public static Lifetime Lifetime { get; private set; }
		public static Coroutiner Coroutiner { get; private set; }
		public static Assets Assets { get; private set; }
		public static Services Services { get; private set; }
		public static Models Models { get; private set; }
		public static UI UI { get; private set; }
		public static Fader Fader { get; private set; }

		public Game()
		{
			Lifetime = new Lifetime();
			Coroutiner = new Coroutiner();
			Assets = new Assets();
			Services = new Services();
			Models = new Models();
			UI = new UI();
			Fader = new Fader();
		}
	}
}