using System;

namespace Core.Tools
{
	public abstract class Task
    {
        private Action<Task> onSuccess;
        private Action<Task, string> onError;

        public TaskState State = TaskState.IDLE;

        public Task()
        {
            Game.Models.Populate(this);
        }

        public void Execute(Action<Task> onSuccess = null, Action<Task, string> onError = null)
        {
            this.onSuccess = onSuccess;
            this.onError = onError;

            State = TaskState.RUNNING;

            Run();
            ReportSuccess();
        }

        protected abstract void Run();

        protected void ReportSuccess()
        {
            if (State != TaskState.RUNNING) return;
            
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