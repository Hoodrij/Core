using System;
using Core.Utils.ExtensionMethods;
using UnityEngine;

namespace Core.Ui
{
	public class UICloseDelayer : MonoBehaviour
	{
		private Animator animator;
		private Action onCloseCallback;

		internal void BeginClose(Action onCloseCallback)
		{
			if (animator == null)
			{
				animator = GetComponent<Animator>();
				if (animator == null)
				{
					onCloseCallback.Invoke();
					return;
				}
			}

			this.onCloseCallback = onCloseCallback;

			animator.SetSingleTrigger("Close");
		}

		// Animaton event
		private void OnAnimationCompleted()
		{
			onCloseCallback.Invoke();
		}
	}
}
