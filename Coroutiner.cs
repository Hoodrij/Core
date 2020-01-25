using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
	public class Coroutiner
	{
		CoroutinerBehaviour behaviour;
		
		public Coroutiner()
		{
			if (!Application.isPlaying) return;
			
			GameObject gameObject = new GameObject("[Coroutiner]");
			Object.DontDestroyOnLoad(gameObject);
			behaviour = gameObject.AddComponent<CoroutinerBehaviour>();
		}
		
		public Coroutine StartCoroutine(IEnumerator coroutine)
		{
			return behaviour.StartCoroutine(coroutine);
		}
		
		public void StopCoroutine(Coroutine coroutine)
		{
			behaviour.StopCoroutine(coroutine);
		}
	}
	
	internal class CoroutinerBehaviour : MonoBehaviour
	{
		
	}
}