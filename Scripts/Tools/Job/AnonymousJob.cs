using System;
using System.Threading.Tasks;

namespace Core.Tools
{
    internal class AnonymousJob : Job
    {
        private readonly Func<Task> action;

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