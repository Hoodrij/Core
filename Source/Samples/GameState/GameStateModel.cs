using Core.Abstract;
using Core.StateMachine;

namespace Core.Samples.GameState
{
    public class GameStateModel : Model
    {
        public FadedStateMachine<GameState> FSM = new FadedStateMachine<GameState>();
    }

    public class GameState : State
    {
        public static readonly GameState Game = new GameState("Game");
        public static readonly GameState Menu = new GameState("Menu", Game);
        public static readonly GameState PlayMode = new GameState("PlayMode", Game);


        private GameState(string name, GameState parent = null) : base(name, parent) { }
    }
}