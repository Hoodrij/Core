using System;
using System.Collections.Generic;
using Core.Tools.Observables;

namespace Core.StateMachine
{
	public class StateMachine<TState> where TState : State
	{
		private Signal<TState> onEnter = new Signal<TState>();
		private Signal<TState> onExit = new Signal<TState>();

		public TState Current { get; private set; }

		public void SetState(TState newState)
		{
			foreach (TState state in GetPath(Current, newState))
			{
				if (state.IsParent(Current) || state == Current)
				{
					onExit.Fire(state);
				}

				if (state.IsChild(Current) || state.OnOtherBranch(Current) || newState == Current)
				{
					onEnter.Fire(state);
				}
			}

			Current = newState;
		}

		private IEnumerable<TState> GetPath(TState fromState, TState toState)
		{
			// Same State
			if (toState == fromState)
			{
				yield return toState;
			}

			// UP by tree
			if (toState.IsParent(fromState))
			{
				yield return fromState;
				if (fromState.Parent != toState)
					foreach (TState state in GetPath(toState, fromState.Parent as TState))
						yield return state;
			}

			// DOWN by tree
			if (toState.IsChild(fromState))
			{
				if (toState.Parent != fromState)
					foreach (TState state in GetPath(fromState, toState.Parent as TState))
						yield return state;
				yield return toState;
			}

			// Into the parallel branch of a tree
			if (toState.OnOtherBranch(fromState))
			{
				TState commonParent = fromState;
				while (!commonParent.IsParent(toState))
				{
					yield return commonParent;
					commonParent = commonParent.Parent as TState;
				}

				foreach (TState state in GetPath(commonParent, toState))
					yield return state;
			}
		}

		public void ListenEnter(TState enterState, Action action)
		{
			onEnter.Listen(state =>
			{
				if (state.Is(enterState))
				{
					action();
				}
			});
		}

		public void ListenExit(TState exitState, Action action)
		{
			onExit.Listen(state =>
			{
				if (state.Is(exitState))
				{
					action();
				}
			});
		}
	}
}