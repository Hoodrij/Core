using System;
using System.Reflection;

namespace Core.Ui
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UIInfoAttribute : Attribute
    {
        public Type RootType { get; }
        public string Path { get; }
        public bool CanBeOverlapped { get; }

        public UIInfoAttribute(Type root, string path, bool canBeOverlapped = true)
        {
            RootType = root;
            Path = path;
            CanBeOverlapped = canBeOverlapped;
        }

        internal bool IsClosingOther(UIInfoAttribute info)
        {
            UIRootInfoAttribute rootInfo = RootType.GetCustomAttribute<UIRootInfoAttribute>();
            return rootInfo.IsClosingOther(info.RootType);
        }
    }
}