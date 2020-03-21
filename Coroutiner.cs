using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
	public class Coroutiner
	{
		CoroutinerBehaviour behaviour;
		
		internal Coroutiner()
		{
			if (!Application.isPlaying) return;
			
			GameObject gameObject = new GameObject("Game.Coroutiner");
			Object.DontDestroyOnLoad(gameObject);
			behaviour = gameObject.AddComponent<CoroutinerBehaviour>();
		}
		
		public Coroutine Start(IEnumerator coroutine)
		{
			return behaviour.StartCoroutine(coroutine);
		}
		
		public void Stop(Coroutine coroutine)
		{
			behaviour.StopCoroutine(coroutine);
		}
	}
	
	internal class CoroutinerBehaviour : MonoBehaviour
	{
		
	}
}