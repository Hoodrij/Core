using System;
using System.Collections.Generic;
using Core.StateMachine;
using Core.Tools.Observables;

namespace Core.Units
{
    public class StateMachine<TState> where TState : State
    {
        protected TState CurrentState { get; private set; }
        
        private readonly Event<TState> enterEvent = new Event<TState>();
        private readonly Event<TState> exitEvent = new Event<TState>();

        public void Set(TState newState)
        {
            foreach (TState state in GetPath(CurrentState, newState))
            {
                if (state.IsParentOf(CurrentState) || state == CurrentState)
                    exitEvent.Fire(state);

                if (state.IsChildOf(CurrentState) || state.OnOtherBranch(CurrentState) || newState == CurrentState)
                    enterEvent.Fire(state);
            }

            CurrentState = newState;
        }

        private IEnumerable<TState> GetPath(TState fromState, TState toState)
        {
            // Same State
            if (toState == fromState) yield return toState;

            // UP by tree
            if (toState.IsParentOf(fromState))
            {
                yield return fromState;
                if (fromState.Parent != toState)
                    foreach (TState state in GetPath(toState, fromState.Parent as TState))
                        yield return state;
            }

            // DOWN by tree
            if (toState.IsChildOf(fromState))
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
                while (!commonParent.IsParentOf(toState))
                {
                    yield return commonParent;
                    commonParent = commonParent.Parent as TState;
                }

                foreach (TState state in GetPath(commonParent, toState))
                    yield return state;
            }
        }

        public void ListenEnter(TState requiredState, Action callback)
        {
            if (CurrentState == requiredState) callback();
            
            enterEvent.Listen(state =>
            {
                if (state.Is(requiredState)) 
                    callback();
            }, callback.Target);
        }
        
        public void ListenExit(TState requiredState, Action callback)
        {
            exitEvent.Listen(state =>
            {
                if (state.Is(requiredState)) 
                    callback();
            }, callback.Target);
        }
    }
}