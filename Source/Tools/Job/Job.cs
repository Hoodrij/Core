using System;
using System.Threading.Tasks;

namespace Core.Tools
{
    public abstract class Job
    {
        public TaskState State { get; private set; } = TaskState.IDLE;

        private Action<string> onError;

        protected Job()
        {
            Injector.Instance.Populate(this);
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