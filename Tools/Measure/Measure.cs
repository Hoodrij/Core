using System;
using System.Diagnostics;
using Core.Tools.ExtensionMethods;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Core.Tools
{
    public class Measure
    {
        private Action action;
        private int count;
        private string name;
        
        public static Measure Method(Action action)
        {
            Measure measure = new Measure
            {
                action = action,
                name = action.Target.ToString(),
                count = 1
            };
            return measure;
        }

        public Measure Count(int count)
        {
            this.count = count;
            return this;
        }

        public Measure Name(string name)
        {
            this.name = name;
            return this;
        }

        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < count; i++)
            {
                action();
            }
            stopwatch.Stop();
            float elapsedTicks = stopwatch.Elapsed.Ticks / (float) count;

            Debug.Log($"{ name.Color(Color.yellow) } with { elapsedTicks.ToString().Color(Color.yellow) }");
#if UNITY_EDITOR
#endif
        }
    }
}