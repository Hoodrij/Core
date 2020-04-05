﻿using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;
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
            if (!Macros.EDITOR) return;

            foreach (var t in Selection.transforms)
            {
                t.localPosition = Vector3.zero;
                t.rotation = Quaternion.identity;
                t.localScale = Vector3.one;
            }

            EditorSceneManager.MarkAllScenesDirty();

            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null) EditorSceneManager.MarkSceneDirty(prefabStage.scene);
        }

        [MenuItem(TOOLS_OTHER + "Toggle active &d")]
        private static void ToggleActive()
        {
            if (!Macros.EDITOR) return;

            foreach (var go in Selection.gameObjects) go.SetActive(!go.activeSelf);

            EditorSceneManager.MarkAllScenesDirty();

            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null) EditorSceneManager.MarkSceneDirty(prefabStage.scene);
        }
    }
}