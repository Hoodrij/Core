using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace Core.Tools.EditorTools.Editor
{
    public static class EditorTools
    {
        //% (ctrl), # (shift), & (alt)
        public const string TOOLS = "Tools/";
        public const string TOOLS_OTHER = TOOLS + "Hotkeys/";

        [MenuItem(TOOLS_OTHER + "ResetTransform &z")]
        private static void ResetTransform()
        {
            if (!IS.EDITOR) return;

            foreach (Transform t in Selection.transforms)
            {
                t.localPosition = Vector3.zero;
                t.rotation = Quaternion.identity;
                t.localScale = Vector3.one;
            }

            EditorSceneManager.MarkAllScenesDirty();

            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null) EditorSceneManager.MarkSceneDirty(prefabStage.scene);
        }

        [MenuItem(TOOLS_OTHER + "Toggle active &d")]
        private static void ToggleActive()
        {
            if (!IS.EDITOR) return;

            foreach (GameObject go in Selection.gameObjects) go.SetActive(!go.activeSelf);

            EditorSceneManager.MarkAllScenesDirty();

            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null) EditorSceneManager.MarkSceneDirty(prefabStage.scene);
        }

        [MenuItem(TOOLS_OTHER + "Run GAME &a")]
        private static void RunGame()
        {
            if (!IS.EDITOR) return;

            if (!Application.isPlaying)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(0);
                EditorSceneManager.OpenScene(scenePath);
            }

            EditorApplication.ExecuteMenuItem("Edit/Play");
        }
    }
}