using System.Threading.Tasks;
using Core.Tools.ExtensionMethods;
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

		public async Task WaitForShown()
		{
			animator.SetSingleTrigger("show");
			await new WaitUntil(() => isShown);  
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