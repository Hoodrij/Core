using Core.Ui;
using Injection;
using UnityEngine;

namespace Core
{
	public class Game
	{
		public static AppEvents AppEvents { get; private set; }
		public static Coroutiner Coroutiner { get; private set; }
		public static Services Services { get; private set; }
		public static Models Models { get; private set; }
		public static UI UI { get; private set; }
		public static Fader Fader { get; private set; }

		public Game()
		{
			AppEvents = new AppEvents();
			Coroutiner = new Coroutiner();
			Services = new Services();
			Models = new Models();
			UI = new UI();
			Fader = new Fader();
		}
	}
}