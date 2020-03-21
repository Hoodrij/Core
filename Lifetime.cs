using Core.Tools.Observables;
using UnityEngine;

namespace Core
{
	public class Lifetime
    {
        public readonly Signal OnUpdate = new Signal();
        public readonly Signal OnLateUpdate = new Signal();
        public readonly Signal OnFixedUpdate = new Signal();
        public readonly Signal OnPause = new Signal();
        public readonly Signal OnResume = new Signal();
        public readonly Signal OnQuit = new Signal();
        
        internal Lifetime()
        {
	        if (!Application.isPlaying) return;
	        
	        LifetimeBehaviour beh = new GameObject("Game.Lifetime").AddComponent<LifetimeBehaviour>();
            Object.DontDestroyOnLoad(beh);
            beh.Lifetime = this;
        }

        internal void FireUpdate() => OnUpdate.Fire();
        internal void FireLateUpdate() => OnLateUpdate.Fire();
        internal void FireFixedUpdate() => OnFixedUpdate.Fire();
        internal void FirePause() => OnPause.Fire();
        internal void FireResume() => OnResume.Fire();
        internal void FireQuit() => OnQuit.Fire();
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
        
        private void OnApplicationPause(bool pause)
        {
            if (!Macros.EDITOR) return;
            
            if (pause)
            {
                Lifetime?.FirePause();
            }
            else
            {
                Lifetime?.FireResume();
            }
        }

        private void OnApplicationQuit()
        {
            Lifetime?.FireQuit();
        }
    }
}