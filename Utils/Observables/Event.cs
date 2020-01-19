﻿using System;

namespace Core.Utils.Observables 
{
	public class Event : AEvent
	{
		public void Listen(Action action) { ListenInternal(action.Method, action.Target); }
		public void Unsubscribe(Action action) { UnsubscribeInternal(action.Method, action.Target); }
		public void Fire() { FireInternal(); }
	}

	public class Event<T1> : AEvent
	{
		public void Listen(Action<T1> action) { ListenInternal(action.Method, action.Target); }
		public void Unsubscribe(Action<T1> action) { UnsubscribeInternal(action.Method, action.Target); }
		public void Fire(T1 t) { FireInternal(t); }
	}

	public class Event<T1, T2> : AEvent
	{
		public void Listen(Action<T1, T2> action) { ListenInternal(action.Method, action.Target); }
		public void Unsubscribe(Action<T1, T2> action) { UnsubscribeInternal(action.Method, action.Target); }
		public void Fire(T1 t1, T2 t2) { FireInternal(t1, t2); }
	}
}