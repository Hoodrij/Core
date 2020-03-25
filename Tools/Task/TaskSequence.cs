using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Tools.Observables;
using UnityEngine;

namespace Core.Tools
{
	public class TaskSequence
    {
        public float Progress => (float) tasks.Count / initialTasksCount;
        public readonly Signal<float> OnProgressChanged = new Signal<float>();
        
        private readonly Queue<Task> tasks = new Queue<Task>();
        private Action onCompleted;
        private int initialTasksCount;

        public TaskSequence Add(Task task)
        {
            tasks.Enqueue(task);
            return this;
        }
        
        public async Task<TaskSequence> Run(Action onCompleted = null)
        {
            this.onCompleted = onCompleted;
            initialTasksCount = tasks.Count;
            await ExecutionRoutine();
            
            return this;
        }

        private async System.Threading.Tasks.Task ExecutionRoutine()
        {
            while (tasks.Count > 0)
            {
                Task task = tasks.Dequeue();
                task.Execute();
                
                await new WaitWhile(() => task.State == TaskState.RUNNING);
                OnProgressChanged.Fire(Progress);
            }

            onCompleted?.Invoke();
        }
    }
}