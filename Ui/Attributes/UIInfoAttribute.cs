using System;

namespace Core.Ui
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UIInfoAttribute : Attribute
    {
        public Type Root { get; }
        public string Path { get; }
        public bool AsyncLoad { get; }

        public UIInfoAttribute(Type root, string path, bool asyncLoad = true)
        {
            Root = root;
            Path = path;
            AsyncLoad = asyncLoad;
        }
    }
}