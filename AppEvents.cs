using Core.Utils.Observables;
using UnityEngine;
using Event = Core.Utils.Observables.Event;

using System;
using UnityEngine;

    public class Lifetime
    {
        public event Action OnUpdate;
        public event Action OnLateUpdate;
        public event Action OnFixedUpdate;
        public event Action OnPause;
        public event Action OnResume;
        public event Action OnQuit;
        
        internal Lifetime()
        {
            if (Application.isPlaying)
            {
                LifetimeBehaviour beh = new GameObject("AppEventsBehaviour").AddComponent<LifetimeBehaviour>();
                beh.Lifetime = this;
            }
        }

        internal void FireUpdate() => OnUpdate?.Invoke();
        internal void FireLateUpdate() => OnLateUpdate?.Invoke();
        internal void FireFixedUpdate() => OnFixedUpdate?.Invoke();
        internal void FirePause() => OnPause?.Invoke();
        internal void FireResume() => OnResume?.Invoke();
        internal void FireQuit() => OnQuit?.Invoke();
    }

    public class LifetimeBehaviour : MonoBehaviour
    {
        public Lifetime Lifetime { get; set; }
        
        private void Update()
        {
            Lifetime?.FireUpdate();
        }

        private void LateUpdate()
        {
            Lifetime?.FireLateUpdate();
        }

        private void FixedUpdate()
        {
            Lifetime?.FireFixedUpdate();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                Lifetime?.FireResume();
            }
            else
            {
                Lifetime?.FirePause();
            }
        }
        
#if UNITY_EDITOR
        private void OnApplicationPause (bool pause)
        {
            if (pause)
            {
                Lifetime?.FirePause();
            }
            else
            {
                Lifetime?.FireResume();
            }
        }
#endif

        private void OnApplicationQuit()
        {
            Lifetime?.FireQuit();
        }
    }

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

	internal class AppEventsBehaviour : MonoBehaviour
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