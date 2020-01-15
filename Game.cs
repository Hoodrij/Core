using Core.Ui;
using UnityEngine;

namespace Core
{
	public class Game
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void Run() => new Game();


		public static AppEvents AppEvents { get; }
		public static Services Services { get; }
		public static Models Models { get; }
		public static UI UI { get; }

		static Game()
		{
			AppEvents = new AppEvents();
			Models = new Models();
			Services = new Services();
			UI = new UI();
		}
	}
}