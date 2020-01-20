using System;

namespace Core.StateMachine
{
	public class FadedStateMachine<T> : StateMachine<T> where T : State
	{
		public new void SetState(T state)
		{
			Game.Fade.AddAction(() => base.SetState(state));
		}
		
		public void SetStateWithoutFade(T state)
		{
            base.SetState(state);
		}
	}
}