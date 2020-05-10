using Core.StateMachine;

namespace Core.Samples.GameState
{
    public class SampleState : State
    {
        public static readonly SampleState Game = new SampleState("Game");
        public static readonly SampleState Menu = new SampleState("Menu", Game);
        public static readonly SampleState PlayMode = new SampleState("PlayMode", Game);

        private SampleState(string name, SampleState parent = null) : base(name, parent) { }
    }
}