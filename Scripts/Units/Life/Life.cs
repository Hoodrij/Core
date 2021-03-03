using Core.Life;
using Core.Tools;
using UnityEngine;
using Event = Core.Tools.Observables.Event;

namespace Core.Units
{
    public class Life : Unit, Lazy
    {
        public readonly Event UpdateEvent = new Event();
        public readonly Event LateUpdateEvent = new Event();
        public readonly Event FixedUpdateEvent = new Event();
        public readonly Event PauseEvent = new Event();
        public readonly Event ResumeEvent = new Event();
        public readonly Event QuitEvent = new Event();

        public Life()
        {
            if (!Application.isPlaying) return;

            LifeBehaviour behaviour = new GameObject("Core.Life").AddComponent<LifeBehaviour>();
            behaviour.gameObject.hideFlags |= HideFlags.HideAndDontSave;
            Object.DontDestroyOnLoad(behaviour);
            behaviour.Life = this;
        }
    }
}