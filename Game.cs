using Core.Ui;
using Injection;

namespace Core
{
	public class Game
	{
		internal static Injector Injector { get; } = new Injector();
		public static AppEvents AppEvents { get; } = new AppEvents();
		public static Coroutiner Coroutiner { get; } = new Coroutiner();
		public static Services Services { get; } = new Services();
		public static Models Models { get; } = new Models();
		public static UI UI { get; } = new UI();
		public static Fader Fader { get; } = new Fader();
	}
}