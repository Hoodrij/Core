namespace Core.Abstract
{
	public class Service
	{
		public static T Get<T>() where T : IModel => Game.Models.Get<T>();
	}
}