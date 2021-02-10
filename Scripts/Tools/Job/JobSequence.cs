using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Tools.Observables;
using UnityAsync;

namespace Core.Tools
{
    public class JobSequence
    {
        public enum Mode
        {
            MultipleShot = 1,
            OneByFrame = 2,
        }

        public float Progress => 1 - (float) (queue.Count - 1) / initialTasksCount;

        private readonly Signal<float> onProgressChanged = new Signal<float>();
        private readonly Queue<Job> queue = new Queue<Job>();
        private int initialTasksCount;
        private Mode mode = Mode.MultipleShot;
        private CancellationTokenSource tokenSource;

        public JobSequence Add(Job job)
        {
            queue.Enqueue(job);
            return this;
        }

        public JobSequence SetMode(Mode mode)
        {
            this.mode = mode;
            return this;
        }

        public JobSequence OnProgress(Action<float> action)
        {
            onProgressChanged.Listen(action);
            return this;
        }

        public async Task<JobSequence> Run()
        {
            initialTasksCount = queue.Count;
            tokenSource = new CancellationTokenSource();
            await ExecutionRoutine();

            return this;
        }
        
        public void Cancel()
        {
            tokenSource.Cancel();
            queue.Clear();
        }

        private async Task ExecutionRoutine()
        {
            while (queue.Count > 0 && !tokenSource.IsCancellationRequested)
            {
                await queue.Dequeue().Run(tokenSource);
                onProgressChanged.Fire(Progress);

                if (mode == Mode.OneByFrame)
                    await Wait.Update;
            }
        }
    }
}