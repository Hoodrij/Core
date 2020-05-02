using System;

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

        internal UIRoot Root => UI.Instance.GetRoot(RootType);
    }
}