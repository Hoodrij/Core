using Core.Tools.ExtensionMethods;
using Core.Tools.Observables;
using UnityEngine;

namespace Core
{
    public class Events : Unit
    {
        public readonly Signal OnUpdate = new Signal();
        public readonly Signal OnLateUpdate = new Signal();
        public readonly Signal OnFixedUpdate = new Signal();
        public readonly Signal OnPause = new Signal();
        public readonly Signal OnResume = new Signal();
        public readonly Signal OnQuit = new Signal();
            
        CoreEventsBehaviour behaviour;

        protected override void OnStart()
        {
            if (!Application.isPlaying) return;

            behaviour = new GameObject("Core.Events").AddComponent<CoreEventsBehaviour>();
            behaviour.gameObject.hideFlags |= HideFlags.HideAndDontSave;
            Object.DontDestroyOnLoad(behaviour);
            behaviour.Events = this;
            
            OnUpdate.Clear();
            OnLateUpdate.Clear();
            OnFixedUpdate.Clear();
            OnPause.Clear();
            OnResume.Clear();
            OnQuit.Clear();
            
            Life.Add(() =>
            {
                if (behaviour != null
                    && behaviour.isActiveAndEnabled
                    && behaviour.gameObject != null)
                {
                    behaviour?.gameObject.Destroy();
                }
            });
        }
    }

    public class CoreEventsBehaviour : MonoBehaviour
    {
        public Events Events { get; set; }

        private void Update()
        {
            Events?.OnUpdate.Fire();
        }

        private void LateUpdate()
        {
            Events?.OnLateUpdate.Fire();
        }

        private void FixedUpdate()
        {
            Events?.OnFixedUpdate.Fire();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus)
                Events?.OnResume.Fire();
            else
                Events?.OnPause.Fire();
        }

        private void OnApplicationPause(bool pause)
        {
            if (!IS.EDITOR) return;

            if (pause)
                Events?.OnPause.Fire();
            else
                Events?.OnResume.Fire();
        }

        private void OnApplicationQuit()
        {
            Events?.OnQuit.Fire();
        }
    }
}