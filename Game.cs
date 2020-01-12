using Core.Ui;
using UnityEngine;

namespace Core
{
	public class Game
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void Run() => new Game();


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