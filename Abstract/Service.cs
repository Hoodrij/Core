namespace Core.Abstract
{
	public abstract class Service
	{
		protected internal virtual void Start() { }
	}
	
	public abstract class RuntimeService : Service
	{
		protected internal abstract void Update();
	}
}