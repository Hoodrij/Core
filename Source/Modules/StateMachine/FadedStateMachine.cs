namespace Core.StateMachine
{
    public class FadedStateMachine<T> : StateMachine<T> where T : State
    {
        [inject] Fader.Fader Fader;
        
        public new void Set(T state)
        {
            Fader.AddAction(async () => base.Set(state));
        }

        public void SetStateWithoutFade(T state)
        {
            base.Set(state);
        }
    }
}