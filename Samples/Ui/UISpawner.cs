using UnityEngine;

namespace Core.Samples.Ui
{
	public class UISpawner
	{
		public GameObject Spawn()
		{
			Object prefab = Game.Assets.Load("UI");

			GameObject instantiatedView = Object.Instantiate(prefab) as GameObject;
			Object.DontDestroyOnLoad(instantiatedView);

			return instantiatedView;
		}
	}
}