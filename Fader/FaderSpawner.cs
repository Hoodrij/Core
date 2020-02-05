using UnityEditor;
using UnityEngine;

namespace Core
{
	public class FaderSpawner
	{
		public IFaderView SpawnView()
		{
			Component prefab = Game.Assets.GetPreloaded(typeof(IFaderView));

			Component instantiatedView = Object.Instantiate(prefab);
			Object.DontDestroyOnLoad(instantiatedView);

			return instantiatedView as IFaderView;
		}
	}
}