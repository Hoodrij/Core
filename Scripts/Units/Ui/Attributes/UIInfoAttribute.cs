using System;
using System.Reflection;

namespace Core.Ui
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UIInfoAttribute : Attribute
    {
        public Type RootType { get; }
        public string Path { get; }

        public UIInfoAttribute(Type root, string path)
        {
            RootType = root;
            Path = path;
        }

        internal bool IsClosingOther(UIInfoAttribute info)
        {
            UIRootInfoAttribute rootInfo = RootType.GetCustomAttribute<UIRootInfoAttribute>();
            return rootInfo.IsClosingOther(info.RootType);
        }
    }
}