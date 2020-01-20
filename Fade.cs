using System;

namespace Core
{
	public class Fade
	{
		public void AddAction(Action action)
		{
			action.Invoke();
		}
	}
}