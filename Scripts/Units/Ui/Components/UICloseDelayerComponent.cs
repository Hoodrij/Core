using System.Threading.Tasks;
using Core.Tools.ExtensionMethods;
using UnityEngine;

namespace Core.Ui.Components
{
    public class UICloseDelayerComponent : MonoBehaviour
    {
        private bool isClosed;

        internal async Task WaitClose()
        {
            Animator animator = GetComponent<Animator>();
            if (animator == null)
            {
                return;
            }

            animator.SetSingleTrigger("Close");

            await new WaitUntil(() => isClosed);
        }

        // Animaton event
        private void OnAnimationCompleted()
        {
            isClosed = true;
        }
    }
}