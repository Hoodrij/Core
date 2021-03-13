using System;
using System.Reflection;
using System.Threading.Tasks;
using UnityAsync;

namespace Core.Ui
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UIInfoAttribute : Attribute
    {
        public Type RootType { get; }
        public string Path { get; }
        public bool CanBeOverlapped { get; }
        private UIView view; 

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
        
        public async Task<UIView> GetView()
        {
            await Wait.Until(() => view != null);
            return view;
        }

        public void SetView(UIView view) => this.view = view;
    }
}