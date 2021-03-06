using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Tools.Observables;

namespace Core.Tools
{
    public abstract class Job
    {
        public readonly Event CompletedEvent = new Event(); 
            
        private CancellationTokenSource tokenSource;
        internal CancellationToken Token => tokenSource.Token;
        
        protected Job()
        {
            Injector.Instance.Populate(this);
        }

        public async Task Run(CancellationTokenSource tokenSource = null)
        {
            this.tokenSource = tokenSource ?? new CancellationTokenSource();
            await Run();
            CompletedEvent.Fire();
        }
        
        protected abstract Task Run();

        public static Job As(Func<Task> action) => new AnonymousJob(action);
    }
}