using System.Collections.Generic;
using Core.StateMachine;

namespace Core.Units
{
    public class StateMachine<TState> where TState : State
    {
        private TState Current { get; set; }

        public void Set(TState newState)
        {
            foreach (TState state in GetPath(Current, newState))
            {
                if (state.IsParentOf(Current) || state == Current)
                    state.Exit();

                if (state.IsChildOf(Current) || state.OnOtherBranch(Current) || newState == Current)
                    state.Enter();
            }

            Current = newState;
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
    }
}