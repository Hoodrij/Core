using UnityEngine;

namespace Core.Life
{
    public class LifeBehaviour : MonoBehaviour
    {
        public Units.Life Life { get; set; }

        private void Update()
        {
            Life?.UpdateEvent.Fire();
        }

        private void LateUpdate()
        {
            Life?.LateUpdateEvent.Fire();
        }

        private void FixedUpdate()
        {
            Life?.FixedUpdateEvent.Fire();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus)
                Life?.ResumeEvent.Fire();
            else
                Life?.PauseEvent.Fire();
        }

        private void OnApplicationPause(bool pause)
        {
            if (!IS.EDITOR) return;

            if (pause)
                Life?.PauseEvent.Fire();
            else
                Life?.ResumeEvent.Fire();
        }

        private void OnApplicationQuit()
        {
            Life?.QuitEvent.Fire();
        }
    }
}