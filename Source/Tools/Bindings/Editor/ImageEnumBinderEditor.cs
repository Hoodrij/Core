using System;
using UnityEditor;
using UnityEngine;

namespace Core.Tools.Bindings.Editor
{
    [CustomEditor(typeof(ImageEnumBinder), true)] [CanEditMultipleObjects]
    public class ImageEnumBinderEditor : ABinderEditor
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
                    var sprite = stateObjects.GetArrayElementAtIndex(i).FindPropertyRelative("Sprite");

                    if (string.IsNullOrEmpty(enumPropVal.stringValue))
                    {
                        var names = Enum.GetNames(_memberType);

                        if (names.Length > 0)
                            enumPropVal.stringValue = names[0];
                    }

                    try
                    {
                        enumPropVal.stringValue = EditorGUILayout.EnumPopup((Enum) Enum.Parse(_memberType, enumPropVal.stringValue)).ToString();
                        sprite.objectReferenceValue = EditorGUILayout.ObjectField(sprite.objectReferenceValue, typeof(Sprite), true);
                    }
                    catch
                    {
                        if (string.IsNullOrEmpty(enumPropVal.stringValue))
                        {
                            var names = Enum.GetNames(_memberType);

                            if (names.Length > 0)
                                enumPropVal.stringValue = names[0];
                        }
                    }
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