using System.Runtime.CompilerServices;
using Core.Abstract;
using Core.StateMachine;

namespace Core.Solutions.GameState
{
	public class GameStateModel : IModel
	{
		public GameState Current => StateMachine.Current;
		public StateMachine<GameState> StateMachine = new StateMachine<GameState>();
	}

	public class GameState : State
	{
		public static readonly GameState Game = new GameState("Game");
		public static readonly GameState Menu = new GameState("Menu", Game);
		public static readonly GameState PlayMode = new GameState("PlayMode", Game);


		private GameState(string name, GameState parent = null) : base(name, parent) { }
	}
}