using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Profiling;

namespace Core.Tools
{
    public class Measure
    {
        private readonly MeasureResult result;

        private Measure(MeasureResult result)
        {
            this.result = result;
        }

        public static Measure Method(Action action)
        {
            Measure measure = new Measure(new MeasureResult()
                {
                    Action = action,
                    Name = action.Target.ToString(),
                    Count = 1
                }
            );
            return measure;
        }

        public Measure Count(int count)
        {
            result.Count = count;
            return this;
        }

        public Measure Name(string name)
        {
            result.Name = name;
            return this;
        }

        public MeasureResult Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            GC.Collect();
            float memoryAllocated = Profiler.GetTotalAllocatedMemoryLong() + GC.GetTotalMemory(false);

            stopwatch.Start();

            for (int i = 0; i < result.Count; i++)
                result.Action();

            stopwatch.Stop();
            memoryAllocated = Profiler.GetTotalAllocatedMemoryLong() + GC.GetTotalMemory(false) - memoryAllocated;
            memoryAllocated /= result.Count;

            result.Ticks = stopwatch.Elapsed.Duration().TotalMilliseconds / result.Count;
            ;
            result.MemoryAlloc = Mathf.Clamp(memoryAllocated, 0, Single.MaxValue);

            ($"{"[MEASURE]".Bold()} {result.Name}. " +
             $"\n [Duration: {result.Ticks} ms]" +
             $"   [Memory : {result.MemoryAlloc}]").log();

            return result;
        }
    }
}