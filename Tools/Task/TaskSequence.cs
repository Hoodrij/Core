using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Tools.Observables;
using UnityEngine;

namespace Core.Tools
{
	public class TaskSequence
    {
        public float Progress => 1 - (float) tasks.Count / initialTasksCount;
        
        private readonly Signal<float> onProgressChanged = new Signal<float>();
        private readonly Queue<Task> tasks = new Queue<Task>();
        private int initialTasksCount;

        public TaskSequence Add(Task task)
        {
            tasks.Enqueue(task);
            return this;
        }

        public TaskSequence OnProgress(Action<float> action)
        {
            onProgressChanged.Listen(action);
            return this;
        }
        
        public async Task<TaskSequence> Run()
        {
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
                onProgressChanged.Fire(Progress);
            }
        }
    }
}