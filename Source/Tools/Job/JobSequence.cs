using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Tools.ExtensionMethods;
using Core.Tools.Observables;
using UnityEngine;

namespace Core.Tools
{
    public class JobSequence
    {
        public enum Mode
        {
            MultipleShot = 1,
            OneByFrame = 2,
        }

        public float Progress => 1 - (float) queue.Count / initialTasksCount;

        private readonly Signal<float> onProgressChanged = new Signal<float>();
        private readonly Queue<Job> queue = new Queue<Job>();
        private int initialTasksCount;
        private Mode mode = Mode.MultipleShot;

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
            await ExecutionRoutine();

            return this;
        }

        private async Task ExecutionRoutine()
        {
            while (queue.Count > 0)
            {
                await queue.Dequeue().Run();
                onProgressChanged.Fire(Progress);

                if (mode == Mode.OneByFrame)
                    await new WaitForUpdate();
            }
        }
    }
}