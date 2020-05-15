using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using System.Collections.Generic;

namespace Core.Tools
{
    public class FontReplacer : EditorWindow
    {
        private const string EditorPrefsKey = "Tools.FontReplacer";
        private const string MenuItemName = "Tools/Replace Fonts...";

        private Font _src;
        private Font _dest;
        private bool _includePrefabs;

        [MenuItem(MenuItemName)] public static void DisplayWindow()
        {
            FontReplacer window = GetWindow<FontReplacer>(true, "Replace Fonts");
            Rect position = window.position;
            position.size = new Vector2(position.size.x, 151);
            position.center = new Rect(0f, 0f, Screen.width, Screen.height).center;
            window.position = position;
            window.Show();
        }

        public void OnEnable()
        {
            string path = EditorPrefs.GetString(EditorPrefsKey + ".src");
            if (path != string.Empty)
                _src = AssetDatabase.LoadAssetAtPath<Font>(path) ?? Resources.GetBuiltinResource<Font>(path);

            path = EditorPrefs.GetString(EditorPrefsKey + ".dest");
            if (path != string.Empty)
                _dest = AssetDatabase.LoadAssetAtPath<Font>(path) ?? Resources.GetBuiltinResource<Font>(path);

            _includePrefabs = EditorPrefs.GetBool(EditorPrefsKey + ".includePrefabs", false);
        }

        public void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PrefixLabel("Find:");
            _src = (Font) EditorGUILayout.ObjectField(_src, typeof(Font), false);

            EditorGUILayout.Space();
            EditorGUILayout.PrefixLabel("Replace with:");
            _dest = (Font) EditorGUILayout.ObjectField(_dest, typeof(Font), false);

            EditorGUILayout.Space();
            _includePrefabs = EditorGUILayout.ToggleLeft("Include Prefabs", _includePrefabs);
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetString(EditorPrefsKey + ".src", GetAssetPath(_src, "ttf"));
                EditorPrefs.SetString(EditorPrefsKey + ".dest", GetAssetPath(_dest, "ttf"));
                EditorPrefs.SetBool(EditorPrefsKey + ".includePrefabs", _includePrefabs);
            }

            GUI.color = Color.green;
            if (GUILayout.Button("Replace All", GUILayout.Height(EditorGUIUtility.singleLineHeight * 2f)))
                ReplaceFonts(_src, _dest, _includePrefabs);

            GUI.color = Color.white;
        }

        private static void ReplaceFonts(Font src, Font dest, bool includePrefabs)
        {
            int sceneMatches = 0;
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                List<GameObject> gos = new List<GameObject>(scene.GetRootGameObjects());
                foreach (GameObject go in gos)
                    sceneMatches += ReplaceFonts(src, dest, go.GetComponentsInChildren<Text>(true));
            }

            if (includePrefabs)
            {
                int prefabMatches = 0;
                IEnumerable<GameObject> prefabs =
                    AssetDatabase.FindAssets("t:Prefab")
                        .Select(guid => AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid)));
                foreach (GameObject prefab in prefabs)
                    prefabMatches += ReplaceFonts(src, dest, prefab.GetComponentsInChildren<Text>(true));

                Debug.LogFormat("Replaced {0} font(s), {1} in scenes, {2} in prefabs", sceneMatches + prefabMatches,
                    sceneMatches, prefabMatches);
            }
            else
            {
                Debug.LogFormat("Replaced {0} font(s) in scenes", sceneMatches);
            }
        }

        private static int ReplaceFonts(Font src, Font dest, IEnumerable<Text> texts)
        {
            int matches = 0;
            IEnumerable<Text> textsFiltered = src != null ? texts.Where(text => text.font == src) : texts;
            foreach (Text text in textsFiltered)
            {
                text.font = dest;
                matches++;
            }

            return matches;
        }

        private static string GetAssetPath(Object assetObject, string defaultExtension)
        {
            string path = AssetDatabase.GetAssetPath(assetObject);
            if (path.StartsWith("Library/", System.StringComparison.InvariantCultureIgnoreCase))
                path = assetObject.name + "." + defaultExtension;
            return path;
        }
    }
}
#endif