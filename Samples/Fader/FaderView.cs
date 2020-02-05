using System;
using System.Collections;
using System.Collections.Generic;
using Core.Utils.ExtensionMethods;
using UnityEngine;

namespace Core.Samples.Fader
{
	public class FaderView : MonoBehaviour, IFaderView
	{
		private Animator animator;
		private bool isShown;

		private void Awake()
		{
			animator = GetComponent<Animator>();
		}

		public IEnumerator WaitForShown()
		{
			animator.SetSingleTrigger("show");
			yield return new WaitWhile(() => !isShown);  
		}

		public void Hide()
		{
			isShown = false;
			animator.SetSingleTrigger("hide");
		}
		
		// animation event
		private void OnAnimShown()
		{
			isShown = true;
		}
	}
}