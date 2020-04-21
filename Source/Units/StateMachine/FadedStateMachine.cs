namespace Core.StateMachine
{
    public class FadedStateMachine<T> : StateMachine<T> where T : State
    {
        public new void Set(T state)
        {
            // Game.Fader.AddAction(async () => base.Set(state));
        }

        public void SetStateWithoutFade(T state)
        {
            base.Set(state);
        }
    }
}