using System.Threading.Tasks;
using Core.Tools.ExtensionMethods;
using UnityAsync;
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

            await Wait.Until(() => isClosed);
        }

        // Animaton event
        private void OnAnimationCompleted()
        {
            isClosed = true;
        }
    }
}