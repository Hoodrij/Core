using System;
using Core.StateMachine;

namespace Core.Abstract
{
	public class StateModel<T> : IModel where T : State
	{
		private StateMachine<T> StateMachine = new StateMachine<T>();
		
		public T Current => StateMachine.Current;
		public void SetState(T state) => StateMachine.SetState(state);
		public void ListenEnter(T state, Action callback) => StateMachine.ListenEnter(state, callback);
		public void ListenExit(T state, Action callback) => StateMachine.ListenExit(state, callback);
	}
}