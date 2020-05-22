using System;
using System.Threading.Tasks;

namespace Core.Scripts.Tools.Job 
{
    internal class AnonymousJob : Core.Tools.Job
    {
        private Func<Task> action;

        internal AnonymousJob(Func<Task> action)
        {
            this.action = action;
        }
    
        protected override async Task Run()
        {
            await action();
        }
    }
}