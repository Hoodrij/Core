using Core.Utils.ExtensionMethods;
using UnityEngine;

namespace Core.Ui
{
	public class UIGenerator
	{
		GameObject uiGO;
		
		public UIGenerator()
		{
			uiGO = new GameObject("[UI]");
			Object.DontDestroyOnLoad(uiGO);
			
		}
		
		internal void AddRoot(UIRoot root)
		{
			GameObject rootGO = new GameObject(root.Name);
		}
	}
}