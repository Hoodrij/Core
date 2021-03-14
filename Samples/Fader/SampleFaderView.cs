using System.Threading.Tasks;
using Core.Fader;
using Core.Tools.ExtensionMethods;
using Core.Ui;
using UnityAsync;
using UnityEngine;

namespace Core.Samples.Fader
{
    public class SampleFaderView : UIView, IFaderView
    {
        private enum EState
        {
            Idle,
            Shown,
            Hiding,
        }

        private Animator animator;
        private EState state;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public async Task Show()
        {
            animator.SetSingleTrigger("show");
            await this.WaitUntil(() => state == EState.Shown);
        }

        public async Task Hide()
        {
            if (state == EState.Shown)
            {
                state = EState.Hiding;
                animator.SetSingleTrigger("hide");
            }
            await this.WaitUntil(() => state == EState.Idle);
        }

        // animation event
        private void OnAnimShown()
        {
            state = EState.Shown;
        }

        // animation event
        private void OnAnimHidden()
        {
            state = EState.Idle;
        }
    }
}