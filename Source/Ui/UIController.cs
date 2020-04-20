using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Object = UnityEngine.Object;

namespace Core.Ui
{
    internal class UIController
    {
        private List<UIView> opened;
        private UILoader loader;

        public UIController(UILoader loader)
        {
            opened = new List<UIView>();
            this.loader = loader;
        }

        internal TView Get<TView>() where TView : UIView
        {
            return (TView) opened.Find(view => view.GetType() == typeof(TView));
        }

        internal async Task<TView> Open<TView>(object data = null) where TView : UIView
        {
            UIInfoAttribute info = UIView<TView>.Info;

            foreach (UIView openedView in opened.Where(openedView =>
                info.Root.IsClosingOther(openedView.Info.Root)))
                openedView.Close();

            TView view = await loader.Load<TView>(info);

            view.Initialize(data);
            opened.Add(view);

            view.CloseAction = () =>
            {
                if (view == null) return;

                opened.Remove(view);
                view.OnClose();
                Object.Destroy(view.gameObject);
            };

            return view;
        }

        internal void CloseAll()
        {
            opened.ToList().ForEach(view => view.Close());
        }
    }
}