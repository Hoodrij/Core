#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class EditorTools
{
	//% (ctrl), # (shift), & (alt)
	public const string TOOLS = "Tools/";
	public const string TOOLS_OTHER = TOOLS + "Hotkeys/";
	public const string PLAYER_PREFS = TOOLS + "PlayerPrefs/";

	[MenuItem(TOOLS_OTHER + "ResetTransform &z")]
	private static void ResetTransform()
	{
		foreach (Transform t in Selection.transforms)
		{
			t.localPosition = Vector3.zero;
			t.rotation = Quaternion.identity;
			t.localScale = Vector3.one;
		}

		EditorSceneManager.MarkAllScenesDirty();

		var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
		if (prefabStage != null)
		{
			EditorSceneManager.MarkSceneDirty(prefabStage.scene);
		}
	}

	[MenuItem(TOOLS_OTHER + "Toggle active &d")]
	private static void ToggleActive()
	{
		foreach (GameObject go in Selection.gameObjects)
		{
			go.SetActive(!go.activeSelf);
		}

		EditorSceneManager.MarkAllScenesDirty();

		var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
		if (prefabStage != null)
		{
			EditorSceneManager.MarkSceneDirty(prefabStage.scene);
		}
	}

	[MenuItem(TOOLS_OTHER + "Apply prefab &a")]
	private static void ApplyPrefab()
	{
		var sel = Selection.activeGameObject;

		if (sel != null)
		{
			var parent = PrefabUtility.GetCorrespondingObjectFromSource(sel);
			if (parent != null)
			{
				PrefabUtility.ReplacePrefab(PrefabUtility.FindRootGameObjectWithSameParentPrefab(sel), parent, ReplacePrefabOptions.ConnectToPrefab);
			}
		}
	}
}
#endif
