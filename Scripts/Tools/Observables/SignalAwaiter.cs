using System;
using System.Runtime.CompilerServices;

namespace Core.Tools.Observables
{
    public class SignalAwaiter : INotifyCompletion
    {
        private Signal signal;
        private bool isCompleted;
        private Action continuation;

        public SignalAwaiter(Signal signal)
        {
            this.signal = signal;
        }

        public bool IsCompleted => isCompleted;
        public void GetResult() { }

        public void OnCompleted(Action continuation)
        {
            this.continuation = continuation;
            signal.ListenOneshot(Listener);
        }

        private void Listener()
        {
            isCompleted = true;
            continuation();
        }
    }
    
    public class SignalAwaiter<T> : INotifyCompletion
    {
        private Signal<T> signal;
        private bool isCompleted;
        private Action continuation;
        private T result;

        public SignalAwaiter(Signal<T> signal)
        {
            this.signal = signal;
        }

        public bool IsCompleted => isCompleted;
        public T GetResult() => result;

        public void OnCompleted(Action continuation)
        {
            this.continuation = continuation;
            signal.ListenOneshot(Listener);
        }

        private void Listener(T result)
        {
            isCompleted = true;
            this.result = result;
            continuation();
        }
    }

    public static class SignalEx
    {
        public static SignalAwaiter GetAwaiter(this Signal s) => new SignalAwaiter(s);
        public static SignalAwaiter<T> GetAwaiter<T>(this Signal<T> s) => new SignalAwaiter<T>(s);
    }
}