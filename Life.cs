using Core.Tools.Observables;
using UnityEngine;

namespace Core
{
    public class Life
    {
        public readonly Signal OnUpdate = new Signal();
        public readonly Signal OnLateUpdate = new Signal();
        public readonly Signal OnFixedUpdate = new Signal();
        public readonly Signal OnPause = new Signal();
        public readonly Signal OnResume = new Signal();
        public readonly Signal OnQuit = new Signal();

        internal Life()
        {
            if (!Application.isPlaying) return;

            var behaviour = new GameObject("Game.Life").AddComponent<LifeBehaviour>();
            behaviour.gameObject.hideFlags |= HideFlags.HideInHierarchy;
            Object.DontDestroyOnLoad(behaviour);
            behaviour.Life = this;
        }
    }

    public class LifeBehaviour : MonoBehaviour
    {
        public Life Life { get; set; }

        private void Update()
        {
            Life?.OnUpdate.Fire();
        }

        private void LateUpdate()
        {
            Life?.OnLateUpdate.Fire();
        }

        private void FixedUpdate()
        {
            Life?.OnFixedUpdate.Fire();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus)
                Life?.OnResume.Fire();
            else
                Life?.OnPause.Fire();
        }

        private void OnApplicationPause(bool pause)
        {
            if (!Macros.EDITOR) return;

            if (pause)
                Life?.OnPause.Fire();
            else
                Life?.OnResume.Fire();
        }

        private void OnApplicationQuit()
        {
            Life?.OnQuit.Fire();
        }
    }
}