using System;

namespace Core.Tools.Task
{
	public abstract class BaseTask
    {
        private Action<BaseTask> onSuccess;
        private Action<BaseTask, string> onError;

        public TaskState State = TaskState.IDLE;

        public BaseTask()
        {
            Game.Models.Populate(this);
        }

        public void Execute(Action<BaseTask> onSuccess = null, Action<BaseTask, string> onError = null)
        {
            this.onSuccess = onSuccess;
            this.onError = onError;

            State = TaskState.RUNNING;

            RunExecution();
        }

        protected abstract void RunExecution();

        protected void ReportSuccess()
        {
            State = TaskState.SUCCESS;

            onSuccess?.Invoke(this);
        }

        protected void ReportError(string errorMessage)
        {
            State = TaskState.ERROR;

            onError?.Invoke(this, errorMessage);
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