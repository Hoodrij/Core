using System;
using System.Threading.Tasks;

namespace Core.Tools
{
    public abstract class Job
    {
        private Action<string> onError;

        public TaskState State = TaskState.IDLE;

        public Job()
        {
            Game.Models.Populate(this);
        }

        public async Task Run(Action<string> onError = null)
        {
            this.onError = onError;
            State = TaskState.RUNNING;

            await Run();
            State = TaskState.SUCCESS;
        }

        protected abstract Task Run();

        protected void ReportError(string errorMessage)
        {
            State = TaskState.ERROR;

            onError?.Invoke(errorMessage);
        }
    }

    public enum TaskState
    {
        IDLE,
        RUNNING,
        SUCCESS,
        ERROR
    }
}