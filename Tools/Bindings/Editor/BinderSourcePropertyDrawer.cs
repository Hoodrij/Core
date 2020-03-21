using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace Core.Tools.Bindings.Editor
{
	[CustomPropertyDrawer(typeof(ABinder.BindSource), true)]
	public class BindSourcePropertyDrawer : PropertyDrawer
	{
		private List<Component> _properties;
		private List<String> _propertyNames;
		private String[] _propertyNamesNice;
		private Type _bindType;

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return 0;
		}
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (_bindType == null)
			{
				var attrs = fieldInfo.GetCustomAttributes(typeof(BindToAttribute), true);

				if (attrs == null || attrs.Length == 0)
				{
					GUI.color = Color.red;
					GUILayout.Label("Property " + property.displayName + " has no BindToAttribute! Add one to go on.");
					GUI.color = Color.white;
					return;
				}

				_bindType = ((BindToAttribute)attrs[0]).BindToType;
			}

			//property.serializedObject.Update( );

			if (_properties == null || _properties.Count == 0)
				UpdateMethods(property);

			DrawProp(position, property);
			//property.serializedObject.ApplyModifiedProperties( );
		}

		private void DrawProp(Rect position, SerializedProperty property)
		{
			GUILayout.BeginVertical(property.displayName, GUI.skin.window, GUILayout.Height(20));
			{
				var componentProp = property.FindPropertyRelative("Target");
				var propertyProp = property.FindPropertyRelative("MemberName");
				var paramsProp = property.FindPropertyRelative("Params");

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(componentProp, false);

				var targetChanged = false;

				if (EditorGUI.EndChangeCheck())
				{
					UpdateMethods(property);
					targetChanged = true;
				}

				if (_propertyNames.Count == 0)
				{
					GUI.color = Color.red;
					EditorGUILayout.LabelField(componentProp.objectReferenceValue != null ? "Target Has No Bindable Properties" : "Choose Target First!!!");
					GUI.color = Color.white;
				}
				else
				{
					var index = _propertyNames.IndexOf(propertyProp.stringValue);

					if (index == -1)
					{
						index = 0;
						propertyProp.stringValue = _propertyNames[index];
						componentProp.objectReferenceValue = _properties[index];
					}
					else if (targetChanged)
					{
						componentProp.objectReferenceValue = _properties[index];
					}

					EditorGUI.BeginChangeCheck();
					index = EditorGUILayout.Popup("Property", index, _propertyNamesNice);

					if (EditorGUI.EndChangeCheck())
					{
						propertyProp.stringValue = _propertyNames[index];
						componentProp.objectReferenceValue = _properties[index];
						paramsProp.stringValue = "";
					}

					var method = componentProp.objectReferenceValue.GetType().GetMethod(propertyProp.stringValue, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (method != null)
					{
						var @params = method.GetParameters();
						if (@params.Length == 1)
						{
							if (@params[0].ParameterType == typeof(String))
								paramsProp.stringValue = EditorGUILayout.TextField(ObjectNames.NicifyVariableName(@params[0].Name), paramsProp.stringValue);

							else if (@params[0].ParameterType == typeof(Int32))
								paramsProp.stringValue = EditorGUILayout.IntField(ObjectNames.NicifyVariableName(@params[0].Name), paramsProp.stringValue == "" ? 0 : Int32.Parse(paramsProp.stringValue)).ToString();

							else if (@params[0].ParameterType == typeof(Single))
								paramsProp.stringValue = EditorGUILayout.FloatField(ObjectNames.NicifyVariableName(@params[0].Name), paramsProp.stringValue == "" ? 0.0f : Single.Parse(paramsProp.stringValue)).ToString();

							else if (@params[0].ParameterType == typeof(Boolean))
								paramsProp.stringValue = EditorGUILayout.Toggle(ObjectNames.NicifyVariableName(@params[0].Name), paramsProp.stringValue != "" && Boolean.Parse(paramsProp.stringValue)).ToString();

							else if (@params[0].ParameterType == typeof(Enum))
							{
								var type = ((BindableAttribute[])method.GetCustomAttributes(typeof(BindableAttribute), true))[0].ArgumentType;
								paramsProp.stringValue = EditorGUILayout.EnumPopup(ObjectNames.NicifyVariableName(type.Name), (Enum)Enum.Parse(type, paramsProp.stringValue == "" ? Enum.GetNames(type)[0] : paramsProp.stringValue)).ToString();
							}
						}
						else
						{
							paramsProp.stringValue = "";
						}
					}
					else
					{
						paramsProp.stringValue = "";
					}
				}
			}
			GUILayout.EndVertical();
		}

		private void UpdateMethods(SerializedProperty property)
		{
			var componentProp = property.FindPropertyRelative("Target");

			UpdateMethods(componentProp, _bindType, out _properties, out _propertyNames, out _propertyNamesNice);
		}

		public static void UpdateMethods(SerializedProperty componentProp, Type bindType, out List<Component> properties, out List<String> propertyNames, out String[] propertyNamesNice)
		{
			if (componentProp.objectReferenceValue == null)
			{
				properties = new List<Component>();
				propertyNames = new List<String>();
				propertyNamesNice = new String[0];
			}
			else
			{
				var obj = ((Component)componentProp.objectReferenceValue).gameObject;
				properties = new List<Component>();
				propertyNames = new List<String>();
				var nicedNames = new List<String>();

				foreach (var component in obj.GetComponents<MonoBehaviour>())
				{
					var type = component.GetType();
					do
					{
						foreach (var propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
						{
							var attrs = propertyInfo.GetCustomAttributes(typeof(BindableAttribute), true);

							if (attrs.Length == 0)
								continue;

							if (!bindType.IsAssignableFrom(propertyInfo.PropertyType) && !bindType.IsAssignableFrom(((BindableAttribute)attrs[0]).ReturnType))
								continue;

							properties.Add(component);
							propertyNames.Add(propertyInfo.Name);
							nicedNames.Add(ObjectNames.NicifyVariableName(component.GetType().Name + " - " + propertyInfo.Name));
						}
						type = type.BaseType;
					}
					while (type != typeof(Object));

					type = component.GetType();
					do
					{
						foreach (var methodInfo in component.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(methodInfo => bindType.IsAssignableFrom(methodInfo.ReturnType) && methodInfo.GetCustomAttributes(typeof(BindableAttribute), true).Length > 0))
						{
							properties.Add(component);
							propertyNames.Add(methodInfo.Name);

							var @params = methodInfo.GetParameters();
							if (@params.Length == 0)
								nicedNames.Add(ObjectNames.NicifyVariableName(component.GetType().Name + " - " + methodInfo.Name + "( )"));
							else
								nicedNames.Add(ObjectNames.NicifyVariableName(component.GetType().Name + " - " + methodInfo.Name + "( " + @params[0].Name + " )"));
						}
						type = type.BaseType;
					}
					while (type != typeof(Object));
				}

				propertyNamesNice = nicedNames.ToArray();
			}
		}
	}
}