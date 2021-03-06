using System;

namespace Core.StateMachine
{
    [Serializable]
    public class State : IEquatable<State>
    {
        public State Parent { get; }
        public string Name { get; }
        
        public State(string name, State parent = null)
        {
            Name = name;
            Parent = parent;
        }

#region Comparers
        
        public bool Is(State other)
        {
            State state = this;
            if (ReferenceEquals(state, other) || state?.Name == other?.Name) return true;

            return false;
        }

        public bool IsChildOf(State other)
        {
            if (other == null) return true;
            State state = Parent;
            while (!ReferenceEquals(state, null))
            {
                if (ReferenceEquals(state, other) || state.Name == other?.Name) return true;

                state = state.Parent;
            }

            return false;
        }

        public bool IsParentOf(State other)
        {
            if (other == null) return false;
            State state = other.Parent;
            while (!ReferenceEquals(state, null))
            {
                if (ReferenceEquals(state, this) || state.Name == Name) return true;

                state = state.Parent;
            }

            return false;
        }

        public static bool operator ==(State s1, State s2)
        {
            return s1?.Equals(s2) ?? ReferenceEquals(null, s2);
        }

        public static bool operator !=(State s1, State s2)
        {
            if (ReferenceEquals(null, s1)) return !ReferenceEquals(null, s2);

            return !s1.Equals(s2);
        }

        protected bool Equals(State other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Is(other);
        }

        public override string ToString() => Name;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((State) obj);
        }

        bool IEquatable<State>.Equals(State other)
        {
            return Equals(other);
        }

        public bool OnOtherBranch(State other)
        {
            return !IsParentOf(other) && !IsChildOf(other) && !Is(other);
        }

#endregion
    }
}