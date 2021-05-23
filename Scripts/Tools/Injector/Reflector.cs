using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Tools.ExtensionMethods;
using Mono.Reflection;

namespace Core.Tools 
{
    public static class Reflector
    {
        private static readonly Dictionary<Type, MemberInfo[]> cachedFieldInfos = new Dictionary<Type, MemberInfo[]>();
        private static readonly Dictionary<Type, MethodInfo[]> cachedMethodInfos = new Dictionary<Type, MethodInfo[]>();
        private static readonly List<MemberInfo> reusableList = new List<MemberInfo>(1024);

        public static void Populate<T>(object obj, Func<Type, object> getter) where T : Attribute
        {
            MemberInfo[] members = GetMembers<T>(obj.GetType());
            foreach (MemberInfo member in members)
            {
                Type type = GetUnderlyingType(member);
                object value = getter(type);

                switch (member.MemberType)
                {
                    case MemberTypes.Field:
                        ((FieldInfo) member).SetValue(obj, value);
                        break;
                    case MemberTypes.Property:
                        PropertyInfo property = ((PropertyInfo) member);
                        property.GetBackingField().SetValue(obj, value);
                        break;
                }
            }
        }
    
        public static MemberInfo[] GetMembers<T>(Type type) where T : Attribute
        {
            if (cachedFieldInfos.TryGetValue(type, out MemberInfo[] cachedResult)) return cachedResult;

            BindingFlags flags = BindingFlags.Public |
                                 BindingFlags.NonPublic |
                                 BindingFlags.Static |
                                 BindingFlags.Instance |
                                 BindingFlags.DeclaredOnly;

            IEnumerable<MemberInfo> fields =
                from it in type.GetMembers(flags)
                where it is PropertyInfo || it is FieldInfo
                select it;

            foreach (Type baseType in GetBaseTypes(type))
                fields = fields.Concat(from it in baseType.GetMembers(flags)
                    where it is PropertyInfo || it is FieldInfo
                    select it);

            foreach (MemberInfo field in fields)
            {
                bool hasInjectAttribute = field.IsDefined(typeof(T));
                if (hasInjectAttribute) reusableList.Add(field);
            }

            MemberInfo[] resultAsArray = reusableList.ToArray();
            reusableList.Clear();
            cachedFieldInfos[type] = resultAsArray;
            return resultAsArray;
        }
    
        public static MethodInfo[] GetMethods<T>(Type type) where T : Attribute
        {
            if (cachedMethodInfos.TryGetValue(type, out MethodInfo[] cachedResult)) return cachedResult;

            MethodInfo[] methodInfos = type.GetAllMethodsWithAttribute<T>(false).ToArray();
            cachedMethodInfos[type] = methodInfos;
            return methodInfos;
        }

        private static List<Type> GetBaseTypes(Type type)
        {
            List<Type> result = new List<Type>();
            if (type.BaseType != null)
            {
                result.Add(type.BaseType);
                result.AddRange(GetBaseTypes(type.BaseType));
            }

            return result;
        }

        private static Type GetUnderlyingType(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo) member).FieldType;
                case MemberTypes.Property:
                    return ((PropertyInfo) member).PropertyType;
                default:
                    throw new ArgumentException
                    (
                        "Input MemberInfo must be if type FieldInfo or PropertyInfo"
                    );
            }
        }
    }
}