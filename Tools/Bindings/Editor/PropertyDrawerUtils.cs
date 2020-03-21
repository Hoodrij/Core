using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Core.Tools.Bindings.Editor
{
	public static class PropertyDrawerUtils
	{
		static PropertyDrawerUtils()
		{
			var drawerType = typeof(PropertyDrawer);

			_drawerTypeForTypeFieldInfo = drawerType.GetField("s_DrawerTypeForType", BindingFlags.NonPublic | BindingFlags.Static);
			_propertyDrawersFieldInfo = drawerType.GetField("s_PropertyDrawers", BindingFlags.NonPublic | BindingFlags.Static);
			_builtinAttributesFieldInfo = drawerType.GetField("s_BuiltinAttributes", BindingFlags.NonPublic | BindingFlags.Static);
			_attributePathFieldInfo = drawerType.GetField("m_Attribute", BindingFlags.NonPublic | BindingFlags.Instance);
			_getDrawerMwthodInfo = drawerType.GetMethod("GetDrawer", BindingFlags.NonPublic | BindingFlags.Static);

			var serializedPropertyType = typeof(SerializedProperty);

			_arrayIndexLessPropertyPathPropertyInfo = serializedPropertyType.GetProperty("arrayIndexLessPropertyPath", BindingFlags.NonPublic | BindingFlags.Instance);
			_intptrFieldInfo = serializedPropertyType.GetField("m_Property", BindingFlags.NonPublic | BindingFlags.Instance);

			var serializedObjectType = typeof(SerializedObject);

			_objIntptrFieldInfo = serializedObjectType.GetField("m_Property", BindingFlags.NonPublic | BindingFlags.Instance);
		}

		private static readonly FieldInfo _drawerTypeForTypeFieldInfo;
		private static readonly FieldInfo _propertyDrawersFieldInfo;
		private static readonly FieldInfo _builtinAttributesFieldInfo;
		private static readonly FieldInfo _attributePathFieldInfo;
		private static readonly MethodInfo _getDrawerMwthodInfo;

		private static readonly FieldInfo _intptrFieldInfo;
		private static readonly PropertyInfo _arrayIndexLessPropertyPathPropertyInfo;

		private static readonly FieldInfo _objIntptrFieldInfo;

		public static Dictionary<Type, Type> s_DrawerTypeForType
		{
			get
			{
				return (Dictionary<Type, Type>)_drawerTypeForTypeFieldInfo.GetValue(null);
			}
		}
		public static Dictionary<String, PropertyDrawer> s_PropertyDrawers
		{
			get
			{
				return (Dictionary<String, PropertyDrawer>)_propertyDrawersFieldInfo.GetValue(null);
			}
		}
		public static Dictionary<String, PropertyAttribute> s_BuiltinAttributes
		{
			get
			{
				return (Dictionary<String, PropertyAttribute>)_builtinAttributesFieldInfo.GetValue(null);
			}
		}

		public static PropertyDrawer GetDrawer(this SerializedProperty property)
		{
			return (PropertyDrawer)_getDrawerMwthodInfo.Invoke(null, new object[] { property });

			//if (property == null)
			//	return (PropertyDrawer) null;

			////if (property.serializedObject.inspectorMode != InspectorMode.Normal)
			////  return (PropertyDrawer) null;

			//var propertyString = GetPropertyString( property );
			//PropertyDrawer propertyDrawer1;
			//if (s_PropertyDrawers.TryGetValue(propertyString, out propertyDrawer1))
			//	return propertyDrawer1;

			//Type type1 = null;
			//PropertyAttribute propertyAttribute;
			//var targetObject = property.serializedObject.targetObject;

			//if ( targetObject is MonoBehaviour || targetObject is ScriptableObject )
			//{
			//	propertyAttribute = GetFieldAttribute( GetFieldInfoFromProperty( property, out type1 ) );
			//}
			//else
			//{
			//	if ( s_BuiltinAttributes == null )
			//		PopulateBuiltinAttributes( );

			//	propertyAttribute = GetBuiltinAttribute( property );
			//}
			//Type type2 = null;

			//if ( propertyAttribute != null )
			//	type2 = GetDrawerTypeForType( propertyAttribute.GetType( ) );

			//if ( type2 == null && type1 != null )
			//		type2 = GetDrawerTypeForType( type1 );

			//if ( type2 != null )
			//{
			//	var propertyDrawer2 = (PropertyDrawer) Activator.CreateInstance( type2 );
			//	propertyDrawer2.SetAttribute( propertyAttribute );
			//	s_PropertyDrawers[propertyString] = propertyDrawer2;

			//	return propertyDrawer2;
			//}

			//s_PropertyDrawers[propertyString] = null;
			//return null;
		}
		public static void SetAttribute(this PropertyDrawer drawer, PropertyAttribute attr)
		{
			_attributePathFieldInfo.SetValue(drawer, attr);
		}
		public static FieldInfo GetFieldInfoFromProperty(SerializedProperty property, out Type type)
		{
			var typeFromProperty = GetScriptTypeFromProperty(property);
			if (typeFromProperty != null)
				return GetFieldInfoFromPropertyPath(typeFromProperty, property.propertyPath, out type);

			type = null;
			return null;
		}
		public static String GetArrayIndexLessPropertyPath(this SerializedProperty property)
		{
			return (String)_arrayIndexLessPropertyPathPropertyInfo.GetValue(property, null);
		}
		public static IntPtr GetIntPtr(this SerializedProperty property)
		{
			return (IntPtr)_intptrFieldInfo.GetValue(property);
		}
		//public static					Object[]									GetTargets							( this SerializedProperty property )											
		//{
		//	var result			= new Object[ property.serializedObject.targetObjects.Length ];
		//	var type			= GetScriptTypeFromProperty( property );
		//	var path			= property.propertyPath;
		//	var data			= new System.Object[ property.serializedObject.targetObjects.Length ];

		//	for ( var i = 0; i < property.serializedObject.targetObjects.Length; i++ )
		//		data[i] = property.serializedObject.targetObjects[i];

		//	var strArray = path.Split('.');

		//	for ( var i = 0; i < strArray.Length; ++i )
		//	{
		//		var fieldName		= strArray[i];
		//		FieldInfo fieldInfo;

		//		if ( i < strArray.Length - 1 && fieldName == "Array" && strArray[i + 1].StartsWith( "data[" ) )
		//		{
		//			var index = Int32.Parse( strArray[i + 1].Substring( 5, 1 ) );

		//			if ( type.IsArray )
		//			{
		//				type = type.GetElementType( );

		//				for ( var j = 0; j < data.Length; j++ )
		//					data[j] =  ((IEnumerable)data[j]).ElementAt( index );
		//			}

		//			else if ( type.GetGenericTypeDefinition( ) == typeof ( List<> ) )
		//			{
		//				type = type.GetGenericArguments( )[0];

		//				for ( var j = 0; j < data.Length; j++ )
		//					data[j] = ((IList)data[j])[index];
		//			}

		//			fieldInfo = null;
		//			++i;
		//		}
		//		else
		//		{
		//			fieldInfo = type.GetField( fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );
		//			if ( fieldInfo == null )
		//				return null;

		//			for ( var j = 0; j < data.Length; j++ )
		//				data[j] = fieldInfo.GetValue( data[j] );

		//			type = fieldInfo.FieldType;
		//		}
		//	}

		//	for ( var i = 0; i < result.Length; i++ )
		//		result[i] = (Object)data[i];

		//	return result;
		//}

		public static IntPtr GetIntPtr(this SerializedObject obj)
		{
			return (IntPtr)_objIntptrFieldInfo.GetValue(obj);
		}

		private static void PopulateBuiltinAttributes()
		{
			//PropertyDrawer.s_BuiltinAttributes = new Dictionary<string, PropertyAttribute>();
			//PropertyDrawer.AddBuiltinAttribute("Label", "m_Text", (PropertyAttribute) new MultilineAttribute(5));
		}
		private static void AddBuiltinAttribute(String componentTypeName, String propertyPath, PropertyAttribute attr)
		{
			//string key = componentTypeName + "_" + propertyPath;
			//PropertyDrawer.s_BuiltinAttributes.Add(key, attr);
		}
		private static PropertyAttribute GetBuiltinAttribute(SerializedProperty property)
		{
			//if (property.serializedObject.targetObject == (UnityEngine.Object) null)
			//	return (PropertyAttribute) null;
			//System.Type type = property.serializedObject.targetObject.GetType();
			//if (type == null)
			//	return (PropertyAttribute) null;
			//string key = type.Name + "_" + property.propertyPath;
			//PropertyAttribute propertyAttribute = (PropertyAttribute) null;
			//PropertyDrawer.s_BuiltinAttributes.TryGetValue(key, out propertyAttribute);
			//return propertyAttribute;
			return null;
		}
		private static void BuildDrawerTypeForTypeDictionary()
		{
			//PropertyDrawer.s_DrawerTypeForType = new Dictionary<Type, Type>();
			//foreach ( Type type in EditorAssemblies.SubclassesOf(typeof (PropertyDrawer ) ) )
			//{
			//  foreach (CustomPropertyDrawer customPropertyDrawer in type.GetCustomAttributes(typeof (CustomPropertyDrawer), true))
			//	PropertyDrawer.s_DrawerTypeForType[customPropertyDrawer.type] = type;
			//}
		}
		private static Type GetDrawerTypeForType(Type type)
		{
			if (s_DrawerTypeForType == null)
				BuildDrawerTypeForTypeDictionary();
			Type type1 = (System.Type)null;
			s_DrawerTypeForType.TryGetValue(type, out type1);
			return type1;
		}
		private static String GetPropertyString(SerializedProperty property)
		{
			if (property.serializedObject.targetObject == null)
				return string.Empty;

			return "" + property.serializedObject.targetObject.GetInstanceID() + "_" + property.GetArrayIndexLessPropertyPath();
		}
		private static PropertyAttribute GetFieldAttribute(FieldInfo field)
		{
			if (field == null)
				return (PropertyAttribute)null;
			object[] customAttributes = field.GetCustomAttributes(typeof(PropertyAttribute), true);
			if (customAttributes != null && customAttributes.Length > 0)
				return (PropertyAttribute)customAttributes[0];
			else
				return (PropertyAttribute)null;
		}

		private static Type GetScriptTypeFromProperty(SerializedProperty property)
		{
			SerializedProperty property1 = property.serializedObject.FindProperty("m_Script");
			if (property1 == null)
				return null;
			var monoScript = property1.objectReferenceValue as MonoScript;

			if (monoScript == null)
				return null;

			return monoScript.GetClass();
		}
		private static FieldInfo GetFieldInfoFromPropertyPath(Type host, string path, out Type type)
		{
			var fieldInfo = (FieldInfo)null;
			type = host;
			var str = path;
			var chArray = new char[1];
			var index1 = 0;
			var num = 46;
			chArray[index1] = (char)num;
			var strArray = str.Split(chArray);
			for (var index2 = 0; index2 < strArray.Length; ++index2)
			{
				var name = strArray[index2];
				if (index2 < strArray.Length - 1 && name == "Array" && strArray[index2 + 1].StartsWith("data["))
				{
					if (type.IsArray)
						type = type.GetElementType();
					else if (type.GetGenericTypeDefinition() == typeof(List<>))
						type = type.GetGenericArguments()[0];
					fieldInfo = null;
					++index2;
				}
				else
				{
					fieldInfo = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (fieldInfo == null)
						return null;
					type = fieldInfo.FieldType;
				}
			}
			return fieldInfo;
		}
	}
}