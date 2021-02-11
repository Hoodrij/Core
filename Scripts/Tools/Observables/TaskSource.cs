using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Tools.Observables
{
    public static class TaskSourceEx
    {
        public static TaskSourceAwaiter<T> GetAwaiter<T>(this TaskSource<T> t) => new TaskSourceAwaiter<T>(t);
    }

    public class TaskSourceAwaiter<T> : INotifyCompletion
    {
        private TaskSource<T> taskSource;
            
        public TaskSourceAwaiter(TaskSource<T> taskSource)
        {
            this.taskSource = taskSource;
        }
        
        public bool IsCompleted => taskSource.IsCompleted;
        public T GetResult() => taskSource.Result;

        public void OnCompleted(Action continuation)
        {
            taskSource.OnCompleted += continuation;
        }
    }
    
    public class TaskSource<T>
    {
        internal bool IsCompleted;
        internal T Result;
        internal event Action OnCompleted;
        
        public void Complete(T t)
        {
            Result = t;
            IsCompleted = true;
            OnCompleted?.Invoke();
            OnCompleted = null;
        }
    }
}