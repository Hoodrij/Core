using UnityEngine;

namespace Core.Tools.Observables
{
    public class TaskSource<T> : CustomYieldInstruction
    {
        private bool isCompleted;
        private T result;
        
        public void SetResult(T t)
        {
            result = t;
            isCompleted = true;
        }
        
        public static implicit operator T(TaskSource<T> taskSource) => taskSource.result;

        public override bool keepWaiting => !isCompleted;
    }
}