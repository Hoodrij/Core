using System;
using Core.StateMachine;

namespace Core.Samples.GameState
{
    [Serializable]
    public class SampleState : State
    {
        public static readonly SampleState Game = new SampleState(nameof(Game));
        public static readonly SampleState Menu = new SampleState(nameof(Menu), Game);
        public static readonly SampleState PlayMode = new SampleState(nameof(PlayMode), Game);

        private SampleState(string name, SampleState parent = null) : base(name, parent) { }
    }
}