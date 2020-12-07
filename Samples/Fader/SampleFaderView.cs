using System.Threading.Tasks;
using Core.Fader;
using Core.Tools.ExtensionMethods;
using UnityAsync;
using UnityEngine;

namespace Core.Samples.Fader
{
    public class SampleFaderView : MonoBehaviour, IFaderView
    {
        private enum State
        {
            Idle,
            Shown,
            Hiding,
        }

        private Animator animator;
        private State state;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public async Task Show()
        {
            animator.SetSingleTrigger("show");
            await this.WaitUntil(() => state == State.Shown);
        }

        public async Task Hide()
        {
            if (state == State.Shown)
            {
                state = State.Hiding;
                animator.SetSingleTrigger("hide");
            }
            await this.WaitUntil(() => state == State.Idle);
        }

        // animation event
        private void OnAnimShown()
        {
            state = State.Shown;
        }

        // animation event
        private void OnAnimHidden()
        {
            state = State.Idle;
        }
    }
}