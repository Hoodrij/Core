using System;

namespace Core.Abstract
{
	public abstract class AEvent
	{
		
	}
	
	public class Event : AEvent
	{
		public void Listen(Action callback)
		{
			
		}
		
		public void Fire()
		{
			
		}
	}
	
	public class Event<T> : AEvent
	{
		
	}
}