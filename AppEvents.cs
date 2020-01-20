using Core.Utils.Observables;
using UnityEngine;
using Event = Core.Utils.Observables.Event;

namespace Core
{
	public class AppEvents
	{
		public Event OnUpdate = new Event();
		public Event OnFixedUpdate = new Event();
		public Event OnLateUpdate = new Event();
		public Event<bool> OnPause = new Event<bool>();
		public Event<bool> OnFocus = new Event<bool>();
		public Event OnQuit = new Event();

		public AppEvents()
		{
			if (!Application.isPlaying) return;
			
			GameObject gameObject = new GameObject("[AppEvents]");
			Object.DontDestroyOnLoad(gameObject);
			AppEventsBehaviour behaviour = gameObject.AddComponent<AppEventsBehaviour>();
			behaviour.appEvents = this;
		}
	}

	public class AppEventsBehaviour : MonoBehaviour
	{
		internal AppEvents appEvents { private get; set; }

		void Update()
		{
			appEvents.OnUpdate.Fire();
		}

		private void FixedUpdate()
		{
			appEvents.OnFixedUpdate.Fire();
		}

		private void LateUpdate()
		{
			appEvents.OnLateUpdate.Fire();
		}

		private void OnApplicationFocus(bool focus)
		{
			appEvents.OnFocus.Fire(focus);
		}

		private void OnApplicationPause(bool pause)
		{
			appEvents.OnPause.Fire(pause);
		}

		private void OnApplicationQuit()
		{
			appEvents.OnQuit.Fire();
		}
	}
}