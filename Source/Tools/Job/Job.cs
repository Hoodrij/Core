using System;
using System.Threading.Tasks;

namespace Core.Tools
{
    public abstract class Job
    {
        public JobState State { get; private set; } = JobState.IDLE;

        private Action<string> onError;

        protected Job()
        {
            Game.Models.Populate(this);
        }

        public async Task Run(Action<string> onError = null)
        {
            this.onError = onError;
            State = JobState.RUNNING;

            await Run();
            State = JobState.SUCCESS;
        }

        protected abstract Task Run();

        protected void ReportError(string errorMessage)
        {
            State = JobState.ERROR;

            onError?.Invoke(errorMessage);
        }
    }
    
    public enum JobState
    {
        IDLE,
        RUNNING,
        SUCCESS,
        ERROR
    }
}