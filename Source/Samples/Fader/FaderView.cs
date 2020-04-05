using System;
using System.Threading.Tasks;
using Core.Tools.ExtensionMethods;
using UnityEngine;

namespace Core.Samples.Fader
{
    public class FaderView : MonoBehaviour, IFaderView
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

        public async Task WaitForShown()
        {
            animator.SetSingleTrigger("show");
            await new WaitUntil(() => state == State.Shown);
        }

        public async Task Hide()
        {
            if (state != State.Shown) return;
            
            state = State.Hiding;
            animator.SetSingleTrigger("hide");
            await new WaitUntil(() => state == State.Idle);
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