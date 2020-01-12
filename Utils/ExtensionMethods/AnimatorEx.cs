using UnityEngine;

namespace Core.Utils.ExtensionMethods
{
	public static class AnimatorEx
	{
		public static void SetSingleTrigger(this Animator animator, string triggerName)
		{
			foreach (AnimatorControllerParameter p in animator.parameters)
			{
				if (p.type == AnimatorControllerParameterType.Trigger)
				{
					animator.ResetTrigger(p.name);
				}
			}

			animator.SetTrigger(triggerName);
		}
	}
}
