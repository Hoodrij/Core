using System.Collections;

namespace Core.Tools.Task
{
	public abstract class AsyncTask : BaseTask
    {
        protected override void RunExecution()
        {
            Game.Coroutiner.Start(ExecutionRoutine());
        }

        IEnumerator ExecutionRoutine()
        {
            yield return Execute();

            if(State == TaskState.RUNNING)
                ReportSuccess();
        }

        protected abstract IEnumerator Execute();
    }
}