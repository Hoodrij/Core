using System;
using System.Collections;
using System.Collections.Generic;
using Core.Tools.Observables;
using UnityEngine;

namespace Core.Tools.Task
{
	public class TaskSequence
    {
        public float Progress => (float) tasks.Count / initialTasksCount;
        public readonly Signal<float> OnProgressChanged = new Signal<float>();
        
        private readonly Queue<BaseTask> tasks = new Queue<BaseTask>();
        private Action onCompleted;
        private int initialTasksCount;

        public TaskSequence Add(BaseTask baseTask)
        {
            tasks.Enqueue(baseTask);
            return this;
        }
        
        public TaskSequence Run(Action onCompleted = null)
        {
            this.onCompleted = onCompleted;
            initialTasksCount = tasks.Count;
            Game.Coroutiner.Start(ExecutionRoutine());
            return this;
        }

        private IEnumerator ExecutionRoutine()
        {
            while (tasks.Count > 0)
            {
                BaseTask baseTask = tasks.Dequeue();
                baseTask.Execute();
                
                yield return new WaitWhile(() => baseTask.State == TaskState.RUNNING);
                OnProgressChanged.Fire(Progress);
            }

            onCompleted?.Invoke();

            yield return null;
        }
    }
}