using Core.Utils.Observables;
using UnityEngine;

namespace Core
{
	public class AppEvents
	{
		public Signal OnUpdate = new Signal();
		public Signal OnFixedUpdate = new Signal();
		public Signal OnLateUpdate = new Signal();
		public Signal<bool> OnPause = new Signal<bool>();
		public Signal<bool> OnFocus = new Signal<bool>();
		public Signal OnQuit = new Signal();

		public AppEvents()
		{
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