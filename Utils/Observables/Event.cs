﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Core.Utils.Observables
{
	public class Event
	{
		protected Dictionary<object, Action> Listeners { get; } = new Dictionary<object, Action>();

		public void Fire()
		{
			List<object> toRemove = new List<object>();
			
			foreach (object target in Listeners.Keys)
			{
				if (target == null || target.Equals(null))
				{
					toRemove.Add(target);
					continue;
				}

				if (target is MonoBehaviour monoBeh && !monoBeh.gameObject.activeInHierarchy)
				{
					continue;
				}
				
				Listeners[target].Invoke();
			}
			
			toRemove.ForEach(o => Listeners.Remove(o));
		}

		public void Listen(Action action)
		{
			Listeners.Add(action.Target, action);
		}

		public void Unsubscribe(Action action)
		{
			Listeners.Remove(action.Target);
		}
	}
	
	public class Event<T>
	{
		protected Dictionary<object, Action<T>> Listeners { get; } = new Dictionary<object, Action<T>>();

		public void Fire(T arg)
		{
			List<object> toRemove = new List<object>();
			
			foreach (object target in Listeners.Keys)
			{
				if (target == null || target.Equals(null))
				{
					toRemove.Add(target);
					continue;
				}

				if (target is MonoBehaviour monoBeh && !monoBeh.gameObject.activeInHierarchy)
				{
					continue;
				}
				
				Listeners[target].Invoke(arg);
			}
			
			toRemove.ForEach(o => Listeners.Remove(o));
		}

		public void Listen(Action<T> action)
		{
			Listeners.Add(action.Target, action);
		}

		public void Unsubscribe(Action<T> action)
		{
			Listeners.Remove(action.Target);
		}
	}
}
