using System;

namespace Core.Tools.Observables
{
    public class Command : Signal
    {
        public new void Listen(Action action)
        {
            Listeners.Clear();
            base.Listen(action);
        }
    }

    public class Command<T> : Signal<T>
    {
        public new void Listen(Action<T> action)
        {
            Listeners.Clear();
            base.Listen(action);
        }
    }
}