using System;
using UnityEditor;
using UnityEngine;

namespace Core.Tools.Bindings.Editor
{
	[CustomEditor(typeof(GraphicColorEnumBinder), true), CanEditMultipleObjects]
	public class TextColorEnumBinderEditor : ABinderEditor
	{
		private void DrawEnumBindingProperties()
		{
			if (_memberType == null)
				return;

			if (!_memberType.IsEnum)
			{
				if (_memberType == typeof(Enum))
				{
					GUI.color = Color.red;
					GUILayout.Label("Add Return Type into BindableAttrbiute of your Enum member");
					GUI.color = Color.white;
				}
				else
				{
					GUI.color = Color.red;
					GUILayout.Label("Member Type is Not Enum subtype");
					GUI.color = Color.white;
				}
				return;
			}

			var stateObjects = serializedObject.FindProperty("_stateObjects");
			stateObjects.arraySize = EditorGUILayout.IntField("Values count", stateObjects.arraySize);

			for (var i = 0; i < stateObjects.arraySize; i++)
			{
				GUILayout.BeginHorizontal();
				{
					var enumPropVal = stateObjects.GetArrayElementAtIndex(i).FindPropertyRelative("EnumVal");
					var color = stateObjects.GetArrayElementAtIndex(i).FindPropertyRelative("Color");

					if (String.IsNullOrEmpty(enumPropVal.stringValue))
					{
						var names = Enum.GetNames(_memberType);

						if (names.Length > 0)
							enumPropVal.stringValue = names[0];
					}

					enumPropVal.stringValue = EditorGUILayout.EnumPopup((Enum)Enum.Parse(_memberType, enumPropVal.stringValue)).ToString();
					color.colorValue = EditorGUILayout.ColorField(color.colorValue);
				}
				GUILayout.EndHorizontal();
			}
		}

		protected override void DrawBinderProperties()
		{
			DrawPropertiesExcluding(serializedObject, "m_Script", "_target", "_memberName", "_params", "_stateObjects");
			DrawEnumBindingProperties();
		}
	}
}
