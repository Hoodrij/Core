using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class Life
    {
        public static readonly Eternal Eternal = new Eternal();

        protected bool isTerminated;
        private string name;
        
        // private readonly Queue<Action> onRestartActions = new Queue<Action>();
        private readonly List<Action> onTerminateActions = new List<Action>();

        internal Life(string name)
        {
            this.name = name;
        }

        public Life Derive(string name = "")
        {
            Life derive = new Life($"{this.name}/{name}");
            this.Add(derive.Terminate);
            return derive;
        }
        
        public void Add(Action onTerminate)
        {
            if (isTerminated) return;
            if (onTerminate == null) return;
            
            onTerminateActions.Add(onTerminate);
        }

        public void Terminate()
        {
            if (isTerminated) return;
            
            isTerminated = true;
            
            foreach (Action terminateAction in Enumerable.Reverse(onTerminateActions))
            {
                terminateAction();
            }
            onTerminateActions.Clear();
        }
    }
}