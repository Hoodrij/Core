#if UNITY_EDITOR
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteAlways]
public class CameraForUIPrefabMode : MonoBehaviour
{
	private Camera cam;
	private bool showingPrefabScene;

	private void OnEnable()
	{
		cam = GetComponent<Camera>();
		PrefabStage.prefabStageOpened -= OnPrefabStageOpened;
		PrefabStage.prefabStageOpened += OnPrefabStageOpened;

		PrefabStage.prefabStageClosing -= OnPrefabStageClosing;
		PrefabStage.prefabStageClosing += OnPrefabStageClosing;
		OnPrefabStageOpened(PrefabStageUtility.GetCurrentPrefabStage()); //check on recompile
	}

	private void OnDisable()
	{
		PrefabStage.prefabStageOpened -= OnPrefabStageOpened;
		PrefabStage.prefabStageClosing -= OnPrefabStageClosing;
		OnPrefabStageClosing(null); //ensure rendering regular scene again when closing and just before recompile
	}

	private void OnPrefabStageOpened(PrefabStage stage)
	{
		if (showingPrefabScene) return;
		if (stage == null) return;

		foreach (var rootGameObject in stage.scene.GetRootGameObjects())
		{
			var canvas = rootGameObject.GetComponent<Canvas>();
			if (canvas != null)
				canvas.worldCamera = cam;
		}

		cam.scene = stage.scene; //set camera to render preview scene
		showingPrefabScene = true;
	}

	private void OnPrefabStageClosing(PrefabStage stage)
	{
		if (!showingPrefabScene) return;

		cam.scene = SceneManager.GetActiveScene(); //return to normal scene
		showingPrefabScene = false;
	}
}
#endif