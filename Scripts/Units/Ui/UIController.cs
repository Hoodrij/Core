using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Tools.Observables;

namespace Core.Ui
{
    internal class UIController
    {
        private readonly List<UIView> opened = new List<UIView>();
        private readonly UILoader loader;

        public UIController(UILoader loader)
        {
            this.loader = loader;
        }

        internal TView Get<TView>() where TView : UIView
        {
            return (TView) opened.Find(view => view.GetType() == typeof(TView));
        }

        internal async Task<TView> Open<TView>(object data = null) where TView : UIView
        {
            UIInfoAttribute info = UIView<TView>.Info;

            await CloseRequiredOpened(info);

            TView view = await loader.Load<TView>(info);

            opened.Add(view);
            view.Open(data);
            view.CloseEvent.Listen(() => opened.Remove(view), this);

            return view;
        }

        private async Task CloseRequiredOpened(UIInfoAttribute nextViewInfo)
        {
            for (int index = opened.Count - 1; index >= 0; index--)
            {
                UIView openedView = opened[index];
                if (!nextViewInfo.IsClosingOther(openedView.Info)) continue;
                
                if (openedView.Info.CanBeOverlapped)
                    await openedView.Close();
                else
                    await openedView.CloseEvent;
            }
        }

        internal void CloseAll()
        {
            opened.ToList().ForEach(view => view.Close());
        }
    }
}