namespace Core.Abstract
{
	public abstract class Service
	{
		protected static T Get<T>() where T : IModel => Game.Models.Get<T>();
	}
	
	public abstract class RuntimeService : Service
	{
		protected internal abstract void Update();
	}
}