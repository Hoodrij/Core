using Core.StateMachine;
using Core.Tools;

namespace Core.Units
{
    public class FadedStateMachine<T> : StateMachine<T> where T : State
    {
        Fader Fader => Injector.Instance.Get<Fader>();
        
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