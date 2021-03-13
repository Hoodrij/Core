using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Tools.Observables;

namespace Core.Ui
{
    internal class UIController
    {
        private readonly List<UIInfoAttribute> opened = new List<UIInfoAttribute>();
        private readonly UILoader loader;

        public UIController(UILoader loader)
        {
            this.loader = loader;
        }

        internal TView Get<TView>() where TView : UIView
        {
            return null;//(TView) opened.Find(view => view.GetType() == typeof(TView)).GetView();
        }

        internal async Task<TView> Open<TView>(object data = null) where TView : UIView
        {
            UIInfoAttribute info = UIView<TView>.Info;
            opened.Add(info);

            await WaitFreeRoot(info);
            await CloseRequiredOpened(info);

            TView view = await loader.Load<TView>(info);
            info.SetView(view);

            view.Open(data);
            view.CloseEvent.Listen(() => opened.Remove(info), this);

            return view;
        }

        private async Task CloseRequiredOpened(UIInfoAttribute nextViewInfo)
        {
            for (int index = opened.Count - 1; index >= 0; index--)
            {
                UIInfoAttribute openedInfo = opened[index];
                if (!nextViewInfo.IsClosingOther(openedInfo)) continue;
                if (Equals(nextViewInfo, openedInfo)) continue;
                UIView view = await openedInfo.GetView();
                await view.Close();
            }
        }

        private async Task WaitFreeRoot(UIInfoAttribute nextViewInfo)
        {
            foreach (UIInfoAttribute openedInfo in opened.ToList())
            {
                if (nextViewInfo == openedInfo) break;
                if (!nextViewInfo.IsClosingOther(openedInfo)) continue;
                if (openedInfo.CanBeOverlapped) continue;
                UIView view = await openedInfo.GetView();
                await view.CloseEvent;
            }
        }

        internal void CloseAll()
        {
            // opened.ToList().ForEach(view => view.Close());
        }
    }
}