using System;
using System.Runtime.CompilerServices;

namespace Core.Tools.Observables
{
    public class EventAwaiter : INotifyCompletion
    {
        private Event @event;
        private bool isCompleted;
        private Action continuation;

        internal EventAwaiter(Event @event)
        {
            this.@event = @event;
        }

        public bool IsCompleted => isCompleted;
        public void GetResult() { }

        public void OnCompleted(Action continuation)
        {
            this.continuation = continuation;
            @event.ListenOneshot(Listener);
        }

        private void Listener()
        {
            isCompleted = true;
            continuation();
        }
    }
    
    public class EventAwaiter<T> : INotifyCompletion
    {
        private Event<T> @event;
        private bool isCompleted;
        private Action continuation;
        private T result;

        internal EventAwaiter(Event<T> @event)
        {
            this.@event = @event;
        }

        public bool IsCompleted => isCompleted;
        public T GetResult() => result;

        public void OnCompleted(Action continuation)
        {
            this.continuation = continuation;
            @event.ListenOneshot(Listener);
        }

        private void Listener(T result)
        {
            isCompleted = true;
            this.result = result;
            continuation();
        }
    }
}