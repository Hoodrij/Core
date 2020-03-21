namespace Core.Tools.Task
{
	public abstract class Task : BaseTask
    {
        protected override void RunExecution()
        {
            Execute();

            if (State == TaskState.RUNNING)
                ReportSuccess();
        }
        
        protected abstract void Execute();
    }
}