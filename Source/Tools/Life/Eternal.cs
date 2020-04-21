using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Core
{
    public class Eternal : Life
    {
        private readonly Queue<Action> onRestartActions = new Queue<Action>();
        
        public Eternal() : base("ETERNAL")
        {
            
        }
        
        public void Restart()
        {
            Terminate();
            isTerminated = false;
        
            // call and remove only old restart actions
            int count = onRestartActions.Count;
            for (int i = 0; i < count; i++)
            {
                onRestartActions.Dequeue().Invoke();
            }
        }
        
        internal void Add(Action onRestart, [CanBeNull] Action onTerminate)
        {
            if (isTerminated) return;
        
            onRestartActions.Enqueue(onRestart);
            Add(onTerminate);
        }
    }
}