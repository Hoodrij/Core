using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bindings.Editor
{
	public class FindBrokenBinder : EditorWindow
	{

		private enum EBinderType
		{
			TextBinder,
			TextureBinder,
		}

		private class BinderData
		{
			public Type type;
			public string _field;
		}

		private class Data
		{
			public string PrefabName;
			public string PrefabPath;
			public int ComponentsCount;
			public BinderData BinderData;
		}

		private List<BinderData> _binders = new List<BinderData>()
		{
			new BinderData() {type = typeof(TextBinder), _field = "_label"},
			new BinderData() {type = typeof(ImageBinder), _field = "_sprite"},
			new BinderData() {type = typeof(TextureBinder), _field = "_texture"}
		};

		private List<Data> _list = new List<Data>();

		[MenuItem("Tools/Binder Tools/Find Broken Binder")]
		static void Init()
		{
			var wnd = EditorWindow.GetWindow<FindBrokenBinder>();
		}

		private int _selectedBinder = 0;
		private string[] _names = new string[0];
		private Vector2 _scrollPosition = Vector2.zero;

		void Awake()
		{
			_names = _binders.Select(b => b.type.Name).ToArray();
		}

		void OnGUI()
		{
			GUILayout.BeginVertical();
			GUILayout.BeginHorizontal();
			GUILayout.Label("Binder");
			_selectedBinder = EditorGUILayout.Popup(_selectedBinder, _names, GUILayout.ExpandWidth(true));
			if (GUILayout.Button("Find")) Find(_binders.Find(b => b.type.Name == _names[_selectedBinder]));
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();


			_scrollPosition = GUILayout.BeginScrollView(_scrollPosition, "box", GUILayout.ExpandWidth(true));
			foreach (var data in _list)
			{
				FillLine(data);
			}
			GUILayout.EndScrollView();

		}

		private void FillLine(Data data)
		{
			GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
			if (GUILayout.Button(data.PrefabName, "box", GUILayout.Width(400)))
			{
				var obj = AssetDatabase.LoadAssetAtPath<GameObject>(data.PrefabPath);
				EditorGUIUtility.PingObject(obj);
				Selection.activeGameObject = obj;
			}
			if (GUILayout.Button(data.ComponentsCount.ToString(), "box", GUILayout.Width(100)))
			{
				var scene = SceneManager.GetActiveScene();
				var roots = scene.GetRootGameObjects();
				foreach (var root in roots)
				{
					var objs = root.GetComponentsInChildren(data.BinderData.type, true);
					foreach (var obj in objs)
					{
						if (!BinderIsNormal(obj, data.BinderData))
						{
							EditorGUIUtility.PingObject(obj);
							Selection.activeGameObject = obj.gameObject;
							break;
						}
					}
				}
			}
			GUILayout.EndHorizontal();
		}

		private void Find(BinderData data)
		{
			_list = new List<Data>();
			var prefabs = AssetDatabase.GetAllAssetPaths().Where(assetPath => assetPath.EndsWith(".prefab"));
			foreach (var path in prefabs)
			{
				GameObject p = AssetDatabase.LoadAssetAtPath<GameObject>(path);

				foreach (var comp in p.GetComponentsInChildren(data.type, true))
				{
					if (BinderIsNormal(comp, data)) continue;
					var obj = _list.Find(d => d.PrefabPath == path);
					if (obj != null)
					{
						++obj.ComponentsCount;
					}
					else
						_list.Add(new Data()
						{
							PrefabPath = path,
							PrefabName = p.name,
							ComponentsCount = 1,
							BinderData = data,
						});
				}
			}
		}

		private bool BinderIsNormal(object component, BinderData data)
		{
			FieldInfo field = data.type.GetField(data._field, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			return field.GetValue(component) != null;
		}
	}
}
