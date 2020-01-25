using System;
using Core.Utils.ExtensionMethods;
using UnityEngine;

namespace Core.Solutions.Fader
{
	public class FaderView : MonoBehaviour, IFaderView
	{
		public bool IsShown { get; private set; }
		
		private Animator animator;

		private void Awake()
		{
			animator = GetComponent<Animator>();
			Game.Fader.SetView(this);
		}

		public void ShowView()
		{
			animator.SetSingleTrigger("show");
		}

		public void HideView()
		{
			animator.SetSingleTrigger("hide");
			IsShown = false;
		}
		
		// animation event
		private void OnAnimShown()
		{
			IsShown = true;
		}
	}
}