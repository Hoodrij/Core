using UnityEngine;

namespace Core.Samples.Fader
{
	public class FaderSpawner
	{
		public IFaderView Spawn()
		{
			Object prefab = Game.Assets.Load("FaderView");

			GameObject instantiatedView = Object.Instantiate(prefab) as GameObject;
			Object.DontDestroyOnLoad(instantiatedView);

			return instantiatedView.GetComponent(typeof(IFaderView)) as IFaderView;
		}
	}
}