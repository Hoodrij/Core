using Core.Tools.Observables;
using UnityEngine;

namespace Core
{
    public class Life : Unit
    {
        public readonly Signal OnUpdate = new Signal();
        public readonly Signal OnLateUpdate = new Signal();
        public readonly Signal OnFixedUpdate = new Signal();
        public readonly Signal OnPause = new Signal();
        public readonly Signal OnResume = new Signal();
        public readonly Signal OnQuit = new Signal();

        public Life ()
        {
            if (!Application.isPlaying) return;

            LifeBehaviour behaviour = new GameObject("Core.Life").AddComponent<LifeBehaviour>();
            behaviour.gameObject.hideFlags |= HideFlags.HideAndDontSave;
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
            if (!IS.EDITOR) return;

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