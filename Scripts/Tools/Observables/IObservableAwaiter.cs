using System;
using System.Runtime.CompilerServices;

namespace Core.Tools.Observables
{
    public class IObservableAwaiter : INotifyCompletion
    {
        private readonly IObservable observable;
        private bool isCompleted;
        private Action continuation;

        internal IObservableAwaiter(IObservable observable)
        {
            this.observable = observable;
        }

        public bool IsCompleted => isCompleted;
        public void GetResult() { }

        public void OnCompleted(Action continuation)
        {
            this.continuation = continuation;
            observable.Listen(Listener);
        }

        private void Listener()
        {
            observable.Unsubscribe(this);
            isCompleted = true;
            continuation();
        }
    }
    
    public class IObservableAwaiter<T> : INotifyCompletion
    {
        private readonly IObservable<T> observable;
        private bool isCompleted;
        private Action continuation;
        private T result;

        internal IObservableAwaiter(IObservable<T> observable)
        {
            this.observable = observable;
        }

        public bool IsCompleted => isCompleted;
        public T GetResult() => result;

        public void OnCompleted(Action continuation)
        {
            this.continuation = continuation;
            observable.Listen(Listener);
        }

        private void Listener(T result)
        {
            observable.Unsubscribe(this);
            isCompleted = true;
            this.result = result;
            continuation();
        }
    }
}