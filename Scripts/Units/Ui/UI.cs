using System.Threading.Tasks;
using Core.Tools;
using Core.Ui;

namespace Core.Units
{
    internal class UI : Unit
    {
        [Inject] IAssets Assets;

        private readonly UIController controller;

        internal static UI Instance => Injector.Instance.Get<UI>();
        
        public UI()
        {
            UILoader loader = new UILoader(Assets);
            controller = new UIController(loader);
        }

        internal async Task<TView> Open<TView>(object data = null) where TView : UIView =>
            await controller.Open<TView>(data);

        internal TView Get<TView>() where TView : UIView => controller.Get<TView>();

        public void CloseAll() => controller.CloseAll();
    }
}