namespace Core.Abstract
{
	public abstract class Service
	{
		protected Service()
		{
			Game.Injector.Inject(this);
		}
	}
	
	public abstract class RuntimeService : Service
	{
		protected internal abstract void Update();
	}
}