using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Core.Utils.Observables
{
	public abstract class ASignal
	{
		private readonly List<EventHandlerData> eventHandlers = new List<EventHandlerData>();

		protected void FireInternal(params object[] args)
		{
			for (var i = 0; i < eventHandlers.Count; i++)
			{
				EventHandlerData eventHandlerData = eventHandlers[i];
				MethodInfo method = eventHandlerData.method;
				object handle = eventHandlerData.handle;

				if (handle == null || handle.Equals(null))
				{
					eventHandlers.Remove(eventHandlers[i]);
					i--;
					continue;
				}

				if (handle is MonoBehaviour behaviour && !behaviour.gameObject.activeInHierarchy)
				{
					continue;
				}

				method.Invoke(eventHandlerData.handle, args);
			}
		}

		protected void ListenInternal(MethodInfo method, object handler)
		{
			if (eventHandlers.Exists(e => e.method == method && e.handle == handler)) return;

			eventHandlers.Add(new EventHandlerData(handler, method));
		}

		protected void UnsubscribeInternal(MethodInfo method, object handler)
		{
			EventHandlerData find = eventHandlers.Find(data => data.handle == handler);
			if (find == null) return;
			find.handle = null;
		}



		private class EventHandlerData
		{
			public object handle;
			public MethodInfo method;

			public EventHandlerData(object handle, MethodInfo method)
			{
				this.handle = handle;
				this.method = method;
			}
		}
	}
}
