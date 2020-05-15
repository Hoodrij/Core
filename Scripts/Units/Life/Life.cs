using Core.Life;
using Core.Tools.Observables;
using UnityEngine;

namespace Core.Units
{
    public class Life : Unit
    {
        public readonly Signal OnUpdate = new Signal();
        public readonly Signal OnLateUpdate = new Signal();
        public readonly Signal OnFixedUpdate = new Signal();
        public readonly Signal OnPause = new Signal();
        public readonly Signal OnResume = new Signal();
        public readonly Signal OnQuit = new Signal();

        public Life()
        {
            if (!Application.isPlaying) return;

            LifeBehaviour behaviour = new GameObject("Core.Life").AddComponent<LifeBehaviour>();
            // behaviour.gameObject.hideFlags |= HideFlags.HideAndDontSave;
            Object.DontDestroyOnLoad(behaviour);
            behaviour.Life = this;
        }
    }
}