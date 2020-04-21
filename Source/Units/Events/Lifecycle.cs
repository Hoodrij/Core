using Core.Tools.Observables;
using UnityEngine;

namespace Core
{
    public class Lifecycle : Unit
    {
        public readonly Signal OnUpdate = new Signal();
        public readonly Signal OnLateUpdate = new Signal();
        public readonly Signal OnFixedUpdate = new Signal();
        public readonly Signal OnPause = new Signal();
        public readonly Signal OnResume = new Signal();
        public readonly Signal OnQuit = new Signal();

        public Lifecycle ()
        {
            if (!Application.isPlaying) return;

            LifecycleBehaviour behaviour = new GameObject("Core.Lifecycle").AddComponent<LifecycleBehaviour>();
            behaviour.gameObject.hideFlags |= HideFlags.HideAndDontSave;
            Object.DontDestroyOnLoad(behaviour);
            behaviour.Lifecycle = this;
        }
    }

    public class LifecycleBehaviour : MonoBehaviour
    {
        public Lifecycle Lifecycle { get; set; }

        private void Update()
        {
            Lifecycle?.OnUpdate.Fire();
        }

        private void LateUpdate()
        {
            Lifecycle?.OnLateUpdate.Fire();
        }

        private void FixedUpdate()
        {
            Lifecycle?.OnFixedUpdate.Fire();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus)
                Lifecycle?.OnResume.Fire();
            else
                Lifecycle?.OnPause.Fire();
        }

        private void OnApplicationPause(bool pause)
        {
            if (!IS.EDITOR) return;

            if (pause)
                Lifecycle?.OnPause.Fire();
            else
                Lifecycle?.OnResume.Fire();
        }

        private void OnApplicationQuit()
        {
            Lifecycle?.OnQuit.Fire();
        }
    }
}