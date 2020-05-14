using UnityEngine;

namespace Core.Life
{
    public class LifeBehaviour : MonoBehaviour
    {
        public Units.Life Life { get; set; }

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