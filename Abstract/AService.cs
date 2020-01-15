namespace Core.Abstract
{
	public class AService
	{
		public static T Get<T>() where T : IModel => Game.Models.Get<T>();
	}
}