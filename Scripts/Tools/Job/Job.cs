using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Tools
{
    public abstract class Job
    {
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
        }
        
        protected abstract Task Run();

        public static Job As(Func<Task> action) => new AnonymousJob(action);
    }
}